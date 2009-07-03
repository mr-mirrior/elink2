using System.Windows.Forms;

namespace Elink
{
    public partial class BriefReportEnergy : UserControl, IBriefReport
    {
        public BriefReportEnergy()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
        }
        public double DailyConsumption { set { if (value < 0 || !Is.ValidF(value)) lbDailyConsumption.Text = "N/A"; else lbDailyConsumption.Text = value.ToString("0.00"); } }
        public double EstForTheYear { set { if (value < 0 || !Is.ValidF(value)) lbEstForTheYear.Text = "N/A"; else lbEstForTheYear.Text = value.ToString("0.00"); } }
        public double TotalForTheYear { set { if (value < 0 || !Is.ValidF(value)) lbTotalForTheYear.Text = "N/A"; else lbTotalForTheYear.Text = value.ToString("0.00"); } }

        private void label7_Click(object sender, System.EventArgs e)
        {

        }
    }
}
