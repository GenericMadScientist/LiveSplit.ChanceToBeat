using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.ChanceToBeat
{
    public class ChanceToBeat : IComponent
    {
        public string ComponentName => Settings.ProbabilityText;
        public ChanceToBeatSettings Settings { get; set; }
        protected InfoPercentComponent InternalComponent { get; set; }

        public float HorizontalWidth => InternalComponent.HorizontalWidth;
        public float MinimumHeight => InternalComponent.MinimumHeight;
        public float VerticalHeight => InternalComponent.VerticalHeight;
        public float MinimumWidth => InternalComponent.MinimumWidth;

        public float PaddingTop => InternalComponent.PaddingTop;
        public float PaddingBottom => InternalComponent.PaddingBottom;
        public float PaddingLeft => InternalComponent.PaddingLeft;
        public float PaddingRight => InternalComponent.PaddingRight;

        public IDictionary<string, Action> ContextMenuControls => null;

        private LiveSplitState currentState;

        private const double Weight = 0.95;

        public ChanceToBeat(LiveSplitState state)
        {
            Settings = new ChanceToBeatSettings()
            {
                CutoffTime = GetPBFromState(state),
                CurrentState = state,
                ProbabilityText = "PB Chance"
            };
            InternalComponent = new InfoPercentComponent(null, null)
            {
                Percentage = 100.0 * SubTargetProbability(state, Settings.CutoffTime)
            };

            currentState = state;

            state.OnSplit += AdjustProbabilityEstimate;
            state.OnUndoSplit += AdjustProbabilityEstimate;
            state.OnReset += AdjustProbabilityEstimate;
            state.RunManuallyModified += AdjustProbabilityEstimate;
        }

        private void DrawBackground(Graphics g, LiveSplitState state, float width, float height)
        {
            if (Settings.BackgroundColor.A > 0
                || Settings.BackgroundGradient != GradientType.Plain
                && Settings.BackgroundColor2.A > 0)
            {
                var gradientBrush = new LinearGradientBrush(
                            new PointF(0, 0),
                            Settings.BackgroundGradient == GradientType.Horizontal
                            ? new PointF(width, 0)
                            : new PointF(0, height),
                            Settings.BackgroundColor,
                            Settings.BackgroundGradient == GradientType.Plain
                            ? Settings.BackgroundColor
                            : Settings.BackgroundColor2);
                g.FillRectangle(gradientBrush, 0, 0, width, height);
            }
        }

        private void PrepareDraw(LiveSplitState state)
        {
            InternalComponent.DisplayTwoRows = Settings.Display2Rows;

            InternalComponent.NameLabel.HasShadow = state.LayoutSettings.DropShadows;
            InternalComponent.ValueLabel.HasShadow = state.LayoutSettings.DropShadows;

            InternalComponent.NameLabel.ForeColor =
                Settings.OverrideTextColor ? Settings.TextColor : state.LayoutSettings.TextColor;
            InternalComponent.ValueLabel.ForeColor =
                Settings.OverrideTextColor ? Settings.TextColor : state.LayoutSettings.TextColor;
        }

        public void DrawHorizontal(Graphics g, LiveSplitState state, float height, Region clipRegion)
        {
            DrawBackground(g, state, HorizontalWidth, height);
            PrepareDraw(state);
            InternalComponent.DrawHorizontal(g, state, height, clipRegion);
        }

        public void DrawVertical(Graphics g, LiveSplitState state, float width, Region clipRegion)
        {
            DrawBackground(g, state, width, VerticalHeight);
            PrepareDraw(state);
            InternalComponent.DrawVertical(g, state, width, clipRegion);
        }

        public XmlNode GetSettings(XmlDocument document) => Settings.GetSettings(document);
        public void SetSettings(XmlNode settings) => Settings.SetSettings(settings);

        public Control GetSettingsControl(LayoutMode mode)
        {
            Settings.Mode = mode;
            return Settings;
        }

        public void Update(IInvalidator invalidator, LiveSplitState state, float width, float height, LayoutMode mode)
        {
            InternalComponent.InformationName = Settings.ProbabilityText;
            InternalComponent.LongestString = Settings.ProbabilityText;

            InternalComponent.Update(invalidator, state, width, height, mode);
        }

        public void Dispose()
        {
            currentState.OnSplit -= AdjustProbabilityEstimate;
            currentState.OnUndoSplit -= AdjustProbabilityEstimate;
            currentState.OnReset -= AdjustProbabilityEstimate;
            currentState.RunManuallyModified -= AdjustProbabilityEstimate;
        }

        public int GetSettingsHashCode() => Settings.GetSettingsHashCode();

        private TimeSpan? GetPBFromState(LiveSplitState state)
        {
            var attemptTimes = state.Run.AttemptHistory
                .Select(x => x.Time[state.CurrentTimingMethod]);
            TimeSpan? pb = null;

            foreach (var time in attemptTimes)
            {
                if (time != null)
                {
                    if ((pb == null) || (TimeSpan.Compare((TimeSpan)pb, (TimeSpan)time) == 1))
                    {
                        pb = time;
                    }
                }
            }

            return pb;
        }

        private double? SubTargetProbability(LiveSplitState state, TimeSpan? time)
        {
            if (time == null)
            {
                return null;
            }

            var fullHistory = GetSplitData(state);
            foreach (var splits in fullHistory)
            {
                if (splits.Count == 0)
                {
                    return null;
                }
            }

            var lastSplit = MostRecentSplit(state);
            var lastSplitTime = lastSplit.Value;
            var curSplitIndex = lastSplit.Key + 1;

            var numbOfSimulations = 20000;
            var numbOfSuccessfulAttempts = 0;
            var rng = new Random(0); // Consistent initial seed an evil hack to lower variation the runner sees.

            for (var i = 0; i < numbOfSimulations; i++)
            {
                if (SimulateAttempt(fullHistory, rng, curSplitIndex) < (time - lastSplitTime))
                {
                    numbOfSuccessfulAttempts++;
                }
            }

            var compProb = CompletionProbability(curSplitIndex);
            return (compProb * numbOfSuccessfulAttempts) / numbOfSimulations;
        }

        private KeyValuePair<int, TimeSpan> MostRecentSplit(LiveSplitState state)
        {
            var index = state.CurrentSplitIndex;

            while (index > 0)
            {
                var prevSplit = state.Run[index - 1].SplitTime[state.CurrentTimingMethod];
                if (prevSplit != null)
                {
                    return new KeyValuePair<int, TimeSpan>(index - 1, (TimeSpan)prevSplit);
                }
                index--;
            }

            return new KeyValuePair<int, TimeSpan>(-1, TimeSpan.Zero);
        }

        private IList<IList<TimeSpan>> GetSplitData(LiveSplitState state)
        {
            var method = state.CurrentTimingMethod;
            var history = state.Run.Select(x => (IList<TimeSpan>)(new List<TimeSpan>())).ToList();

            foreach (var attempt in state.Run.AttemptHistory)
            {
                var ind = attempt.Index;
                var ignoreNextHistory = false;
                foreach (var segment in state.Run)
                {
                    if (segment.SegmentHistory.TryGetValue(ind, out Time attemptHistory))
                    {
                        if (attemptHistory[method] == null)
                        {
                            ignoreNextHistory = true;
                        }
                        else if (!ignoreNextHistory)
                        {
                            history[state.Run.IndexOf(segment)].Add(attemptHistory[method].Value);
                        }
                        else
                        {
                            ignoreNextHistory = false;
                        }
                    }
                }
            }

            return history;
        }

        private TimeSpan SimulateAttempt(IList<IList<TimeSpan>> history, Random rng, int curSplit)
        {
            var time = TimeSpan.Zero;
            for (var i = curSplit; i < history.Count; ++i)
            {
                time += RandomlySelectSplit(history[i], rng);
            }

            return time;
        }

        // Picks a random split time, biased towards recent times (assuming the recent
        // times are later in the list). It's assumed the splits list is non-empty.
        //
        // The distribution of the index chosen is "truncated" geometric, with values
        // ranging from 0 to splits.Length - 1 and P(X = k + 1) = weight * P(X = k) in
        // the nonzero range. It turns out that if X is Uniform(0, 1), then
        // floor(log_w(1 - (1 - w^N)X)) has the desired distribution.
        private TimeSpan RandomlySelectSplit(IList<TimeSpan> splits, Random rng)
        {
            var roll = Math.Log(1 - (1 - Math.Pow(Weight, splits.Count)) * rng.NextDouble(), Weight);

            if (roll < 0.0)
            {
                roll = 0.0;
            }
            else if (roll >= (splits.Count - 1))
            {
                roll = splits.Count - 1;
            }
            return splits[splits.Count - 1 - (int)Math.Floor(roll)];
        }

        private double CompletionProbability(int currentSplit)
        {
            return 1.0;
        }

        private void AdjustProbabilityEstimate<T>(object sender, T e)
        {
            InternalComponent.Percentage =
                100.0 * SubTargetProbability((LiveSplitState)sender, Settings.CutoffTime);
        }
    }
}
