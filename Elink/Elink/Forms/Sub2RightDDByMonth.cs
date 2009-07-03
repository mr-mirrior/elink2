using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class Sub2RightDDByMonth : UserControl, IDisplayData
    {
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
        public Sub2RightDDByMonth()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            month1 = DateTime.MinValue;
            SetUnits();
            // In Daily by month: scroll all months within the selected year.
            // 2 months per screen
            scrWeeks.Min = 0;
            scrWeeks.Max = 10;
        }
        float maxValue = 0.5f;
        public float MaxValue 
        { 
            get { return maxValue; }
            set { if (!Is.ValidF(value)) value = 0; maxValue = value; SetUnits(); } 
        }

        DateTime month1;
        public string Unit { get { return lbUnit.Text; } set { lbUnit.Text = value; } }
        List<RoundRectControl> dg1 = new List<RoundRectControl>();
        List<RoundRectControl> dg2 = new List<RoundRectControl>();
        void Reload()
        {
            int days1 = DT.DaysInMonth(month1);
            int days2 = DT.DaysInMonth(DT.NextMonth(month1));
            for (int i = 0; i < 31; i++ )
            {
                if (i < days1)
                    dg1[i].Visible = true;
                else
                    dg1[i].Visible = false;

                if (i < days2)
                    dg2[i].Visible = true;
                else
                    dg2[i].Visible = false;
                dg1[i].GradientColor = Color.FromArgb(224, 224, 224);
                dg2[i].GradientColor = Color.FromArgb(224, 224, 224);
            }
        }
        void CreateCylinders()
        {
//             foreach (RoundRectControl rc in dg1)
//             {
//                 rc.Dispose();
//             }
//             foreach (RoundRectControl rc in dg2)
//             {
//                 rc.Dispose();
//             }
//             dg1.Clear();
//             dg2.Clear();
            
            int blank = 1;
            int days1 = 31;//DT.DaysInMonth(month1);
            int days2 = 31;//DT.DaysInMonth(DT.NextMonth(month1));
            for(int i=0; i<days1; i++ )
            {
                int x = 0, y = 0;
                RoundRectControl rc = (RoundRectControl)sample1.Clone();
                //rc.SuspendLayout();
                x = rc.Location.X + i * (rc.Width + blank) +plBounds.Location.X;
                y = rc.Location.Y + plBounds.Location.Y;
                //rc.Visible = true;
                rc.Location = new Point(x, y);
                dg1.Add(rc);
                //rc.Parent = panel1;
                panel1.Controls.Add(rc);
                panel1.Controls.SetChildIndex(rc, 0);
                //rc.ResumeLayout();
            }
            for (int i = 0; i < days2; i++ )
            {
                int x = 0, y = 0;
                RoundRectControl rc = (RoundRectControl)sample2.Clone();
                x = rc.Location.X + i * (rc.Width + blank) +plBounds.Location.X;
                y = rc.Location.Y +plBounds.Location.Y;
                //rc.Visible = true;
                rc.Location = new Point(x, y);
                dg2.Add(rc);
                //rc.Parent = panel1;
                panel1.Controls.Add(rc);
                panel1.Controls.SetChildIndex(rc, 0);
            }
            foreach (Control c in dg1)
            {
                c.Visible = true;
            }
            foreach (Control c in dg2)
            {
                c.Visible = true;
            }
            //UpdateData();
            //wc.Dispose();
        }
        RoundRectControl SetValue(int month, int day, float value)
        {
            List<RoundRectControl> reference = (month == 1) ? dg2 : dg1;
            RoundRectControl rc = reference[day];
            string tip = null;
            //rc.Visible = false;
            string strUnit = Unit;
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
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            DateTime dt = (month == 0) ? month1 : DT.NextMonth(month1);
            dt = new DateTime(dt.Year, dt.Month, day+1);
            string dtString = string.Format("{0:00}/{1:00}/{2:00}", dt.Day, dt.Month, dt.Year % 100);
            tip = string.Format("{0:D} {2}: {1}{3}", dtString, tip, Global.GetString("s9001", "Consumption"),strUnit);
            toolTip1.SetToolTip(rc, tip);
//             Calendar.Months mon = (Calendar.Months)(month + 5);
//             weekday++;
//             string s = string.Format("{0}{1} {2}, {3:0.00}{4}", weekday, Calendar.DaySuffix(weekday), mon.ToString(), value, Unit);
//             toolTip1.SetToolTip(rc, s);

            //rc.Visible = true;

            return rc;
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
            DateTime pointer = DT.DateFloor(month1);
            pointer = DT.MonthFloor(pointer);
            DateTime nextmonth = DT.NextMonth(pointer);
            int days1 = DT.DaysInMonth(pointer);
            int days2 = DT.DaysInMonth(nextmonth);
            ReadValueFunc read;
            float carbon = 1;
            float max = 0;

            switch (mode)
            {
                case 0:
                    read = /*EnergyDataNew.QueryDailyEnergy;*/EnergyData.QueryGlobalEnergyDaily;
                    max = EnergyData.GlobalMaxDaily;//EnergyDataNew.max_daily_e;
                    Unit = "kWh";
                    break;
                case 1:
                    read = EnergyData.QueryGlobalCostDaily;//EnergyDataNew.QueryDailyCost;
                    max = EnergyData.GlobalMaxCostDaily;//EnergyDataNew.max_daily_c;
                    Unit = Settings.I.currency;
                    break;
                case 2:
                    read = EnergyData.QueryGlobalEnergyDaily;//EnergyDataNew.QueryDailyEnergy;
                    carbon = Settings.I.carbon;
                    max = EnergyData.GlobalMaxDaily * carbon;//EnergyDataNew.max_daily_e * carbon;
                    Unit = "kg.CO2";
                    break;
                default:
                    return;
            }

            panel1.Visible = false;
            MaxValue = max;
            float mx = -1;
            RoundRectControl m = null;
            for (int i = 0; i < days1; i++ )
            {
                float value = carbon*read(pointer);
                RoundRectControl rc = SetValue(0, pointer.Day - 1, value);
                if( value >= 0 && mx < value )
                {
                    mx = value;
                    m = rc;
                }
                pointer += DT.OneDay;
            }
            pointer = nextmonth;
            for (int i = 0; i < days2; i++)
            {
                float value = carbon * read(pointer);
                RoundRectControl rc = SetValue(1, pointer.Day - 1, value);
                if (mx < value)
                {
                    mx = value;
                    m = rc;
                }

                pointer += DT.OneDay;
            }
            panel1.Visible = true;

            scrWeeks.Pos = month1.Month - 1;
            if (m != null)
                m.BackColor = Color.OrangeRed;

            Reload();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        private void scrWeeks_Scrolled(object sender, EventArgs e)
        {
            int month = scrWeeks.Pos+1;
            //Calendar.I.SetMonth(month1.Year, month);
            DateTime dt = new DateTime(month1.Year, month, 1);
            Calendar.I.SetDay(dt);
        }

        public void SetDay(DateTime day) 
        {
            if (!this.Visible)
                return;
//             if (!DT.SameMonth(month1, day))
//             {
                month1 = DT.MonthFloor(day);
//             }
            DateTime nextmonth = DT.NextMonth(month1);
            lbMonth1.Text = (cbMonths.Items[month1.Month-1] as string);
            lbMonth2.Text = (cbMonths.Items[nextmonth.Month-1] as string);
            lbDate.Text = month1.Year.ToString();
            UpdateData();
        }
        private void LocateToday()
        {
            DateTime t = DateTime.Today;
//             float x = EnergyData.QueryGlobalEnergyDaily(t);
//             if (!Is.ValidF(x) || x < 0)
//                 t = EnergyData.QueryGlobalLastDayValid();
            //SetDay(t);
            t = DT.PrevMonth(t);
            Calendar.I.SetDay(t);
        }
        private void Sub2RightDDByMonth_Load(object sender, EventArgs e)
        {
            CreateCylinders();
            LocateToday();
        }
    }
}
