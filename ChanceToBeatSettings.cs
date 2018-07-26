using LiveSplit.Model;
using LiveSplit.TimeFormatters;
using LiveSplit.UI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace LiveSplit.ChanceToBeat
{
    public partial class ChanceToBeatSettings : UserControl
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

        public LiveSplitState CurrentState { get; set; }
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
            }
        }

        public ChanceToBeatSettings()
        {
            InitializeComponent();
            
            TextColor = Color.FromArgb(255, 255, 255);
            OverrideTextColor = false;

            BackgroundColor = Color.Transparent;
            BackgroundColor2 = Color.Transparent;
            BackgroundGradient = GradientType.Plain;
            Display2Rows = false;

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

            try
            {
                CutoffTime = SettingsHelper.ParseTimeSpan(element["CutoffTime"]);
            }
            catch (FormatException)
            {
                CutoffTime = null;
            }
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", GetType().Assembly.GetName().Version) ^
                SettingsHelper.CreateSetting(document, parent, "TextColor", TextColor) ^
                SettingsHelper.CreateSetting(document, parent, "OverrideTextColor", OverrideTextColor) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor2", BackgroundColor2) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundGradient", BackgroundGradient) ^
                SettingsHelper.CreateSetting(document, parent, "Display2Rows", Display2Rows) ^
                SettingsHelper.CreateSetting(document, parent, "CutoffTime", CutoffTime) ^
                SettingsHelper.CreateSetting(document, parent, "ProbabilityText", ProbabilityText);
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
    }
}