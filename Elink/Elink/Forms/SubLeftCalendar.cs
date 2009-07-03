using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class Calendar : UserControl
    {
        static Calendar instance = null;
        public static Calendar I { get { return instance; } }

        Color colorblack = Color.FromArgb(64, 64, 64);
        Color colortext = Color.Gray;
        Color colortoday = Color.FromArgb(0, 98, 170);
        Color colorred = Color.OrangeRed;
        DateTime thisday = DateTime.Today;
        Label[] lbDots;
        int selectedYear = 0;
        int offsetYear = 0;
        public DateTime ThisDay
        {
            get { return thisday; }
            set { thisday = value; SetDay(thisday); }
        }
        public Calendar()
        {
            selectedYear = DateTime.Today.Year;
            offsetYear = selectedYear;
            instance = this;
            Global.GoMultiLanguage();
            InitializeComponent();
            lbDots = new Label[] { dot1, dot2, dot3, dot4, dot5, dot6 };
            lbWeeks = new Label[7] { lbw1, lbw2, lbw3, lbw4, lbw5, lbw6, lbw7};
            lbMonthes = new Label[12] { lbMonth1, lbMonth2, lbMonth3, lbMonth4, lbMonth5, lbMonth6, lbMonth7, lbMonth8, lbMonth9, lbMonth10, lbMonth11, lbMonth12 };
            lbDays = new Label[42] {
                lb11, lb12, lb13, lb14, lb15, lb16, lb17, 
                lb21, lb22, lb23, lb24, lb25, lb26, lb27, 
                lb31, lb32, lb33, lb34, lb35, lb36, lb37, 
                lb41, lb42, lb43, lb44, lb45, lb46, lb47, 
                lb51, lb52, lb53, lb54, lb55, lb56, lb57, 
                lb61, lb62, lb63, lb64, lb65, lb66, lb67
            };
            for (int i = 0; i < lbWeeks.Length; i++ )
            {
                lbWeeks[i].Click += OnClickWeek;
                lbWeeks[i].Tag = i + 1;
            }
            for (int i = 0; i < lbMonthes.Length; i++ )
            {
                lbMonthes[i].Click += OnClickMonth;
                lbMonthes[i].Tag = i + 1;
            }
            InitializeDays();
        }
        public void RefreshAll()
        {
            //if (!tblDays.Visible)
            //    return;
            //this.SuspendLayout();
            //bool wasVisible = tblDays.Visible;
            //tblDays.Visible = false;
            UpdateYear();
            InitializeDays();
            UpdateWeekNumber();
            //tblDays.Visible = wasVisible;
            //this.ResumeLayout(true);
        }
        Label[] lbDays;
        Label[] lbWeeks;
        Label[] lbMonthes;

//        DateTime justnow = DateTime.MinValue;
        private void InitializeDays()
        {
//             if( justnow != DateTime.MinValue )
//             {
//                 if (justnow.Year == thisday.Year &&
//                     justnow.Month == thisday.Month)
//                 {
//                     RenderColor();
//                     return;
//                 }
//             }
// 
//             justnow = thisday;
            if (!plDays.Visible)
                return;
            DateTime firstdayofmonth = new DateTime(thisday.Year, thisday.Month, 1);
            int start = (int)firstdayofmonth.DayOfWeek;
            int count = DateTime.DaysInMonth(thisday.Year, thisday.Month);
            int idx = 0;
            for (int i = 0; i < lbDays.Length; i++ )
            {
                if( i < start )
                {
                    lbDays[i].Visible = false;
                }
                else
                    if ( idx < count )
                    {
                        idx++;
                        lbDays[i].Text = idx.ToString();
                        lbDays[i].Click -= OnClickDay;
                        lbDays[i].Click += OnClickDay;
                        DateTime today = new DateTime(thisday.Year, thisday.Month, idx);
                        lbDays[i].Tag = today;
                        if (today.Date == thisday.Date)
                            lbDays[i].ForeColor = colorred;
                        if (today.Date == DateTime.Today.Date)
                            lbDays[i].ForeColor = colortoday;
                        else
                            lbDays[i].ForeColor = colortext;
                        lbDays[i].Visible = true;
                    }
                    else
                    {
                        lbDays[i].Visible = false;
                    }

            }

            UpdateWeekNumber();
            //tblDays.SuspendLayout();
//             if (lbDays != null)
//                 for (int i = 0; i < lbDays.Length; i++)
//                 {
//                     lbDays[i].Dispose();
//                 }
// 
//             lbDays = new Label[count];
//             //tblDays.Controls.Clear();
//             for (int i = 0; i < count; i++)
//             {
//                 lbDays[i] = new Label();
//                 lbDays[i].Margin = new Padding(0, 0, 0, 0);
//                 lbDays[i].AutoSize = false;
//                 lbDays[i].TextAlign = ContentAlignment.MiddleRight;
//                 lbDays[i].ForeColor = colortext;
//                 lbDays[i].Tag = "Days";
//                 lbDays[i].Click += OnClickDay;
//                 lbDays[i].Tag = i + 1;
//                 PlaceDay(start, i);
//             }
            RenderColor();
            //tblDays.ResumeLayout();
        }
        private void PlaceDay(int start, int idx)
        {
//             lbDays[idx].Text = (idx + 1).ToString();
//             int x = 0, y = 0;
//             int stride = tblDays.ColumnCount;
//             x = (start + idx) % stride;
//             y = (start + idx) / stride + 1; // +1 给星期留出一行空间
//             tblDays.Controls.Add(lbDays[idx]);
//             tblDays.SetCellPosition(lbDays[idx], new TableLayoutPanelCellPosition(x, y));
//             lbDays[idx].Dock = DockStyle.Fill;
        }

        private void RenderColor()
        {
            //DateTime realnow = DateTime.Today;
            //lbYear.Text = thisday.Year.ToString();
            //lbYear.ForeColor = colorblack;

//             for (int i = 0; i < lbWeeks.Length; i++)
//             {
//                 lbWeeks[i].ForeColor = colortext;
//             }
            //lbWeeks[(int)thisday.DayOfWeek].ForeColor = colortoday;
//             for( int i=0; i<lbMonthes.Length; i++ )
//             {
//                 if (i == (thisday.Month - 1))
//                     lbMonthes[i].ForeColor = colortoday;
//                 else
//                     lbMonthes[i].ForeColor = colorblack;
//             }
            //lbMonthes[thisday.Month - 1].ForeColor = colortoday;

//             for (int i = 0; i < lbDays.Length; i++)
//             {
//                 if (realnow.Year == thisday.Year &&
//                     realnow.Month == thisday.Month &&
//                     realnow.Day == (i + 1))
//                 {
//                     lbDays[i].ForeColor = colortoday;
//                     lbMonthes[thisday.Month - 1].ForeColor = colortoday;
//                     lbYear.ForeColor = colortoday;
//                     lbWeeks[(int)thisday.DayOfWeek].ForeColor = colortoday;
//                 }
//                 else
//                     lbDays[i].ForeColor = colortext;
//             }
            label1.ForeColor = colorblack;
            label2.ForeColor = colorblack;
        }

        private void OnClickMonth(object sender, EventArgs e)
        {
            Label month = sender as Label;
            int m = Convert.ToInt32(month.Tag);
            thisday = new DateTime(thisday.Year, m, 1);
            RefreshAll();
            month.ForeColor = colorred;
            UpdateWeekNumber();
            UpdateMonth();
            DisplayData.Instance.SetDay(thisday);
        }
        private void OnClickWeek(object sender, EventArgs e)
        {
            //Label week = sender as Label;
            //int w = Convert.ToInt32(week.Tag);
            //thisday = new DateTime(thisday.Year, thisday.Month, d);
            //RefreshAll();
            //DisplayData.Instance.UpdateData();
        }
        private void UpdateDays()
        {
            InitializeDays();
        }
        private void OnClickDay(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            if (sender.GetType() != typeof(Label))
                return;
            Label day = sender as Label;
            if (day.Tag == null)
                return;
            if (day.Tag.GetType() != typeof(DateTime))
                return;
            DateTime dt = (DateTime)day.Tag;
            thisday = dt;
            //RefreshAll();
            UpdateDays();
            //lbDays[d-1].ForeColor = colorred;
            day.ForeColor = colorred;
            DisplayData.Instance.SetDay(thisday);
            //DisplayData.Instance.UpdateData();
        }
        private void UpdateYear()
        {
            //int days = DateTime.DaysInMonth(offsetYear, thisday.Month);
            //if (thisday.Day < days)
            //    days = thisday.Day;
            //thisday = new DateTime(offsetYear, thisday.Month, days);
            //RefreshAll();
            //DisplayData.Instance.SetMontlyPerYear(offsetYear - selectedYear + 3);
            //DisplayData.Instance.SetDay(thisday);
            lbYear.ForeColor = colorred;
            if (thisday.Year == DateTime.Today.Year)
            {
                lbYear.ForeColor = colortoday;
            }

            lbYear.Text = thisday.Year.ToString();
        }
        private void PrevYear_Click(object sender, EventArgs e)
        {
            thisday = new DateTime(thisday.Year - 1, 1, 1);
            UpdateYear();
            DisplayData.Instance.SetDay(thisday);
        }
        private void NextYear_Click(object sender, EventArgs e)
        {
            thisday = new DateTime(thisday.Year + 1, 1, 1);
            UpdateYear();
            DisplayData.Instance.SetDay(thisday);
        }
        public void Today()
        {
            //if (thisday == DateTime.Today)
            //    return;

            thisday = DateTime.Today;
            UpdateYear();
            UpdateMonth();
            UpdateWeekNumber();
            UpdateDays();

            DisplayData.Instance.SetDay(thisday);
            //RefreshAll();
            //DisplayData.Instance.UpdateData();
        }
        public void ShowYearOnly()
        {
            UpdateWeekNumber();
            UpdateMonth();
            plMonth.Visible = false;
            plDays.Visible = false;
            plWeeks.Visible = false;
        }
        public void ShowYearMonthesOnly()
        {
            UpdateWeekNumber();
            UpdateMonth();
            plYear.Visible = true;
            plMonth.Visible = true;
            plDays.Visible = false;
            plWeeks.Visible = false;
        }
        public void ShowYearMonthAndWeeks()
        {
            plYear.Visible = true;
            plMonth.Visible = true;
            plDays.Visible = false;
            plWeeks.Visible = true;
            UpdateWeekNumber();
            UpdateMonth();

        }
        public void ShowToWeekDays()
        {
            plYear.Visible = true;
            plMonth.Visible = true;
//             foreach (Control c in lbDays)
//             {
//                 c.Visible = false;
//             }
//             foreach (Control c in lbWeeks)
//             {
//                 c.Visible = true;
//             }
            plDays.Visible = true;
            plWeeks.Visible = false;
        }
        public void ShowAll()
        {
            plYear.Visible = true;
            plMonth.Visible = true;
//             foreach (Control c in lbDays)
//             {
//                 c.Visible = true;
//             }
//             foreach (Control c in lbWeeks)
//             {
//                 c.Visible = true;
//             }
            plDays.Visible = true;
            plWeeks.Visible = false;
        }
        const int WEEKS_IN_YEAR = 53;
        public int NextWeek(int week)
        {
            return DT.NormalWeek(week+1);
        }
        public void UpdateWeekNumber()
        {
            if( plWeeks.Visible )
            {
                DateTime dt = DT.MonthFloor(thisday);
                int week = DT.WhichWeekOfYear(dt);
                int deltaweek = DT.WeekDistance(dt, thisday);
                for (int i = 0; i < 6; i++)
                {
                    lbDots[i].Text = string.Format("{0}", week);
                    lbDots[i].Tag = dt;  // Use NextWeek()
                    week = NextWeek(week);
                    dt += DT.OneWeek;

                    if (i == deltaweek)
                    {
                        lbDots[i].ForeColor = colorred;
                    }
                    else
                        lbDots[i].ForeColor = colortext;
                }
            }
            if( plDays.Visible )
            {
                for (int i = 0; i < lbWeeks.Length; i++ )
                {
                    lbWeeks[i].ForeColor = colortext;
                    if (i == (int)thisday.DayOfWeek)
                    {
                        lbWeeks[i].ForeColor = colorred;
                    }
                    else
                        if (i == (int)DateTime.Today.DayOfWeek)
                        {
                            if( DT.SameMonth(thisday, DateTime.Today))
                                lbWeeks[i].ForeColor = colortoday;
                        }
                }
            }
        }
        public MonthShort PreviousMonth(int m)
        {
            return PreviousMonth((MonthShort)m);
        }
        public MonthShort PreviousMonth(MonthShort m)
        {
            int x = (int)m;
            x--;
            if (x == 0)
                x = 12;
            return (MonthShort)(x);
        }
        public static MonthShort NextMonth(int m)
        {
            return NextMonth((MonthShort)m);
        }
        public static MonthShort NextMonth(MonthShort m)
        {
            int x = (int)m;
            x--;
            x = (x + 1) % 12;
            return (MonthShort)(x + 1);
        }
        public void UpdateMonth()
        {
            foreach (Label l in lbMonthes)
            {
                l.ForeColor = colorblack;
            }
            int month = thisday.Month;
            lbMonthes[month - 1].ForeColor = colorred;
            if (ThisDay.Year == DateTime.Today.Year)
                lbMonthes[DateTime.Today.Month - 1].ForeColor = colortoday;
//             if( thisday.Year == DateTime.Today.Year )
//             {
//                 lbMonthes[thisday.Month - 1].ForeColor = colorred;
//             }
            //DisplayData.Instance.SetMonth(thisday.Year, thisday.Month);
            //DisplayData.Instance.SetMonth1(m.ToString());
            //DisplayData.Instance.SetMonth2(NextMonth(m).ToString());
            //DisplayData.Instance.SetDay(thisday);
        }

        public static string DaySuffix(int day)
        {
            string s;
            switch (day % 10)
            {
                case 1:
                    s = "st";
                    break;
                case 2:
                    s = "nd";
                    break;
                case 3:
                    s = "rd";
                    break;
                default:
                    s = "th";
                    break;
            }
            return s;
        }
        public enum Months
        {
            January = 1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }
        public enum MonthShort
        {
            JAN = 1,
            FEB,
            MAR,
            APR,
            MAY,
            JUN,
            JUL,
            AUG,
            SEP,
            OCT,
            NOV,
            DEC
        }

        private void dot1_Click(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            Label lb = (Label)sender;
            if (lb.Tag == null)
                return;
            if (lb.Tag.GetType() != typeof(DateTime))
                return;
            DateTime t = (DateTime)lb.Tag;
            thisday = t;
            DisplayData.Instance.SetDay(t);
            UpdateWeekNumber();

            UpdateMonth();
            foreach (Label l in lbDots)
            {
                if (l != lb)
                    l.ForeColor = colortext;
                else
                {
                    l.ForeColor = colorred;
                    lbMonthes[t.Month - 1].ForeColor = colorred;
                }
            }
        }
//         public void SetMonth(int year, int month)
//         {
//             //thisday = new DateTime(year, month, 1);
//             //UpdateMonth();
//         }
        public void SetWeek(int week)
        {
            if (lbDots == null)
                return;
            if (week < 0 || week > lbDots.Length)
                return;
            dot1_Click(lbDots[week], null);
            
        }
        public void SetDay(DateTime day)
        {
            thisday = day;
            RefreshAll();
            UpdateYear();
            UpdateMonth();
            UpdateDays();
            DisplayData.Instance.SetDay(thisday);
        }
        public void SetDay(int day)
        {
            // weekday: 0~6 within the week
            if (thisday.Day - (int)thisday.DayOfWeek <= 0)
                return;
            DateTime sunday = new DateTime(thisday.Year, thisday.Month, thisday.Day - (int)thisday.DayOfWeek);
            TimeSpan ts = new TimeSpan(day, 0, 0, 0);
            DateTime theday = sunday + ts;
            SetDay(theday);
        }

        private void Calendar_Load(object sender, EventArgs e)
        {
            SetDay(DateTime.Today);
        }

//         public void SetMontlyPerYear(int year)
//         {
//             // year: 0~5
//             int delta = year - 3;
//             offsetYear = selectedYear + delta;
//             UpdateYear();
//             //DisplayData.Instance.SetMontlyPerYear(year);
//             DisplayData.Instance.SetDay(thisday);
//         }
    }

    public static class DT
    {
        public static readonly TimeSpan OneHour = new TimeSpan(1, 0, 0);
        public static readonly TimeSpan OneDay = new TimeSpan(1, 0, 0, 0);
        public static readonly TimeSpan OneWeek = new TimeSpan(7, 0, 0, 0);
        public static readonly TimeSpan OneTick = new TimeSpan(0, 0, 0, 0, 1);
        public static bool IsValid(DateTime t)
        {
            if (t.Year == DateTime.MinValue.Year ||
                t.Year == DateTime.MaxValue.Year)
                return false;

            return true;
        }
        public static bool IsStrictlyValid(DateTime t)
        {
            if (!IsValid(t))
                return false;
            if (t.Year < 1980 /*No computer yet*/ || 
                t.Year > 4999 /*Can I live old enough to see this?*/)
                return false;
            return true;
        }
        public static TimeSpan Days(int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }
        public static TimeSpan Hours(int hours)
        {
            return new TimeSpan(hours, 0, 0);
        }
        public static int WeekDistance(DateTime d1, DateTime d2)
        {
            if (d1 > d2) { DateTime tmp = d1; d1 = d2; d2 = tmp; }
            double days = (d2 - d1).TotalDays;
            int week = (int)Math.Floor(days / 7);
            if (d1.DayOfWeek > d2.DayOfWeek)
                week++;
            return week;
        }
        const int WEEKS_IN_YEAR = 53;
        public static int NormalWeek(int week)
        {
            // 1 based week
            while (week <= 0)
                week += WEEKS_IN_YEAR;
            if (week <= WEEKS_IN_YEAR)
                return week;
            week %= WEEKS_IN_YEAR;
            return week;
        }
        public static int WhichWeekOfYear(DateTime t)
        {
            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool ThursdayFlag = false;

            int DayOfYear = t.DayOfYear;

            int StartWeekDayOfYear =
                 (int)(new DateTime(t.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            int EndWeekDayOfYear =
                 (int)(new DateTime(t.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

//             StartWeekDayOfYear = StartWeekDayOfYear;
//             EndWeekDayOfYear = EndWeekDayOfYear;
            if (StartWeekDayOfYear == 0)
                StartWeekDayOfYear = 7;
            if (EndWeekDayOfYear == 0)
                EndWeekDayOfYear = 7;

            int DaysInFirstWeek = 8 - (StartWeekDayOfYear);
            int DaysInLastWeek = 8 - (EndWeekDayOfYear);

            if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
                ThursdayFlag = true;

            int FullWeeks = (int)Math.Ceiling((DayOfYear - (DaysInFirstWeek)) / 7.0);

            int WeekNumber = FullWeeks;

            if (DaysInFirstWeek >= THURSDAY)
                WeekNumber = WeekNumber + 1;

            if (WeekNumber > 52 && !ThursdayFlag)
                WeekNumber = 1;

            if (WeekNumber == 0)
                WeekNumber = WhichWeekOfYear(
                     new DateTime(t.Year - 1, DEC, LASTDAYOFDEC));
            return WeekNumber;
        }

        public static int NormalMonth(int month)
        {
            while (month <= 0)
                month += 12;
            if (month <= 12)
                return month;
            month %= 12;
            return month;
        }
        public static DateTime LastMonth(DateTime m)
        {
            int year = m.Year;
            int month = m.Month;
            month--;
            if (month == 0)
            {
                year--;
                month = 12;
            }
            return new DateTime(year, month, 1,
                m.Hour,
                m.Minute,
                m.Second,
                m.Kind);
        }
        // 00:00:00.000
        public static DateTime DateFloor(DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0, d.Kind);
        }
        // 23:59:59.999
        public static DateTime DateCeiling(DateTime d)
        {
            if (d == DateTime.MaxValue)
                return DateTime.MaxValue;
//             DateTime max = DateTime.MaxValue;
//             DateTime x = new DateTime(d.Year, d.Month, d.Day,
//                 max.Hour,
//                 max.Minute,
//                 max.Second
//                 );
            DateTime x = DateFloor(d + OneDay);
            x -= OneTick;
            return x;
        }
        public static DateTime NextDay(DateTime t )
        {
            return t + OneDay;
        }
        public static DateTime NextDayFloor(DateTime t)
        {
            return DateFloor(t) + OneDay;
        }
        public static DateTime HourFloor(DateTime h)
        {
            return new DateTime(h.Year, h.Month, h.Day, h.Hour, 0, 0, h.Kind);
        }
        public static DateTime HourCeiling(DateTime h)
        {
            if (h == DateTime.MaxValue)
                return DateTime.MaxValue;

            h += new TimeSpan(1, 0, 0);
            return HourFloor(h);
        }

        public static DateTime NextMonth(DateTime d)
        {
            int year = d.Year;
            int month = d.Month;
            month++;
            if (month == 13)
            {
                month = 1;
                year++;
            }
            return new DateTime(year, month, 1, 
                d.Hour,
                d.Minute,
                d.Second,
                d.Kind);
        }
        public static DateTime PrevMonth(DateTime d)
        {
            if (d == DateTime.MinValue)
                return DateTime.MinValue;
            int year = d.Year;
            int month = d.Month;
            month--;
            if (month == 0)
            {
                month = 12;
                year--;
            }
            return new DateTime(year, month, 1, 
                d.Hour,
                d.Minute,
                d.Second,
                d.Kind);
        }
        public static int DaysInMonth(DateTime d)
        {
            return DateTime.DaysInMonth(d.Year, d.Month);
        }
        public static DateTime MonthFloor(DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1,
                0,
                0,
                0,
                d.Kind);
        }
        public static DateTime MonthCeiling(DateTime d)
        {
            int days = DaysInMonth(d);
            DateTime max = DateTime.MaxValue;
            DateTime dt = new DateTime(d.Year, d.Month, days, 
                max.Hour,
                max.Minute,
                max.Second,
                max.Millisecond,
                d.Kind
                );
            return dt;
        }
        public static bool SameMonth(DateTime d1, DateTime d2)
        {
            return d1.Year == d2.Year && d1.Month == d2.Month;
        }
        public static bool SameDay(DateTime d1, DateTime d2 )
        {
            if (!SameMonth(d1, d2))
                return false;
            return d1.Day == d2.Day;
        }
        public static bool SameWeek(DateTime d1, DateTime d2)
        {
            if( d1.Year == d2.Year )
            {
                int week1 = WhichWeekOfYear(d1);
                int week2 = WhichWeekOfYear(d2);
                return week1 == week2;
            }
            return false;
        }
        public static DateTime WeekFloor(DateTime d)
        {
            if (d == DateTime.MinValue)
                return DateTime.MinValue;
            d = DateFloor(d);
            int day = (int)d.DayOfWeek;
            return d - new TimeSpan(day, 0, 0, 0);
        }
        public static DateTime NextWeek(DateTime d)
        {
            if (d == DateTime.MaxValue)
                return DateTime.MaxValue;

            return d + new TimeSpan(7, 0, 0, 0);
        }
        public static int MonthDistance(DateTime d1, DateTime d2)
        {
            if(d1>d2 )
            {
                DateTime d3 = d1;
                d1 = d2;
                d2 = d3;
            }

            int count = 0;
            count = (d2.Year - d1.Year) * 12;
            count += (d2.Month - d1.Month);
            return count;
        }
        public static DateTime BeforeMonths(DateTime t, int m)
        {
            if (!IsStrictlyValid(t))
                return DateTime.MinValue;
            for (int i = 0; i < m; i++ )
            {
                t = PrevMonth(t);
            }
            return t;
        }
        public static DateTime AfterMonths(DateTime t, int m)
        {
            if (!IsStrictlyValid(t))
                return DateTime.MaxValue;
            for (int i = 0; i < m; i++)
            {
                t = NextMonth(t);
            }
            return t;
        }
    }
}
