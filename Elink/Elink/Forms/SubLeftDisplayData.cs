using System;
using System.Windows.Forms;

namespace Elink
{
    public partial class SubLeftDisplayData : UserControl
    {
        PercentageSmall perc = new PercentageSmall();
        public SubLeftDisplayData()
        {
            JumpWindows.GoTime += OnGoTime;
            Global.GoMultiLanguage();
            InitializeComponent();
            plPercentSmall.Controls.Add(perc);
        }
        private void OnGoTime(object sender, EventArgs e)
        {
            lbDay.PerformClick();
            JumpWindows.TimeEvent f = (JumpWindows.TimeEvent)e;
            Calendar.I.SetDay(f.time);
        }
        private void lbWeek_Click(object sender, EventArgs e)
        {
            //Calendar.I.ShowYearOnly();
            Calendar.I.ShowYearMonthAndWeeks();
            DisplayData.Instance.GoByWeek();
        }

        private void lbMonth_Click(object sender, EventArgs e)
        {
            Calendar.I.ShowYearMonthesOnly();
            DisplayData.Instance.GoByMonth();
        }

        private void lbYear_Click(object sender, EventArgs e)
        {
            Calendar.I.ShowYearOnly();
            DisplayData.Instance.GoMonthlyByYear();
        }

        private void lbDay_Click(object sender, EventArgs e)
        {
            Calendar.I.ShowAll();
            DisplayData.Instance.GoLast24Hours();
        }
        private void btnCurve_Click(object sender, EventArgs e)
        {
            Calendar.I.ShowYearOnly();
            DisplayData.Instance.GoCurve();
            DisplayData.Instance.UpdateData();
        }

        public void OnLoad()
        {
            if (lbWeek.IsDown)
                lbWeek_Click(this, null);
            if (lbMonth.IsDown)
                lbMonth_Click(this, null);
            if (lbYear.IsDown)
                lbYear_Click(this, null);
            if (lbDay.IsDown)
                lbDay_Click(this, null);
            if (lbCurve.IsDown)
                btnCurve_Click(this, null);
        }

    }
}
