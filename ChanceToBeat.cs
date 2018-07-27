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

        public ChanceToBeat(LiveSplitState state)
        {
            currentState = state;

            Settings = new ChanceToBeatSettings()
            {
                CurrentState = state,
                ProbabilityText = "PB Chance"
            };
            InternalComponent = new InfoPercentComponent(null, null)
            {
                Percentage = 100.0 * SubTargetProbability(Settings.CutoffTime)
            };

            state.OnSplit += AdjustProbabilityEstimate;
            state.OnUndoSplit += AdjustProbabilityEstimate;
            state.OnReset += AdjustProbabilityEstimate;
            state.RunManuallyModified += AdjustProbabilityEstimate;

            Settings.CutoffChanged += AdjustProbabilityEstimate;
            Settings.ResetChancesChanged += AdjustProbabilityEstimate;
            Settings.WeightChanged += AdjustProbabilityEstimate;
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                currentState.OnSplit -= AdjustProbabilityEstimate;
                currentState.OnUndoSplit -= AdjustProbabilityEstimate;
                currentState.OnReset -= AdjustProbabilityEstimate;
                currentState.RunManuallyModified -= AdjustProbabilityEstimate;
            }
        }

        private double? SubTargetProbability(TimeSpan? time)
        {
            if (time == null)
            {
                return null;
            }

            var fullHistory = GetSplitData(currentState);
            foreach (var splits in fullHistory)
            {
                if (splits.Count == 0)
                {
                    return null;
                }
            }

            var lastSplit = MostRecentSplit(currentState);
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

            var compProb = CompletionChance(curSplitIndex);
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
            var roll = 0.0;

            if (Settings.Weight == 0.0)
            {
                roll = 0.0;
            }
            else if (Settings.Weight == 1.0)
            {
                roll = rng.Next(0, splits.Count);
            }
            else
            {
                roll = Math.Log(1 - (1 - Math.Pow(Settings.Weight, splits.Count)) * rng.NextDouble(), Settings.Weight);
            }

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

        private double CompletionChance(int currentSplit)
        {
            var completionChance = 1.0;

            foreach (var resetChance in Settings.ChanceOfResetBySegment().Skip(currentSplit))
            {
                completionChance *= (100.0 - resetChance) / 100.0;
            }

            return completionChance;
        }

        private void AdjustProbabilityEstimate<T>(object sender, T e)
        {
            InternalComponent.Percentage =
                100.0 * SubTargetProbability(Settings.CutoffTime);
        }
    }
}
