using LiveSplit.Model;
using LiveSplit.UI;
using LiveSplit.UI.Components;
using System.Drawing;
using System.Globalization;

namespace LiveSplit.ChanceToBeat
{
    public class InfoPercentComponent : InfoTextComponent
    {
        private double? percentage;
        public double? Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                if (value <= 0.0)
                {
                    percentage = 0.0;
                }
                else if (value >= 100.0)
                {
                    percentage = 100.0;
                }
                else
                {
                    percentage = value;
                }

                SetPercentageString();
            }
        }

        public InfoPercentComponent(string informationName)
            : base(informationName, null)
        {
        }

        public override void PrepareDraw(LiveSplitState state, LayoutMode mode)
        {
            ValueLabel.IsMonospaced = true;
            ValueLabel.Font = state.LayoutSettings.TimesFont;
            NameMeasureLabel.Font = state.LayoutSettings.TextFont;
            NameLabel.Font = state.LayoutSettings.TextFont;

            if (mode == LayoutMode.Vertical)
            {
                NameLabel.VerticalAlignment = StringAlignment.Center;
                ValueLabel.VerticalAlignment = StringAlignment.Center;
            }
            else
            {
                NameLabel.VerticalAlignment = StringAlignment.Near;
                ValueLabel.VerticalAlignment = StringAlignment.Far;
            }
        }

        private void SetPercentageString()
        {
            if (percentage == null)
            {
                InformationValue = "–";
            }
            else
            {
                InformationValue = percentage?.ToString("F2", CultureInfo.InvariantCulture) + "%";
            }
        }
    }
}
