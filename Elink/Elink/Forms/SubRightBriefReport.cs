using System;
using System.Windows.Forms;

namespace Elink
{
    public partial class BriefReport : UserControl
    {
        public BriefReport()
        {
            ENERGY = energy;
            COSTS = costs;
            CARBON = carbon;
            Global.GoMultiLanguage();
            InitializeComponent();
            energy.Visible = false;
            costs.Visible = false;
            carbon.Visible = false;
            plReports.Controls.Add(energy);
            plReports.Controls.Add(costs);
            plReports.Controls.Add(carbon);
            LoadPage();
        }
        BriefReportEnergy energy = new BriefReportEnergy();
        BriefReportCosts costs = new BriefReportCosts();
        BriefReportCarbon carbon = new BriefReportCarbon();
        public IBriefReport ENERGY;
        public IBriefReport COSTS;
        public IBriefReport CARBON;
        UserControl page = null;
        private void LoadPage()
        {
            SwitchTo(energy);
        }
        private void SwitchTo(UserControl p)
        {
            if (page == p)
                return;

            if (page != null)
            {
                //plReports.Controls.Remove(page);
                page.Hide();
            }
            //plReports.Controls.Add(p);
            p.Visible = true;
            page = p;
        }

        private void btnEnergy_Click(object sender, EventArgs e)
        {
            SwitchTo(energy);
        }

        private void btnCosts_Click(object sender, EventArgs e)
        {
            SwitchTo(costs);
        }

        private void btnCarbon_Click(object sender, EventArgs e)
        {
            SwitchTo(carbon);
        }
        public void OnCurrencyChange(object sender, EventArgs e)
        {
            costs.RefreshCurrency();
        }
    }
}
