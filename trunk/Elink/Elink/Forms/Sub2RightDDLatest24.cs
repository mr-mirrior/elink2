using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public delegate float ReadValueFunc(DateTime param);

    public partial class Sub2RightDDLatest24 : UserControl, IDisplayData
    {
        public Sub2RightDDLatest24()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            //CreateHours(24);
            //UpdateData();
            // In 24 Hours: scroll within the selected week.
            scrWeeks.Min = 0;
            scrWeeks.Max = 6;
        }
        void CreateHours(int hours)
        {
            int left = cyStart.Left;
            int right = cyStop.Right;

            const int HOURS_A_DAY = 24;
            float cyWidth = 0;
            int cyInterval = 5;
            // (w1+w2)*24 = right - left;
            cyWidth = (float)(right - left) / HOURS_A_DAY - cyInterval;
            //System.Diagnostics.Debug.Print("width:{0:0.0}, interval:{1}", cyWidth, cyInterval);
            if (cylinders != null)
                for (int i = 0; i < HOURS_A_DAY; i++)
                {
                    cylinders[i].Dispose();
                }
            cylinders = new RoundRectControl[HOURS_A_DAY];
            for (int i = 0; i < HOURS_A_DAY; i++ )
            {
                float x = (float)Math.Round(left + i * (cyWidth + cyInterval));
                float y = 10;

                cylinders[i] = (RoundRectControl)cyStart.Clone();
                cylinders[i].Width = (int)Math.Round(cyWidth);
                cylinders[i].Location = new Point((int)x, (int)y);

                panel1.Controls.Add(cylinders[i]);
                cylinders[i].BringToFront();
                cylinders[i].GradientColor = Color.FromArgb(224, 224, 224);
                cylinders[i].Visible = true;
            }
        }
        RoundRectControl[] cylinders;
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

        public void SetValue(int hour, float value)
        {
            RoundRectControl rc = cylinders[hour];
            string strUnit=Unit;
            string tip;
            if (!Is.ValidF(value) || 
                value < 0 || value > maxValue)
            {
                //rc.Visible = false;
                //return;
                value = 0;
                tip = "N/A";
                rc.BackColor = eColor.NA;
                strUnit = "";
            }
            else
            {
                tip = value.ToString(/*"0.000"*/);
                rc.BackColor = eColor.MyBlue;
            }

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

            DateTime dt = DT.DateFloor(now);
            
            TimeSpan ts = new TimeSpan(hour, 0, 0);
            dt += ts;
            //dt -= ts;
            //string today = "Today";
            //if (DateTime.Now.Day != dt.Day)
            //    today = dt.ToShortDateString();
            //string s = string.Format("{2} Value={3}, Duration={0:00}:00 ~ {1:00}:00", 
            //    dt.Hour, (dt.Hour+1)%24, today, value);
            tip = string.Format("{0:D} {2}: {1}{3}", dt.ToString("t"), tip, Global.GetString("s9001", "Consumption"),strUnit);
            toolTip1.SetToolTip(rc, tip);

            //rc.Visible = true;
        }
        private void HideAll()
        {
            foreach (RoundRectControl rc in cylinders)
            {
                rc.Visible = false;
            }
        }
        int mode = 0;
        public void SetMode(int m)
        {
            if (m < 0 || m > 2)
                return;
            mode = m;
            UpdateData();
        }
        public void UpdateData()
        {
            float max = 0;
            DateTime pointer = DT.DateFloor(now);
            ReadValueFunc read;
            float carbon = 1.0f;

            switch (mode)
            {
                case 0:
                    max = /*EnergyDataNew.max_e;*/EnergyData.GlobalMax;
                    read = /*EnergyDataNew.QueryHourlyEnergy;*/EnergyData.QueryGlobalEnergyHourly;
                    Unit = "kWh";
                    break;
                case 1:
                    max = EnergyData.GlobalMaxCost;//EnergyDataNew.max_c;
                    read = EnergyData.QueryGlobalCostHourly;//EnergyDataNew.QueryHourlyCost;
                    Unit = Settings.I.currency;
                    break;
                case 2:
                    carbon = Settings.I.carbon;
                    max = EnergyData.GlobalMax * carbon;//EnergyDataNew.max_e * carbon;
                    read = EnergyData.QueryGlobalEnergyHourly;//EnergyDataNew.QueryHourlyEnergy;
                    Unit = "kg.CO2";
                    break;
                default:
                    return;
            }

            panel1.Visible = false;
            //HideAll();

            MaxValue = max;
            float mx = -1;
            int idx = -1;
            for (int i = 0; i < 24; i++)
            {
                float value = carbon*read(pointer);
                SetValue(i, value);
                pointer += DT.OneHour;

                if (value >= 0)
                {
                    if( mx < value )
                    {
                        mx = value;
                        idx = i;
                    }
                }
            }

            if( idx != -1 )
                cylinders[idx].BackColor = Color.OrangeRed;

            panel1.Visible = true;
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        private void LocateToday()
        {
            DateTime lastday = DateTime.Today;
//             float todayenergy = EnergyData.QueryGlobalEnergyDaily(lastday);
//             if (!Is.ValidF(todayenergy))
//                 lastday = EnergyData.QueryGlobalLastDayValid();
            Calendar.I.SetDay(lastday);
        }
        private void Sub2RightDDLatest24_Load(object sender, EventArgs e)
        {
            CreateHours(24);
            LocateToday();
        }

        private void scrWeeks_Scrolled(object sender, EventArgs e)
        {
            //Calendar.I.SetDay(scrWeeks.Pos);
            //int x = (int)start.DayOfWeek;
            //DateTime t = start + new TimeSpan(scrWeeks.Pos-x, 0, 0, 0);
            DateTime t = start + new TimeSpan(scrWeeks.Pos, 0, 0, 0);
            Calendar.I.SetDay(t);
        }
        private void RefreshDay()
        {
            //CreateHours(24);
            //scrWeeks.Pos = (int)start.DayOfWeek;
            lbDate.Text = now.ToString("D");
            UpdateData();
        }
        DateTime now;
        public void SetDay(DateTime day)
        {
            if (!this.Visible)
                return;
            now = day;
            start = DT.WeekFloor(day);
            
            scrWeeks.Pos = (int)day.DayOfWeek;
            RefreshDay();
        }
    }
}
