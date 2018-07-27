using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.ChanceToBeat
{
    public partial class ChanceToBeatSettings : UserControl, IDisposable
    {
        public Color TextColor { get; set; }
        public bool OverrideTextColor { get; set; }

        public Color BackgroundColor { get; set; }
        public Color BackgroundColor2 { get; set; }
        public GradientType BackgroundGradient { get; set; }
        public string GradientString
        {
            get
            {
                return BackgroundGradient.ToString();
            }
            set
            {
                BackgroundGradient = (GradientType)Enum.Parse(typeof(GradientType), value);
            }
        }

        private LiveSplitState state;
        public LiveSplitState CurrentState
        {
            get
            {
                return state;
            }
            set
            {
                dataGridSplits.Rows.Clear();
                if (state != null)
                {
                    state.OnReset -= UpdatePb;
                    state.RunManuallyModified -= UpdateResetChanceNames;
                }
                state = value;
                if (state != null)
                {
                    state.OnReset += UpdatePb;
                    state.RunManuallyModified += UpdateResetChanceNames;
                    UpdateResetChanceNames(state, EventArgs.Empty);
                }
                PersonalBest = GetPbFromState(state);
            }
        }
        public bool Display2Rows { get; set; }

        public LayoutMode Mode { get; set; }

        public string ProbabilityText { get; set; }
        private TimeSpan? cutoffTime;
        public TimeSpan? CutoffTime
        {
            get
            {
                return cutoffTime;
            }
            set
            {
                cutoffTime = value;
                
                if (value == null)
                {
                    txtBoxTimeCutoff.Text = "";
                }
                else
                {
                    var formatter = new RegularTimeFormatter();
                    txtBoxTimeCutoff.Text = formatter.Format(value);
                }

                OnCutoffChanged(EventArgs.Empty);
            }
        }

        private TimeSpan? pb;
        public TimeSpan? PersonalBest
        {
            get
            {
                return pb;
            }
            set
            {
                pb = value;
                if (chkUsePb.Checked)
                {
                    CutoffTime = pb;
                }
            }
        }

        private double weight;
        public double Weight
        {
            get
            {
                return weight;
            }
            set
            {
                if (value <= 0.0)
                {
                    weight = 0.0;
                }
                else if (value >= 1.0)
                {
                    weight = 1.0;
                }
                else
                {
                    weight = value;
                }
                trackBarWeight.Value = Convert.ToInt32(trackBarWeight.Maximum * weight);
                toolTipWeight.SetToolTip(trackBarWeight, weight.ToString("F3", CultureInfo.InvariantCulture));
                WeightChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler CutoffChanged;
        public event EventHandler ResetChancesChanged;
        public event EventHandler WeightChanged;

        public ChanceToBeatSettings()
        {
            InitializeComponent();
            
            TextColor = Color.FromArgb(255, 255, 255);
            OverrideTextColor = false;

            BackgroundColor = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;
            Display2Rows = false;
            Weight = 0.75; // Weight used for average splits.

            chkOverrideTextColor.DataBindings.Add("Checked", this, "OverrideTextColor", false,
                DataSourceUpdateMode.OnPropertyChanged);
            btnTextColor.DataBindings.Add("BackColor", this, "TextColor", false,
                DataSourceUpdateMode.OnPropertyChanged);

            cmbGradientType.DataBindings.Add("SelectedItem", this, "GradientString", false,
                DataSourceUpdateMode.OnPropertyChanged);
            btnColor1.DataBindings.Add("BackColor", this, "BackgroundColor", false,
                DataSourceUpdateMode.OnPropertyChanged);
            btnColor2.DataBindings.Add("BackColor", this, "BackgroundColor2", false,
                DataSourceUpdateMode.OnPropertyChanged);

            txtBoxText.DataBindings.Add("Text", this, "ProbabilityText", false,
                DataSourceUpdateMode.OnPropertyChanged);

            dataGridSplits.CellValueChanged += (s, e) => ResetChancesChanged?.Invoke(this, e);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public void SetSettings(XmlNode settings)
        {
            var element = (XmlElement)settings;

            TextColor = SettingsHelper.ParseColor(element["TextColor"]);
            OverrideTextColor = SettingsHelper.ParseBool(element["OverrideTextColor"]);
            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"]);
            BackgroundColor2 = SettingsHelper.ParseColor(element["BackgroundColor2"]);
            GradientString = SettingsHelper.ParseString(element["BackgroundGradient"]);
            Display2Rows = SettingsHelper.ParseBool(element["Display2Rows"], false);
            ProbabilityText = SettingsHelper.ParseString(element["ProbabilityText"]);
            Weight = SettingsHelper.ParseDouble(element["Weight"]);

            try
            {
                CutoffTime = SettingsHelper.ParseTimeSpan(element["CutoffTime"]);
            }
            catch (FormatException)
            {
                CutoffTime = null;
            }

            var timesTextList = element["ResetChances"].InnerText.Split();

            if (timesTextList.Length == dataGridSplits.Rows.Count)
            {
                for (var i = 0; i < timesTextList.Length; i++)
                {
                    if (double.TryParse(timesTextList[i], out double result) && ((result <= 0.0) || (result > 100.0)))
                    {
                        dataGridSplits[1, i].Value = "";
                    }
                    else
                    {
                        dataGridSplits[1, i].Value = timesTextList[i];
                    }
                }
            }
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            var resetChances = ChanceOfResetBySegment();

            if (document != null)
            {
                var element = document.CreateElement("ResetChances");
                element.InnerText = string.Join(" ", resetChances.Select(x => x.ToString()));
                parent.AppendChild(element);
            }

            return SettingsHelper.CreateSetting(document, parent, "Version", GetType().Assembly.GetName().Version) ^
                SettingsHelper.CreateSetting(document, parent, "TextColor", TextColor) ^
                SettingsHelper.CreateSetting(document, parent, "OverrideTextColor", OverrideTextColor) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
                SettingsHelper.CreateSetting(document, parent, "Display2Rows", Display2Rows) ^
                SettingsHelper.CreateSetting(document, parent, "CutoffTime", CutoffTime) ^
                SettingsHelper.CreateSetting(document, parent, "ProbabilityText", ProbabilityText) ^
                SettingsHelper.CreateSetting(document, parent, "Weight", Weight) ^
                resetChances.GetHashCode();
        }

        private void TimeProbabilitySettings_Load(object sender, EventArgs e)
        {
            chkOverrideTextColor_CheckedChanged(null, null);

            chkTwoRows.DataBindings.Clear();
            if (Mode == LayoutMode.Horizontal)
            {
                chkTwoRows.Enabled = false;
                chkTwoRows.Checked = true;
            }
            else
            {
                chkTwoRows.Enabled = true;
                chkTwoRows.DataBindings.Add("Checked", this, "Display2Rows", false,
                    DataSourceUpdateMode.OnPropertyChanged);
            }
        }

        public IList<double> ChanceOfResetBySegment()
        {
            var chances = new List<double>();

            for (var i = 0; i < dataGridSplits.Rows.Count; i++)
            {
                string chanceText = (string)dataGridSplits[1, i].Value;

                if (string.IsNullOrEmpty(chanceText))
                {
                    chances.Add(0.0);
                }
                else
                {
                    var success = double.TryParse(chanceText, out double chance);
                    if (success)
                    {
                        chance = Math.Min(chance, 100.0);
                        chance = Math.Max(chance, 0.0);
                        chances.Add(chance);
                    }
                    else
                    {
                        chances.Add(0.0);
                    }
                }
            }

            return chances;
        }

        private TimeSpan? GetPbFromState(LiveSplitState state)
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

        private void cmbGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnColor1.Visible = cmbGradientType.SelectedItem.ToString() != "Plain";
            btnColor2.DataBindings.Clear();
            btnColor2.DataBindings.Add("BackColor", this,
                btnColor1.Visible ? "BackgroundColor2" : "BackgroundColor",
                false, DataSourceUpdateMode.OnPropertyChanged);
            GradientString = cmbGradientType.SelectedItem.ToString();
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }

        private void chkOverrideTextColor_CheckedChanged(object sender, EventArgs e)
        {
            lblTextColor.Enabled = chkOverrideTextColor.Checked;
            btnTextColor.Enabled = chkOverrideTextColor.Checked;
        }

        private void txtBoxTimeCutoff_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxTimeCutoff.Text))
                return;

            try
            {
                TimeSpanParser.Parse(txtBoxTimeCutoff.Text);
            }
            catch
            {
                e.Cancel = true;
                txtBoxTimeCutoff.Select(0, txtBoxTimeCutoff.Text.Length);
            }
        }

        private void txtBoxTimeCutoff_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxTimeCutoff.Text))
            {
                CutoffTime = null;
            }
            else
            {
                try
                {
                    CutoffTime = TimeSpanParser.Parse(txtBoxTimeCutoff.Text);
                }
                catch
                {
                }
            }
        }

        private void UpdateResetChanceNames(object sender, EventArgs e)
        {
            dataGridSplits.Rows.Clear();
            var segmentNames = ((LiveSplitState)sender).Run.Select(x => x.Name).ToList();
            
            foreach (var name in segmentNames)
            {
                dataGridSplits.Rows.Add(name, "");
            }
        }

        private void dataGridSplits_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var headerText = dataGridSplits.Columns[e.ColumnIndex].HeaderText;

            if (!headerText.Equals("Reset Chance"))
            {
                return;
            }

            var chanceText = e.FormattedValue.ToString();

            if (string.IsNullOrEmpty(chanceText))
            {
                return;
            }

            var success = double.TryParse(chanceText, out double chance);

            if (!success || (chance < 0.0) || (chance > 100.0))
            {
                e.Cancel = true;
            }
        }

        private void trackBarWeight_Scroll(object sender, EventArgs e)
        {
            Weight = ((double)trackBarWeight.Value) / trackBarWeight.Maximum;
        }

        protected virtual void OnCutoffChanged(EventArgs e)
        {
            CutoffChanged?.Invoke(this, e);
        }

        private void chkUsePb_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsePb.Checked)
            {
                CutoffTime = PersonalBest;
            }

            lblCustomCutoff.Enabled = !chkUsePb.Checked;
            txtBoxTimeCutoff.Enabled = !chkUsePb.Checked;
        }

        private void UpdatePb(object sender, TimerPhase e)
        {
            PersonalBest = GetPbFromState(CurrentState);
        }
    }
}