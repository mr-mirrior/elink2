using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class Sub2RightDDMonthPerYear : UserControl, IDisplayData
    {
        public Sub2RightDDMonthPerYear()
        {
            Global.GoMultiLanguage(); 
            InitializeComponent();
            cylinders = new RoundRectControl[] {
                jan, feb, mar, apr, may, jun,
                jul, aug, sep, oct, nov, dec
            };
            UpdateData();
            // In Monthly per year: scroll 3 years before and after the selected year.
            scrWeeks.Min = 0;
            scrWeeks.Max = 6;
        }
        float maxValue = 0.5f;
        public float MaxValue
        {
            get { return maxValue; }
            set { if (!Is.ValidF(value)) value = 0; maxValue = value; SetUnits(); }
        }
        void SetUnits()
        {
            double u = maxValue / 5;
            double pt = u;
            lb1.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb2.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb3.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb4.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb5.Text = string.Format("{0:0.000}", pt);
        }
        DateTime start;
        public string Unit { get { return lbUnit.Text; } set { lbUnit.Text = value; } }
        RoundRectControl[] cylinders;
        public void SetValue(int month, float value)
        {
            if (start == DateTime.MinValue)
                return;
            string strUnit = Unit;
            string tip;
            if (!Is.ValidF(value) ||
                    value < 0 ||
                    value > maxValue)
            {
                tip = "N/A";
                value = 0;
                cylinders[month].BackColor = eColor.NA;
                strUnit = "";
            }
            else
            {
                tip = value.ToString(/*"0.000"*/);
                cylinders[month].BackColor = eColor.MyBlue;
            }

            RoundRectControl rc = cylinders[month];
            //rc.Visible = false;
            int x, y;
            int w, h;
            float percent = value / maxValue;
            h = (int)Math.Round(percent * plBounds.Height);
            w = rc.Width;

            rc.Width = w;
            rc.Height = h;
            x = rc.Location.X;
            y = plBounds.Bounds.Bottom - rc.Height;
            rc.Location = new Point(x, y);
            //rc.Visible = true;
            string st = Global.GetString("s9001", "Consumption");
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            if (!st.Equals("Consumption"))
            {
                ci = new System.Globalization.CultureInfo("es-ES");
            }
            DateTime dt = new DateTime(start.Year, month + 1, 1);
            string dtString = string.Format("{0:00}/{1:00}", dt.Month, dt.Year % 100);
            tip = string.Format("{0:D} {2}: {1}{3}", dtString, tip, st,strUnit);
            toolTip1.SetToolTip(rc, tip);
        }
        public void UpdateData()
        {
            if (start == DateTime.MinValue)
                return;

            float max = 0;
            DateTime pointer = new DateTime(start.Year, 1, 1);
            ReadValueFunc read;
            float carbon = 1.0f;

            switch (mode)
            {
                case 0:
                    max = EnergyData.GlobalMaxMonthly;//EnergyDataNew.max_monthly_e;
                    read = EnergyData.QueryGlobalEnergyMonthly;//EnergyDataNew.QueryMonthlyEnergy;
                    Unit = "kWh";
                    break;
                case 1:
                    max = EnergyData.GlobalMaxCostMonthly;//EnergyDataNew.max_monthly_c;
                    read = EnergyData.QueryGlobalCostMonthly;//EnergyDataNew.QueryMonthlyCost;
                    Unit = Settings.I.currency;
                    break;
                case 2:
                    carbon = Settings.I.carbon;
                    max = EnergyData.GlobalMaxMonthly*carbon;//EnergyDataNew.max_monthly_c * carbon;
                    read = EnergyData.QueryGlobalEnergyMonthly;//EnergyDataNew.QueryMonthlyEnergy;
                    Unit = "kg.CO2";
                    break;
                default:
                    return;
            }

            panel1.Visible = false;

            MaxValue = max;
            float mx = -1;
            int idx = -1;
            for (int i = 0; i < 12; i++)
            {
                float value = carbon * read(pointer);
                SetValue(i, value);
                pointer = DT.NextMonth(pointer);
                if( value >= 0 && mx < value )
                {
                    mx = value;
                    idx = i;
                }
            }

            panel1.Visible = true;
            if (idx != -1)
                cylinders[idx].BackColor = Color.OrangeRed;
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void scrWeeks_Scrolled(object sender, EventArgs e)
        {
            //Calendar.I.SetMontlyPerYear(scrWeeks.Pos);
            keep_center = true;
            DateTime dt = new DateTime(centeryear.Year+(scrWeeks.Pos-3),1,1);
            Calendar.I.SetDay(dt);
        }

        int mode = 0;
        public void SetMode(int m)
        {
            if (m < 0 || m > 2)
                return;
            mode = m;
            UpdateData();
        }
        DateTime centeryear = DateTime.MinValue;
        bool keep_center = false;
        public void SetDay(DateTime d)
        {
            if (d.Year == start.Year)
                return;
            if (!this.Visible)
                return;
            if (centeryear == DateTime.MinValue)
                centeryear = d;
            if (!keep_center)
                centeryear = d;
            keep_center = false;

            start = d;// new DateTime(d.Year, 1, 1);
            scrWeeks.Pos = 3-(centeryear.Year - d.Year);

            lbDate.Text = d.Year.ToString();
            UpdateData();
        }
        private void LocateToday()
        {
            DateTime t = DateTime.Today;
            Calendar.I.SetDay(t);
        }
        private void Sub2RightDDMonthPerYear_Load(object sender, EventArgs e)
        {
            LocateToday();
        }

    }
}
