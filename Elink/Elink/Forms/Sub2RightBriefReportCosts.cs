using System;
using System.Windows.Forms;

namespace Elink
{
    public partial class BriefReportCosts : UserControl, IBriefReport
    {
        public BriefReportCosts()
        {
            Global.GoMultiLanguage();
            InitializeComponent();

            SubRightSettings.Instance.OnPriceChange += OnPriceChange;
        }
        public void RefreshCurrency()
        {
            lbC1.Text = Settings.I.currency;
            lbC2.Text = Settings.I.currency;
            lbC3.Text = Settings.I.currency;
        }
        private void OnPriceChange(object sender, EventArgs e)
        {
            RefreshCurrency();
        }
        public double DailyConsumption { set { if (value < 0 || !Is.ValidF(value)) lbDailyConsumption.Text = "N/A"; else lbDailyConsumption.Text = value.ToString("0.00"); } }
        public double EstForTheYear { set { if (value < 0 || !Is.ValidF(value)) lbEstForTheYear.Text = "N/A"; else lbEstForTheYear.Text = value.ToString("0.00"); } }
        public double TotalForTheYear { set { if (value < 0 || !Is.ValidF(value)) lbTotalForTheYear.Text = "N/A"; else lbTotalForTheYear.Text = value.ToString("0.00"); } }
    }
}
