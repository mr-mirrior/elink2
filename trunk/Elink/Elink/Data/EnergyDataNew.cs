using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Elink
{
    public class EnergyDataNew1: ICloneable
    {
        #region 全局部分
        public static DataFromE2 devdata = new DataFromE2();
        public const string REPOSITORY_PATH = "Repository";
        public static List<float> map = new List<float>();
        public static List<float> costs = new List<float>();
        public static List<TimeValuePair> max_instant_power = new List<TimeValuePair>();
        public static TwoWorld latest_time_e2 = new TwoWorld();
        public static DateTime GlobalSince = DateTime.MinValue; // E2 Time
        public static DateTime GlobalUntil = DateTime.MaxValue; // E2 Time
        public static TimeSpan TimeDifferenceE2 = new TimeSpan();
        public static List<EnergyDataNew1> periods = new List<EnergyDataNew1>();
        public static event EventHandler OnChange;


        private static DateTime IndexToHour(int idx, DateTime latest_day1)
        {
            DateTime dt = DT.DateFloor(latest_day1);
            if (!DT.IsStrictlyValid(dt))
                return DateTime.MinValue;

            int days = idx / 24;
            int hours = idx % 24;
            return (DT.DateFloor(dt) - DT.Days(days) + DT.Hours(hours));
        }
        private static void Clear()
        {
            ClearValues();
            map.Clear();
            costs.Clear();
            max_instant_power.Clear();
        }
        private static string EnergyRecord()
        {
            string file = Path.Combine(REPOSITORY_PATH, "EnergyRecord.csv");
            FileInfo fi = new FileInfo(file);
            return fi.FullName;
        }
        private static List<string[]> LoadCSV()
        {
            string energyfile = EnergyRecord();
            FileInfo fi = new FileInfo(energyfile);
            if (!fi.Exists)
                return null;
            CSV csv = new CSV();
            csv.Load(energyfile);
            if (csv.lines == null)
            {
                return null;
            }
            if (csv.lines.Count < 2)
                return null;
            return csv.lines;
        }
        private static bool Parse(List<string[]> read)
        {
            if (read.Count < 2)
                return false;

            map = new List<float>(new float[read.Count - 2]);
            //costs = new List<float>(map);
            int idx = 0;

            // 解析最后保存时间
            if( read[1].Length > 1 )
            {
                DateTime e2, pc;
                bool result = true;
                result &= DateTime.TryParse(read[1][0], out e2);
                result &= DateTime.TryParse(read[1][1], out pc);
                if (result)
                {
                    latest_time_e2 = new TwoWorld(e2, pc);
                }
                else
                    latest_time_e2 = null;
            }
            else
                latest_time_e2 = null;
            // 解析保存数据
            for (int i = 2; i < read.Count; i++, idx++)
            {
                string[] s = read[i];
                // 分析小时电量
                if (s.Length >= 2)
                {
                    float energy = 0;
                    float.TryParse(s[1], out energy);
                    map[idx] = energy;
                }
                // 分析最大功率
                if (s.Length >= 3)
                {
                    float maxinstantpower = 0;
                    if (!float.TryParse(s[2], out maxinstantpower))
                        continue;
                    DateTime whenmax;
                    if (!DateTime.TryParse(s[3], out whenmax))
                        continue;
                    max_instant_power.Add(new TimeValuePair(whenmax, maxinstantpower));
                }
            }
            return true;
        }
        public static EnergyDataNew1 Latest = null;
        private static void LoadTariffs()
        {
            periods.Clear();
            DirectoryInfo di = new DirectoryInfo(REPOSITORY_PATH);
            if (!di.Exists)
            {
                Directory.CreateDirectory(REPOSITORY_PATH);
                return;
            }
            FileInfo[] tariff = di.GetFiles("*.xml");
            if (tariff.Length == 0)
                return;

            Latest = null;
            DateTime latest = DateTime.MinValue;
            for (int i = 0; i < tariff.Length; i++ )
            {
                FileInfo tarifffile = new FileInfo(tariff[i].FullName);
                Tariff adv = Tariff.LoadTariffXML(tariff[i].FullName);
                TimeDifferenceE2 = adv.valid_until.Diff();
                if (adv != null)
                {
                    EnergyDataNew1 edn = new EnergyDataNew1(adv);
                    //edn.Stat();
                    periods.Add(edn);
                    if( latest < edn.valid_until.E2Time )
                    {
                        latest = edn.valid_until.E2Time;
                        Latest = edn;
                    }
                }
            }
            if (Latest != null)
            {
                Settings.I.advTariff = Latest.t;
            }
        }
        public static bool IsLastDownload()
        {
            return latest_time_e2 != null;
        }
        private static void UpdateTimeLoad()
        {
//             if( periods.Count == 0 )
//             {
// 
//                 {
//                     GlobalSince = DateTime.MinValue;
//                     GlobalUntil = DateTime.MaxValue;
//                     TimeDifferenceE2 = new TimeSpan();
//                     latest_time_e2 = null;
//                 }
//                 return;
//             }
//             DateTime t1 = DateTime.MaxValue;
//             for (int i = 0; i < periods.Count; i++ )
//             {
//                 if (t1 > periods[i].valid_since.E2Time)
//                     t1 = periods[i].valid_since.E2Time;
//             }
            GlobalUntil = latest_time_e2.E2Time;
        }
        private static void UpdateSinceLoad()
        {
            DateTime t1 = DateTime.MaxValue;
            for (int i = 0; i < periods.Count; i++)
            {
                if (t1 > periods[i].valid_since.E2Time)
                    t1 = periods[i].valid_since.E2Time;
            }
            if (DT.IsStrictlyValid(t1))
                GlobalSince = t1;
            else
                GlobalSince = GlobalUntil - DT.Days(map.Count / 24);
        }
        public static void StatAll()
        {
            foreach (EnergyDataNew1 edn in periods)
            {
                edn.Stat();
            }
        }
        public static bool Load()
        {
            Clear();
            UpdateTimeLoad();

            List<string[]> lines = LoadCSV();
            if (lines == null)
                return false;
            Parse(lines);
            LoadTariffs();
            UpdateSinceLoad();
            StatAll();

            GlobalStat();

            if (OnChange != null)
                OnChange.Invoke(null, null);
            return true;
        }
        public static bool NewPeriod(Tariff t )
        {
            for (int i = 0; i < periods.Count; i++ )
            {
                if (periods[i].t.SameName(t))
                    return false;
            }
            EnergyDataNew1 e = new EnergyDataNew1(t);

            int i_was_here;

            e.Stat();
            periods.Add(e);
            Save();
            Load();
            return false;
        }
        public static bool Rename(string oldt, string newt)
        {
            Tariff t = null;
            for (int i = 0; i < periods.Count; i++)
            {
                if (periods[i].t.SameName(t))
                {
                    t = periods[i].t;
                    break;
                }
            }
            if (t != null)
                return false;

            t.Rename(newt);
            return true;
        }
        public static int Count() { return periods.Count; }
        public static bool Empty() { return Count() == 0; }

        /////////////////////////////////////////////////////////////////////////
        // Save
        /////////////////////////////////////////////////////////////////////////
        private static List<string[]> CreateTitle()
        {
            List<string[]> save = new List<string[]>();
            string[] title = new string[] { 
                "Time",
                "Hourly KWH",
                "Monthly Max Instant KW",
                "When Max",
                "Warning: DO NOT EDIT THIS FILE PLEASE!"};
            save.Add(title);

            string[] last_save = new string[title.Length];
            if (latest_time_e2.IsValid)
            {
                last_save[0] = latest_time_e2.E2Time.ToString("o", CultureInfo.InvariantCulture);
                last_save[1] = latest_time_e2.PCTime.ToString("o", CultureInfo.InvariantCulture);
            }
            save.Add(last_save);
            return save;
        }
        private static void UpdateTimeSave()
        {
            for (int i = 0; i < periods.Count; i++ )
            {

            }
        }
        private static string RefTime(int i)
        {
            DateTime pointer = IndexToHour(i, DT.DateFloor(GlobalUntil));
            if (i % 24 == 0)
                return string.Format("'{0:00}:00 {1}/{2}/{3:00}",
                    pointer.Hour, 
                    pointer.Year, 
                    ((Calendar.MonthShort)pointer.Month).ToString(), 
                    pointer.Day);
            else
                return string.Format("'{0:00}:00", pointer.Hour);
        }
        private static void SaveValues(ref List<string[]> save)
        {
            if( map.Count == 0 )
                return;
            for (int i = 0; i < map.Count; i++ )
            {
                string[] s = new string[5];

                s[0] = RefTime(i);
                s[1] = map[i].ToString();
                if( i < max_instant_power.Count )
                {
                    s[2] = max_instant_power[i].value.ToString();
                    s[3] = max_instant_power[i].when.ToString("o", CultureInfo.InvariantCulture);
                }
                save.Add(s);
            }
            CSV csv = new CSV();
            csv.lines = save;
            csv.Save(EnergyRecord());
        }
        
        public static bool Save()
        {
            UpdateTimeSave();

            List<string[]> save = CreateTitle();
            SaveValues(ref save);

            return true;
        }
        static void CheckTimeMerge()
        {
            DateTime dt = devdata.now_in_E2;
            if (!DT.IsStrictlyValid(dt))
                return;
            if (!DT.IsStrictlyValid(GlobalSince))
                GlobalSince = dt - DT.Days(240);
        }

        static void UpdateTimeMerge()
        {
            TimeDifferenceE2 = devdata.now_in_PC - devdata.now_in_E2;
            GlobalUntil = devdata.now_in_E2;
            latest_time_e2 = new TwoWorld(devdata.now_in_E2, devdata.now_in_PC);
        }
        static void MergeHourly()
        {
            if( map.Count == 0 )
            {
                map.AddRange(devdata.kwh_240days);
                return;
            }
            if (!DT.IsStrictlyValid(GlobalUntil) ||
                !DT.IsStrictlyValid(GlobalSince))
                return;
            int newdays = 240;
            int olddays = map.Count / 24;
            DateTime until = DT.DateFloor(devdata.now_in_E2);
            DateTime since = DT.DateFloor(GlobalSince);
            DateTime olduntil = DT.DateFloor(GlobalUntil);
            DateTime boundary = DT.DateCeiling(GlobalUntil);
            DateTime newbottom = until - DT.Days(newdays);

            float[] newdata = devdata.kwh_240days;
            List<float> olddata = map;

            int diffdays = (int)(until - olduntil).TotalDays;

            int count = ((int)(until - since).TotalDays+1) * 24;
            List<float> mapalt = new List<float>(newdata);
            DateTime pointer = until;
            int idx = diffdays * 24;
            int from = olddata.Count - (diffdays*24);

            for (int i = from; i < olddata.Count; i++ )
            {
                mapalt.Add(map[i]);
            }

            map = mapalt;
        }
        static void MergeMaxPower()
        {
            max_instant_power.Clear();
            // 只重写，未合并
            int TODOMergeMaxPower;
            DateTime until = DT.DateFloor(GlobalUntil);
            DateTime pointer = until;
            for (int i = 0; i < devdata.when_max_power_13months.Length; i++ )
            {
                int d = devdata.when_max_power_13months[i];
                float v = devdata.max_power_13months[i];
                if (d == 0)
                    continue;
                try
                {
                    DateTime t = new DateTime(pointer.Year,
                                        pointer.Month,
                                        BitOp.Byte2Int(d), // day
                                        BitOp.Byte1Int(d), // hour
                                        BitOp.Byte0Int(d), // minute
                                        0, DateTimeKind.Local);

                    max_instant_power.Add(new TimeValuePair(t, v));
                }
                catch
                {
                    continue;
                }
                pointer = DT.PrevMonth(pointer);
            }
        }
        public const string DATA_E2_PATH = "DataE2";
        public static void SavePreservedData()
        {
            DateTime e2 = devdata.now_in_E2;
            string file = string.Format("{0}.{1}.{2} {3}.{4}.{5}.xml",
                e2.Year, e2.Month, e2.Day,
                e2.Hour, e2.Minute, e2.Second);
            XMLUtil<DataFromE2>.SaveXml(
                Path.Combine(DATA_E2_PATH, file), devdata);
        }
        public static void SavePeriods()
        {
            if( periods.Count == 0 )
            {
                Tariff t = new Tariff();
                t.valid_since = new TwoWorld(GlobalSince, TimeDifferenceE2);
                t.valid_until = new TwoWorld(devdata.now_in_E2, TimeDifferenceE2);
                t.tariff_name = "Untitled";

                EnergyDataNew1 edn = new EnergyDataNew1(t);
                periods.Add(edn);
            }
            DirectoryInfo di = new DirectoryInfo(REPOSITORY_PATH);
            if (!di.Exists)
                di.Create();
            for (int i = 0; i < periods.Count; i++ )
            {
                string file = Path.Combine(di.FullName, (i+1).ToString());
                file = Path.ChangeExtension(file, "xml");
                periods[i].t.SaveToXML(file);
            }
        }
        public static void SimulateMerge()
        {
            DataFromE2 d = XMLUtil<DataFromE2>.LoadXml("DataE2\\2008.9.7 13.52.0.xml");
            if( d != null )
            {
                devdata = d;
                Merge();
            }
        }
        public static void Merge()
        {
            CheckTimeMerge();
            SavePreservedData();
            MergeHourly();
            MergeMaxPower();
            UpdateTimeMerge();

            SavePeriods();
        }

        #endregion

        #region 对象部分


        public EnergyDataNew1(Tariff tt)
        {
            t = tt;
        }
        List<float> valid_kwhs = new List<float>();
        List<float> valid_costs = new List<float>();
        public TwoWorld valid_since { get { return t.valid_since; } set { t.valid_since = value; } }
        public TwoWorld valid_until { get { return t.valid_until; } set { t.valid_until = value; } }
        Tariff t = new Tariff();

        public object Clone()
        {
            EnergyDataNew1 x = (EnergyDataNew1)this.MemberwiseClone();
            x.t = (Tariff)this.t.Clone();

            return x;
        }

        void Stat()
        {
            CalcValid();
            CalcCosts();
            CalcOthers();
        }

        void CalcValid()
        {
            List<float> alt = new List<float>(map);
            for (int i = 0; i < alt.Count; i++)
            {
                if (alt[i] == 0)
                {
                    alt[i] = Is.UntouchedF;
                    continue;
                }
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (pointer >= valid_since.E2Time &&
                    pointer <= DT.DateCeiling(valid_until.E2Time))
                {
                    // keep it
                }
                else
                {
                    alt[i] = Is.UntouchedF;
                }
            }
            valid_kwhs = alt;

        }
        void CalcCosts()
        {
            valid_costs = new List<float>(valid_kwhs);
            for (int i = 0; i < valid_costs.Count; i++ )
            {
                if (!Is.ValidF(valid_costs[i]))
                    valid_costs[i] = 0;
            }

            int month = -1;
            int idx = -1;
            List<float> ml = new List<float>();

            // 首先计算每个有效月份的总电量，
            // 以判断是否超过限额
            for (int i = 0; i < valid_kwhs.Count; i++)
            {
                DateTime now = IndexToHour(i, GlobalUntil);
                if (month != now.Month)
                {
                    month = now.Month;
                    idx++;
                    ml.Add(0);
                }
                else
                {
                }
                if (Is.ValidF(valid_kwhs[i]))
                {
                    //if (float.IsNaN(ml[idx]))
                    //    ml[idx] = 0;
                    ml[idx] += valid_kwhs[i];
                }
            }
            month = -1;
            idx = -1;
            // 再根据每月实际耗电量，比较限额
            // 计算超出的部分，并计费
            for (int i = 0; i < valid_kwhs.Count; i++)
            {
                DateTime now = IndexToHour(i, GlobalUntil);
                if (month != now.Month)
                {
                    month = now.Month;
                    idx++;
                }

                float over = ml[idx] - t.over_limit;
                float overchargeperhour = 0;
                if (over > 0)
                {
                    overchargeperhour = over * t.over_charge / 30 / 24;
                }
                valid_costs[i] = t.HourlyFee(now, valid_kwhs[i], overchargeperhour);
            }
        }
        void CalcOthers()
        {

        }

        public List<float> MonthlyCost(int year, out float total, out float max)
        {
            total = 0;
            max = float.MinValue;
            List<float> monthly = new List<float>();
            for (int i = 0; i < 12; i++)
            {
                monthly.Add(float.NaN);
            }

            for (int i = 0; i < valid_costs.Count; i++)
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (pointer.Year != year)
                    continue;
                if (!Is.ValidF(valid_costs[i]) ||
                    valid_costs[i] <= 0)
                    continue;
                total += valid_costs[i];
                //if (max < valid_costs[i])
                //    max = valid_costs[i];

                int m = pointer.Month - 1;
                if (!Is.ValidF(monthly[m]))
                    monthly[m] = valid_costs[i];
                else
                    monthly[m] += valid_costs[i];
            }
            max = FloatList.Max(monthly);
            return monthly;
        }

        public float TotalCost()
        {
            if (valid_costs == null)
                return -1;
            float sum = 0;
            for (int i = 0; i < valid_costs.Count; i++)
            {
                if (Is.ValidF(valid_costs[i]) &&
                    valid_costs[i] > 0)
                    sum += valid_costs[i];
            }
            return sum;
        }

        public void SetTariff(Tariff tt)
        {
            t = tt;
            Stat();
        }
        public string TariffName()
        {
            if (t != null)
                return t.tariff_name;
            return null;
        }

        bool isYearly = false;
        bool isMonthly = false;
        public bool IsYearly() { return isYearly; }
        public bool IsMonthly() { return isMonthly; }
        // 不带计费信息的年度EnergyDataNew1，用于比较其他Tariff
        public List<EnergyDataNew1> YearlyCopies()
        {
            List<EnergyDataNew1> yearly = new List<EnergyDataNew1>();
            int year = 0;
            for (int i = 0; i < valid_costs.Count; i++)
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (pointer.Year != year)
                {
                    year = pointer.Year;
                    //yearly.Add(new EnergyDataNew1());
                }
                else
                    continue;
                EnergyDataNew1 e = (EnergyDataNew1)this.Clone();
                DateTime from = new DateTime(year, 1, 1);
                DateTime to = DT.DateCeiling(new DateTime(year, 12, 31));
                bool need_restat = false;
                // 如果起始时间早于这一年，设为这一年
                if (e.valid_since.E2Time < from &&
                    from <= e.valid_until.E2Time)
                {
                    e.t.valid_since.E2Time = from;
                    need_restat = true;
                }
                // 如果结束时间晚于这一年，设为这一年
                if (e.valid_until.E2Time > to &&
                    to > e.valid_since.E2Time)
                {
                    e.t.valid_until.E2Time = to;
                    need_restat = true;
                }
                if (need_restat)
                    e.Stat();

                e.isYearly = true;
                e.isMonthly = false;

                yearly.Add(e);
            }

            return yearly;
        }
        // 不带计费信息的月度EnergyDataNew1，用于比较其他Tariff
        public List<EnergyDataNew1> MonthlyCopies()
        {
            List<EnergyDataNew1> monthly = new List<EnergyDataNew1>();
            int month = 0;
            for (int i = 0; i < valid_costs.Count; i++)
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (month != pointer.Month)
                {
                    month = pointer.Month;
                }
                else
                    continue;
                EnergyDataNew1 e = (EnergyDataNew1)this.Clone();
                DateTime from = DT.MonthFloor(pointer);
                DateTime to = DT.MonthCeiling(pointer);
                bool need_restat = false;
                // 如果起始时间早于这一月，设为这一月
                if (e.valid_since.E2Time < from &&
                    from <= e.valid_until.E2Time )
                {
                    e.t.valid_since.E2Time = from;
                    need_restat = true;
                }
                if (e.valid_until.E2Time > to &&
                    to >= e.valid_since.E2Time)
                {
                    e.t.valid_until.E2Time = to;
                    need_restat = true;
                }

                if (need_restat)
                    e.Stat();

                e.isMonthly = true;
                e.isYearly = false;

                monthly.Add(e);
            }

            return monthly;
        }
        //////////////////////////////////////////////////////////////////
        // 全局
        public static float max_e = float.MinValue;
        public static float max_c = float.MinValue;
        public static float max_daily_e = 0;
        public static float max_daily_c = 0;
        public static float max_monthly_e = 0;
        public static float max_monthly_c = 0;
        public static float avr_hourly_e = Is.UntouchedF;
        public static float avr_hourly_c = Is.UntouchedF;
        public static float total_thisyear_e = 0;
        public static float total_thisyear_c = 0;
        public static float avr_daily_e = 0;
        public static float avr_daily_c = 0;
        public static List<float> daily_e;
        public static List<float> daily_c;
        public static List<float> weekly_e;
        public static List<float> weekly_c;
        public static List<float> monthly_e;
        public static List<float> monthly_c;
        public static float avr_weekly_e = 0, avr_weekly_c = 0;
        public static float avr_monthly_e = 0, avr_monthly_c = 0;
        public static float avr_yuan_per_kwh = 0;
        public static float AvrDailyEnergy() { return avr_daily_e; }
        public static float AvrDailyCost() { return avr_daily_c; }
        public static float AvrDailyCarbon() { return AvrDailyEnergy() * Settings.I.carbon; }
        public static float AvrWeeklyEnergy() { return avr_weekly_e; }
        public static float AvrWeeklyCost() { return avr_weekly_c; }
        public static float AvrWeeklyCarbon() { return AvrWeeklyEnergy() * Settings.I.carbon; }
        public static float AvrMonthlyEnergy() { return avr_monthly_e; }
        public static float AvrMonthlyCost() { return avr_monthly_c; }
        public static float AvrMonthlyCarbon() { return AvrMonthlyEnergy() * Settings.I.carbon; }

        private static void ClearValues()
        {
        }
        private static void GlobalStat()
        {
            calcGlobalCosts();
            avr_hourly_e = calcHourlyAverageEnergy();
            avr_hourly_c = calcHourlyAverageCost();
            total_thisyear_e = calcTotalThisYearEnergy();
            total_thisyear_c = calcTotalThisYearCost();
            avr_daily_e = calcAvrDailyEnergy();
            avr_daily_c = calcAvrDailyCost();
            calcOthers(out daily_e, out daily_c,
                out weekly_e, out weekly_c,
                out monthly_e, out monthly_c);

            max_e = FloatList.Max(map);
            max_c = FloatList.Max(costs);
            max_daily_e = Is.UntouchedF;
            max_daily_c = Is.UntouchedF;
            max_monthly_c = Is.UntouchedF;
            max_monthly_e = Is.UntouchedF;
            avr_monthly_e = avr_monthly_c = Is.UntouchedF;
            avr_weekly_e = avr_weekly_c = Is.UntouchedF;
            if (daily_e.Count != 0) max_daily_e = FloatList.Max(daily_e);
            if (daily_c.Count != 0) max_daily_c = FloatList.Max(daily_c);
            if (monthly_e.Count != 0)
            {
                max_monthly_e = FloatList.Max(monthly_e);
                max_monthly_c = FloatList.Max(monthly_c);
                avr_monthly_e = FloatList.Average(monthly_e);
                avr_monthly_c = FloatList.Average(monthly_c);
            }
            if (weekly_e.Count != 0)
            {
                avr_weekly_e = FloatList.Average(weekly_e);
                avr_weekly_c = FloatList.Average(weekly_c);
            }
            avr_yuan_per_kwh = Is.UntouchedF;
            if( map.Count != 0 )
            {
                float e = FloatList.Sum(map);
                float c = FloatList.Sum(costs);
                if (e != 0)
                    avr_yuan_per_kwh = c / e;
            }
        }
        static float calcTotalThisYearEnergy()
        {
            if (map.Count == 0)
                return Is.UntouchedF;
            int year = DateTime.Today.Year;
            //int count = 0;
            float sum = 0;
            for (int i = 0; i < map.Count; i++ )
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (year != pointer.Year)
                    continue;
                if (Is.ValidF(map[i]) && map[i] != 0)
                {
                    sum += map[i];
                    //count++;
                }
            }

            return sum;
        }
        static float calcTotalThisYearCost()
        {
            if (costs.Count == 0)
                return Is.UntouchedF;
            int year = DateTime.Today.Year;
            //int count = 0;
            float sum = 0;
            for (int i = 0; i < map.Count; i++)
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (year != pointer.Year)
                    continue;
                if (Is.ValidF(map[i]) && map[i] != 0)
                {
                    sum += costs[i];
                    //count++;
                }
            }

            return sum;
        }

        static void calcGlobalCosts()
        {
            if (periods.Count == 0)
            {
                costs.Clear();
                return;
            }
            costs = new List<float>(map);
            foreach (EnergyDataNew1 ed in periods)
            {
                List<float> c = ed.valid_costs;
                //int from = (int)(DT.DateFloor(GlobalUntil) - ed.valid_until.E2Time).TotalHours;
                for (int i = 0; i < c.Count; i++ )
                {
                    if( Is.ValidF(c[i]) )
                    {
                        costs[i] = c[i];
                    }
                }
            }
        }
        static float TotalThisYearEnergy() { return total_thisyear_e; }
        static float TotalThisYearCost() { return total_thisyear_c; }
        static float TotalThisYearCarbon() { return TotalThisYearEnergy() * Settings.I.carbon; }

        static float calcAvrDailyEnergy() 
        {
            if (map.Count == 0)
                return Is.UntouchedF;

            List<float> days = new List<float>();
            int idx = 0;
            int day = 0;
            for (int i = 0; i < map.Count; i++ )
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if( day != pointer.Day )
                {
                    days.Add(Is.UntouchedF);
                    idx = days.Count - 1;
                    day = pointer.Day;
                }
                if (map[i] != 0)
                {
                    if( !Is.ValidF(days[idx]) )
                    {
                        days[idx] = 0;
                    }
                    days[idx] += map[i];
                }
            }

            return FloatList.Average(days);
        }
        static float calcAvrDailyCost()
        {
            if (costs.Count == 0)
                return Is.UntouchedF;

            List<float> days = new List<float>();
            int idx = 0;
            int day = 0;
            for (int i = 0; i < costs.Count; i++)
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if (day != pointer.Day)
                {
                    days.Add(0);
                    idx = days.Count - 1;
                    day = pointer.Day;
                }
                if (map[i] != 0)
                {
                    if (!Is.ValidF(days[idx]))
                    {
                        days[idx] = 0;
                    }
                    days[idx] += costs[i];
                }
            }

            return FloatList.Average(days);
        }
        static float calcHourlyAverageEnergy()
        {
            if (map.Count == 0)
                return Is.UntouchedF;

            List<float> alt = new List<float>(map);
            for (int i = 0; i < alt.Count; i++ )
            {
                if (alt[i] == 0)
                    alt[i] = Is.UntouchedF;
            }
            float average = FloatList.Average(alt);
            return average;
        }
        static float calcHourlyAverageCost()
        {
            if (costs.Count == 0)
                return Is.UntouchedF;
            float cost = 0;
            int count = 0;
            foreach (EnergyDataNew1 ed in periods)
            {
                for (int i=0; i<ed.valid_costs.Count; i++)
                {
                    if( Is.ValidF(ed.valid_kwhs[i]))
                    {
                        cost += ed.valid_costs[i];
                        count++;
                    }
                }
            }
            if (count == 0)
                return Is.UntouchedF;
            return cost / count;
        }
        
        static void calcOthers(out List<float> e, out List<float> c,
            out List<float> ew, out List<float> cw,
            out List<float> em, out List<float> cm)
        {
            e = new List<float>();
            c = new List<float>();
            em = new List<float>();
            cm = new List<float>();
            ew = new List<float>();
            cw = new List<float>();
            if (map.Count == 0)
                return;

            int week = 0;
            int month = 0;
            int day = 0;
            int idx = 0;
            int idxm = 0;
            int idxw = 0;
            for (int i = 0; i < map.Count; i++)
            {
                DateTime pointer = IndexToHour(i, GlobalUntil);
                if( day != pointer.Day )
                {
                    day = pointer.Day;
                    e.Add(0);
                    c.Add(0);
                    idx = e.Count - 1;
                }
                if( week != DT.WhichWeekOfYear(pointer) )
                {
                    week = DT.WhichWeekOfYear(pointer);
                    ew.Add(0);
                    if (costs.Count != 0)
                        cw.Add(0);
                    idxw = ew.Count - 1;
                }
                if( month != pointer.Month )
                {
                    month = pointer.Month;
                    em.Add(0);
                    if(costs.Count != 0)
                        cm.Add(0);
                    idxm = em.Count - 1;
                }
                if (map[i] == 0)
                    continue;

                if (!Is.ValidF(e[idx]))
                {
                    e[idx] = 0;
                    if (costs.Count != 0)
                        c[idx] = 0;
                }
                if (!Is.ValidF(em[idxm]))
                {
                    em[idxm] = 0;
                    if (costs.Count != 0)
                        cm[idxm] = 0;
                }
                if (!Is.ValidF(ew[idxw]))
                {
                    ew[idxw] = 0;
                    if (costs.Count != 0)
                        cw[idxw] = 0;
                }

                e[idx] += map[i];
                if( idx < costs.Count )
                    c[idx] += costs[i];
                em[idxm] += map[i];
                if (idxm < costs.Count)
                    cm[idxm] += costs[i];
                ew[idxw] += map[i];
                if (idxw < costs.Count)
                    cw[idxw] += costs[i];
            }
        }
        static int TimeToIndex(DateTime t)
        {
            if (map.Count == 0)
                return -1;
            if (t < GlobalSince || t > DT.DateCeiling(GlobalUntil))
                return -1;
            TimeSpan ts = DT.DateCeiling(GlobalUntil) - t;
            int days = (int)ts.TotalDays;
            int hours = t.Hour;
            
            int idx = days * 24 + hours;
            if (idx < 0 || idx > map.Count)
                return -1;
            return idx;
        }
        public static float QueryHourlyEnergy(DateTime t)
        {
            if (t < GlobalSince || t > DT.DateCeiling(GlobalUntil))
                return Is.UntouchedF;
            int idx = TimeToIndex(t);
            if (idx == -1)
                return Is.UntouchedF;
            return map[idx];
        }
        public static float QueryHourlyCost(DateTime t)
        {
            if (t < GlobalSince || t > DT.DateCeiling(GlobalUntil))
                return Is.UntouchedF;
            int idx = TimeToIndex(t);
            if (idx == -1 || idx >= costs.Count)
                return Is.UntouchedF;
            return costs[idx];
        }
        static int DayIndex(DateTime t )
        {
            int idx = TimeToIndex(t);
            if (idx == -1)
                return -1;
            return idx / 24;
        }
        public static float QueryDailyEnergy(DateTime t)
        {
            int idx = DayIndex(t);
            if (idx == -1)
                return Is.UntouchedF;
            return daily_e[idx];
        }

        public static float QueryDailyCost(DateTime t)
        {
            int idx = DayIndex(t);
            if (idx == -1 || idx >= costs.Count)
                return Is.UntouchedF;
            return daily_c[idx];
        }
        static int MonthlyIndex(DateTime t )
        {
            if (t < GlobalSince || t > GlobalUntil)
                return -1;
            int month = DT.MonthDistance(t, GlobalUntil);
            if (month >= monthly_e.Count)
                return -1;
            return month;
        }
        public static float QueryMonthlyEnergy(DateTime t)
        {
            int idx = MonthlyIndex(t);
            if (idx == -1)
                return Is.UntouchedF;
            return monthly_e[idx];
        }
        public static float QueryMonthlyCost(DateTime t)
        {
            int idx = MonthlyIndex(t);

            if (idx == -1|| idx >= monthly_c.Count)
                return Is.UntouchedF;
            return monthly_c[idx];
        }
        static int WeeklyIndex(DateTime t)
        {
            if (t < GlobalSince || t > GlobalUntil)
                return -1;
            int week = DT.WeekDistance(t, GlobalUntil);
            if (week >= weekly_e.Count)
                return -1;
            return week;
        }


        // 画月度曲线用
        public static List<List<float>> CurveMonthsAverage(
            int year,
            out List<TimeValuePair> max)
        {
            max = null;
            if (map.Count == 0)
                return null;
            max = new List<TimeValuePair>();
            List<List<float>> lst = new List<List<float>>();
            List<List<int>> cnt = new List<List<int>>();
            for (int i = 0; i < 12; i++)
            {
                lst.Add(new List<float>());
                cnt.Add(new List<int>());
                max.Add(new TimeValuePair(DateTime.MinValue, float.MinValue));
                for (int j = 0; j < 24; j++)
                {
                    lst[i].Add(0);
                    cnt[i].Add(0);
                }
            }

            DateTime pointer;// = GlobalFirst;
            for (int i = 0; i < map.Count; i++)
            {
                pointer = IndexToHour(i, GlobalUntil);
                if (pointer.Year != year)
                    continue;
                if ( 0!= map[i] )
                {
                    int idx1, idx2;
                    idx1 = (int)pointer.Month - 1;
                    idx2 = pointer.Hour;
                    lst[idx1][idx2] += map[i];
                    cnt[idx1][idx2]++;

                    //                     if( max[idx1].value < GlobalMap[i] )
                    //                     {
                    //                         max[idx1].value = GlobalMap[i];
                    //                         max[idx1].when = pointer;
                    //                     }
                }

                //                 if (IsEndOfDay(i))
                //                 {
                //                     pointer = DT.DateFloor(pointer);
                //                     pointer -= DT.OneDay;
                //                 }
                //                 else
                //                     pointer += DT.OneHour;
            }

            // 计算平均值
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    if (cnt[i][j] != 0)
                        lst[i][j] /= cnt[i][j];
                }
            }

            for (int i = 0; i < max_instant_power.Count; i++ )
            {
                if( year == max_instant_power[i].when.Year )
                {
                    max[max_instant_power[i].when.Month-1] = max_instant_power[i];
                }
            }
//             // 最大功率值
//             for (int i = 0; i < GlobalMaxInstantMonthly.Count; i++)
//             {
//                 TimeValuePair pair = GlobalMaxInstantMonthly[i];
//                 DateTime when = pair.when;
//                 float value = pair.value;
//                 if (DT.IsStrictlyValid(when))
//                 {
//                     if (when.Year == year)
//                     {
//                         if (value > max[when.Month - 1].value)
//                         {
//                             max[when.Month - 1] = pair;
//                         }
//                     }
//                 }
//             }

            return lst;
        }


        public static List<List<float>> CurveWeekdayAverage(int year/*周数据不区别年份*/)
        {
            if (map.Count == 0)
                return null;
            List<List<float>> lst = new List<List<float>>();
            List<List<int>> count = new List<List<int>>();
            for (int i = 0; i < 7; i++)
            {
                lst.Add(new List<float>());
                count.Add(new List<int>());
                for (int j = 0; j < 24; j++)
                {
                    lst[i].Add(Is.UntouchedF);
                    count[i].Add(0);
                }
            }

            DateTime pointer;// = GlobalFirst;
            for (int i = 0; i < map.Count; i++)
            {
                pointer = IndexToHour(i, GlobalUntil);
                // 周数据不区别年份
                //if (year != pointer.Year)
                //    continue;
                if (0 != map[i] )
                {
                    int idx1, idx2;
                    idx1 = (int)pointer.DayOfWeek;
                    idx2 = pointer.Hour;
                    if (Is.ValidF(lst[idx1][idx2]))
                        lst[idx1][idx2] += map[i];
                    else
                        lst[idx1][idx2] = map[i];
                    count[idx1][idx2]++;
                }

                //                 if (IsEndOfDay(i))
                //                 {
                //                     pointer = DT.DateFloor(pointer);
                //                     pointer -= DT.OneDay;
                //                 }
                //                 else
                //                     pointer += DT.OneHour;
            }

            // 计算平均值
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 24; j++)
                {
                    if (count[i][j] != 0)
                        lst[i][j] /= count[i][j];
                }
            }
            return lst;
        }

        public static FileInfo[] Tariffs()
        {
            DirectoryInfo di = new DirectoryInfo(REPOSITORY_PATH);
            if (!di.Exists)
                return null;
            FileInfo[] AllTariffs = di.GetFiles("*.xml");
            return AllTariffs;
        }
        #endregion
    }
}
