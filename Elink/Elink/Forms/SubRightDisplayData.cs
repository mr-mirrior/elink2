using System;
using System.Windows.Forms;

namespace Elink
{
    public partial class DisplayData : UserControl
    {
        static DisplayData I = null;
        public static DisplayData Instance { get { return I; } }
        public DisplayData()
        {
            curve = curveWeek;
            I = this;
            Global.GoMultiLanguage();
            InitializeComponent();
            LoadPage();
            //costs.Unit = SubRightSettings.Instance.Currency;
            //carbon_ratio.Unit = "KG";
            //l24.Unit 
        }
        Sub2RightDDByWeek byWeek = new Sub2RightDDByWeek();
        Sub2RightDDByMonth byMonth = new Sub2RightDDByMonth();
        Sub2RightDDMonthPerYear monthlyPerYear = new Sub2RightDDMonthPerYear();
        Sub2RightDDLatest24 last24 = new Sub2RightDDLatest24();
        Sub2RightDDCurveWeek curveWeek = new Sub2RightDDCurveWeek();
        Sub2RightDDCurveMonth curveMonth = new Sub2RightDDCurveMonth();
        UserControl curve;

        UserControl page = null;
        private void LoadPage()
        {
            SwitchTo(byWeek);
            GoEnergy();
        }
        public void GoByWeek() { SwitchTo(byWeek); }
        public void GoByMonth() { SwitchTo(byMonth); }
        public void GoMonthlyByYear() { SwitchTo(monthlyPerYear); }
        public void GoLast24Hours() { SwitchTo(last24); }
        public void GoCurve() 
        {
            btnMonthly.Visible = true;
            btnWeekly.Visible = true;
            rndFrame.Visible = true;
            btnSmooth.Visible = true;

            btnEnergy.Visible = false;
            btnCarbon.Visible = false;
            btnCosts.Visible = false;
            label1.Visible = false;

            //plReports.BackColor = Color.White;
            SwitchTo(curve);
        }
        private void LeaveCurve()
        {
            btnMonthly.Visible = false;
            btnWeekly.Visible = false;
            rndFrame.Visible = false;
            btnSmooth.Visible = false;

            btnEnergy.Visible = true;
            btnCarbon.Visible = true;
            btnCosts.Visible = true;
            label1.Visible = true;
            //plReports.BackColor = Color.FromArgb(224, 224, 224);
        }
        IDisplayData Current { get { return page as IDisplayData; } }
        private void GoEnergy() 
        {
            Current.SetMode(0);
        }
        private void GoCosts() 
        {
            Current.SetMode(1);
        }
        private void GoCarbon() 
        {
            Current.SetMode(2);
        }
        private void GoWeekly() { SwitchTo(curveWeek); curve = curveWeek; }
        private void GoMonthly() { SwitchTo(curveMonth); curve = curveMonth; }
        private void SwitchTo(UserControl p)
        {
            if (page == p)
                return;
            UserControl dd = page;
            UserControl ddto = p;
            //string unit = "kWh";
            //float maxvalue = 0.5f;
            page = p;
            if (dd != null)
            {
                //unit = dd.Unit;
                //maxvalue = dd.MaxValue;
                plReports.Controls.Remove(dd);
                dd.Hide();
            }
            if( (dd == curveWeek || dd ==curveMonth) &&
                (ddto!=curveWeek && ddto!=curveMonth) )
            {
                LeaveCurve();
            }
            //((IDisplayData)ddto).SetMode(0);
            plReports.Controls.Add(ddto);
            ((IDisplayData)p).SetDay(Calendar.I.ThisDay);
            ddto.Visible = true;

            if( p != curveWeek && p != curveMonth )
                btnEnergy.PerformClick();

            //ddto.Unit = unit;
            //ddto.MaxValue = maxvalue;
        }

        private void btnEnergy_Click(object sender, EventArgs e)
        {
            GoEnergy();
        }

        private void btnCosts_Click(object sender, EventArgs e)
        {
            GoCosts();
        }

        private void btnCarbon_Click(object sender, EventArgs e)
        {
            GoCarbon();
        }
        private void btnWeekly_Click(object sender, EventArgs e)
        {
            GoWeekly();
        }
        private void btnMonthly_Click(object sender, EventArgs e)
        {
            GoMonthly();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //SwitchTo(l24);
            UpdateData();
        }
        public void UpdateData()
        {
            if (page != null)
                (page as IDisplayData).UpdateData();
        }

        private void btnSmooth_Click(object sender, EventArgs e)
        {
            curveMonth.SetMode(btnSmooth.IsDown?1:0);
            curveWeek.SetMode(btnSmooth.IsDown ? 1 : 0);
        }
//         public void SetWeek(int baseweek, int week)
//         {
//             //byWeek.Week1 = string.Format("{0}", start);
//             byWeek.SetWeek(baseweek, week);
//         }
//         public void SetWeekNumber2(int start)
//         {
//             byWeek.Week2 = string.Format("{0}", start);
//         }
//         public void SetMonth(int year, int month)
//         {
//             byMonth.SetMonth(year, month);
//         }
        public void SetDay(DateTime day)
        {
            Current.SetDay(day);
        }
//         public void SetMontlyPerYear(int year)
//         {
//             monthlyPerYear.SetMontlyPerYear(year);
//         }
    }
}
