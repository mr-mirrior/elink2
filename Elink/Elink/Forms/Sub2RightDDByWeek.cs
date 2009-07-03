using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class Sub2RightDDByWeek : UserControl, IDisplayData
    {
        public Sub2RightDDByWeek()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            week1 = new RoundRectControl[] { 
                su1, mo1, tu1, we1, th1, fr1, sa1};
            week2 = new RoundRectControl[] { 
                su2, mo2, tu2, we2, th2, fr2, sa2};
            UpdateData();
            // In Daily by week: scroll all weeks within the selected month.
            // 2 weeks per screen
            scrWeeks.Min = 0;
            scrWeeks.Max = 4;
        }
        DateTime start;
        public DateTime StartFrom { get { return start; } set { start = value; } }
        public string Unit { get { return lbUnit.Text; } set { lbUnit.Text = value; } }
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

        RoundRectControl[] week1;
        RoundRectControl[] week2;
        /// <summary>
        /// 设置柱状图的值
        /// </summary>
        public RoundRectControl SetValue(int day, float value)
        {
            RoundRectControl[] reference = (day/7==0)?week1:week2;
            RoundRectControl rc = reference[day%7];
            if (now == DateTime.MinValue)
                return rc;
            string strUnit = Unit;
            string tip;
            if (!Is.ValidF(value) ||
                    value < 0 ||
                    value > maxValue)
            {
                tip = "N/A";
                value = 0;
                rc.BackColor = eColor.NA;
                strUnit = "";
            }
            else
            {
                tip = value.ToString(/*"0.000"*/);
                rc.BackColor = eColor.MyBlue;
            }

            rc.Visible = false;
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
            rc.Visible = true;
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            DateTime dt = DT.WeekFloor(now) 
                + new TimeSpan(day, 0, 0, 0);
            string dtString = string.Format("{0:00}/{1:00}/{2:00}", dt.Day, dt.Month, dt.Year % 100);
            tip = string.Format("{0:D} {2}: {1}{3}", dtString, tip, Global.GetString("s9001", "Consumption"),strUnit);
            toolTip1.SetToolTip(rc, tip);

            return rc;
        }
        public void UpdateData()
        {
            if (now == DateTime.MinValue)
                return;

            float max = 0;
            DateTime pointer = DT.DateFloor(DT.WeekFloor(now));
            ReadValueFunc read;
            float carbon = 1.0f;

            int w1 = DT.WhichWeekOfYear(pointer);
            int w2 = DT.WhichWeekOfYear(DT.NextWeek(pointer));
            lbWeek1.Text = w1.ToString();
            lbWeek2.Text = w2.ToString();

            switch (mode)
            {
                case 0:
                    max = /*EnergyDataNew.max_daily_e;*/EnergyData.GlobalMaxDaily;
                    read = /*EnergyDataNew.QueryDailyEnergy;*/EnergyData.QueryGlobalEnergyDaily;
                    Unit = "kWh";
                    break;
                case 1:
                    max = EnergyData.GlobalMaxCostDaily;//EnergyDataNew.max_daily_c;
                    read = EnergyData.QueryGlobalCostDaily;//EnergyDataNew.QueryDailyCost;
                    Unit = Settings.I.currency;
                    break;
                case 2:
                    carbon = Settings.I.carbon;
                    max = /*EnergyDataNew.max_daily_e*/ EnergyData.GlobalMaxDaily * carbon;
                    read = EnergyData.QueryGlobalEnergyDaily; //EnergyDataNew.QueryDailyEnergy;
                    Unit = "kg.CO2";
                    break;
                default:
                    return;
            }

            panel1.Visible = false;
            //HideAll();

            MaxValue = max;
            float mx = -1;
            RoundRectControl m = null;
            for (int i = 0; i < 14; i++)
            {
                float value = carbon * read(pointer);
                RoundRectControl rc = SetValue(i, value);
                pointer += DT.OneDay;

                if( value >= 0 )
                {
                    if (mx < value)
                    {
                        mx = value;
                        m = rc;
                    }
                }
            }
            if (m != null)
                m.BackColor = Color.OrangeRed;

            panel1.Visible = true;
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
//         int weekNum = 0;
//         int baseWeek = 0;
//         public void SetWeek(int baseweek, int week)
//         {
//             weekNum = week;
//             lbWeek1.Text = week.ToString();
//             lbWeek2.Text = Calendar.I.NextWeek(week).ToString();
//             baseWeek = baseweek;
//             scrWeeks.Pos = baseweek;
//         }
        private void scrWeeks_Scrolled(object sender, EventArgs e)
        {
            //DateTime firstday = DT.MonthFloor(start);
            //int week = DT.WhichWeekOfYear(firstday);
            //weekoffset = scrWeeks.Pos;
            //DateTime dt = now + new TimeSpan(weekoffset*7, 0, 0, 0);
            DateTime dt = DT.MonthFloor(now) + new TimeSpan(scrWeeks.Pos*7,0,0,0);
            if (dt.Month != now.Month)
                return;
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
        int weekoffset = 0;
        DateTime now;
        public void SetDay(DateTime d)
        {
            if (!this.Visible)
                return;
            now = d;
            start = DT.MonthFloor(d);
            weekoffset = DT.WeekDistance(start, d);
            scrWeeks.Pos = weekoffset;
            lbDate.Text = now.ToString("Y");
            UpdateData();
        }
        private void LocateToday()
        {
            DateTime t = DateTime.Today;
//             float x = EnergyData.QueryGlobalEnergyDaily(t);
//             if (!Is.ValidF(x) || x < 0)
//                 t = EnergyData.QueryGlobalLastDayValid();
//             if (t == DateTime.MinValue)
//                 t = DateTime.Today;
            //SetDay(t);
            Calendar.I.SetDay(DT.WeekFloor(t)-DT.OneWeek);
        }
        private void Sub2RightDDByWeek_Load(object sender, EventArgs e)
        {
            LocateToday();
        }

    }
}
