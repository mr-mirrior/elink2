#define MSGBOXS1

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace Elink
{
    public static class FloatList
    {
        public static float First(List<float> lst)
        {
            return lst[0];
        }
        public static float Last(List<float> lst)
        {
            return lst[lst.Count - 1];
        }
        public static float Max(List<float> lst)
        {
            if(lst.Count == 0 )
                return Is.UntouchedF;
            float mx = float.MinValue;
            for (int i = 0; i < lst.Count; i++ )
            {
                if( mx < lst[i] && Is.ValidF(lst[i]) )
                {
                    mx = lst[i];
                }
            }
            return mx;
        }
        public static float Sum(List<float> lst)
        {
            if (lst.Count == 0)
                return Is.UntouchedF;
            float sum = 0;
            foreach (float f in lst)
            {
                if( Is.ValidF(f) )
                    sum += f;
            }
            return sum;
        }
        public static int ValidCount(List<float> lst)
        {
            if (lst.Count == 0)
                return -1;
            int count = 0;
            for (int i = 0; i < lst.Count; i++ )
            {
                if (Is.ValidF(lst[i]))
                    count++;
            }
            return count;
        }
        public static float Average(List<float> lst)
        {
            if (lst.Count == 0)
                return Is.UntouchedF;
            float sum = 0;
            int count = 0;
            foreach (float f in lst)
            {
                if (Is.ValidF(f))
                {
                    sum += f;
                    count++;
                }
            }
            if( count == 0 )
                return Is.UntouchedF;
            return sum/count;
        }
    }
    public static class Is
    {
        public static bool ValidF(float x)
        {
            return (!float.IsNaN(x));
        }
        public static bool ValidF(double x)
        {
            return ValidF((float)x);
        }
        public static float UntouchedF { get { return float.NaN; } }
    }

    public class EnergyData: ICloneable
    {
        public static DataFromE2 devdata = new DataFromE2();
        public static bool IsCurrentPresent()
        {
            return C != null;
        }
        public static EnergyData C
        {
            get
            {
                EnergyData ed = null;
                if (history.Count == 0)
                {
                    ed = new EnergyData(Settings.I.advTariff);
                    history.Add(ed);
                }
                else
                {
                    foreach (EnergyData e in history)
                    {
                        if (e.IsCurrent())
                            return e;
                    }
                }
                return ed;
            }
        }
        public static Tariff T
        {
            get
            {
                return C.t;
            }
            set
            {
                if (IsCurrentPresent())
                {
                    C.t = value;
                    //C.t.SaveToXML(C.TariffFile.FullName);
                }
            }
        }
        public static EnergyData Find(string name)
        {
            foreach (EnergyData e in history)
            {
                if (e.tariff_name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return e;
            }
            return null;
        }
        public static List<EnergyData> history = new List<EnergyData>();
        
        public Tariff t = new Tariff();
        //public DateTime valid_until_e2 = DateTime.MaxValue;
        //public DateTime valid_until_pc = DateTime.MaxValue;
        // in universal format
        public TwoWorld valid_until = new TwoWorld(DateTime.MaxValue, DateTime.MaxValue);
        // in universal format / only pc time
        public TwoWorld valid_since = new TwoWorld(DateTime.MinValue, DateTime.MinValue);
        public FileInfo DataSheetFile;
        public FileInfo TariffFile;
        public static event EventHandler OnChange;
        public static TwoWorld last_valid_e2 = new TwoWorld();

//         static public FileInfo[] AllTariffs;

        public string tariff_name;
        public List<float> kwh_hourly = new List<float>();
        public List<float> kwh_daily = new List<float>();
        public List<float> kwh_weekly = new List<float>();
        public List<float> kwh_monthly = new List<float>();
        public List<float> kw_max_monthly = new List<float>();
        public List<DateTime> when_kw_max_monthly = new List<DateTime>();

        private static void OnChangeDummy(object sender, EventArgs e)
        {

        }
        public void Rename(string newname)
        {
            string csv = Path.Combine(REPOSITORY_PATH, newname);
            string xml = csv;
            csv = Path.ChangeExtension(csv, "csv");
            xml = Path.ChangeExtension(xml, "xml");

            string oldcsv = Path.Combine(REPOSITORY_PATH, this.t.tariff_name);
            string oldxml = oldcsv;
            oldcsv = Path.ChangeExtension(oldcsv, "csv");
            oldxml = Path.ChangeExtension(oldxml, "xml");

            FileInfo fi1 = new FileInfo(csv);
            FileInfo fi2 = new FileInfo(xml);

            FileInfo fi3 = new FileInfo(oldcsv);
            FileInfo fi4 = new FileInfo(oldxml);

            if (fi3.Exists)
                fi3.MoveTo(fi1.FullName);
            if (fi4.Exists)
                fi4.MoveTo(fi2.FullName);
            tariff_name = newname;
            t.tariff_name = newname;
            SaveAllCSVAndXML();
        }
        public object Clone()
        {
            EnergyData x = (EnergyData) this.MemberwiseClone();
            x.valid_since = (TwoWorld)this.valid_since.Clone();
            x.valid_until = (TwoWorld)this.valid_until.Clone();
            return x;
        }
        public override string ToString()
        {
            return t.tariff_name + " " + valid_until.ToString();
        }
        public const string REPOSITORY_PATH = "Repository";
        public const string DATA_E2_PATH = "DataE2";
        public EnergyData(Tariff tt)
        {
            t = (Tariff)tt.Clone();
            valid_since = new TwoWorld(DateTime.MinValue, DateTime.MinValue);
            OnChange -= OnChangeDummy;
            OnChange += OnChangeDummy;
        }
        public bool IsCurrent()
        {
            return t.SameName(Settings.I.advTariff);//this.t.Equals(Settings.I.advTariff);
        }
        public static bool Empty()
        {
            return history.Count == 0;
        }
        static void ClearGlobals()
        {
            //AllTariffs = null;

            GlobalBegin = DateTime.MaxValue;
            GlobalEnd = DateTime.MinValue;
            GlobalMap = new List<float>();
            GlobalCost = new List<float>();

            GlobalDaily = new List<float>();
            GlobalWeekly = new List<float>();
            GlobalMonthly = new List<float>();
            GlobalYearly = new List<float>();

            GlobalCostDaily = new List<float>();
            GlobalCostWeekly = new List<float>();
            GlobalCostMonthly = new List<float>();
            GlobalCostYearly = new List<float>();

            GlobalMax = 0;
            GlobalMaxDaily = 0;
            GlobalMaxWeekly = 0;
            GlobalMaxMonthly = 0;
            GlobalMaxYearly = 0;

            GlobalMaxCost = 0;
            GlobalMaxCostDaily = 0;
            GlobalMaxCostWeekly = 0;
            GlobalMaxCostMonthly = 0;
            GlobalMaxCostYearly = 0;
            //costValid = false;
        }
        public static bool Load()
        {
            history.Clear();
            ClearGlobals();
            if( OnChange != null )
                OnChange.Invoke(null, null);

            //return LoadDeviceDataCSV(CSV_FILE);
            DirectoryInfo di = new DirectoryInfo(REPOSITORY_PATH);
            if (!di.Exists)
            {
                Directory.CreateDirectory(REPOSITORY_PATH);
                return false;
            }
            FileInfo[] data = di.GetFiles("*.csv");
            FileInfo[] tariff = di.GetFiles("*.xml");

#if MSGBOXS
            MB.OKI(string.Format("Found {0} CSV and {1} tariffs", data.Length, tariff.Length));
#endif
            //AllTariffs = tariff;
            if (data.Length == 0)
                return false;
            foreach (FileInfo fi in data)
            {
                string tariffname = Path.ChangeExtension(fi.FullName, "xml");
                //fi.FullName.TrimEnd(new char[] { 'c','s','v', 'C','S','V'});
                //tariffname += "xml";

                FileInfo tarifffile = new FileInfo(tariffname);
                Tariff adv = Tariff.LoadTariffXML(tarifffile.FullName);
#if MSGBOXS
                MB.OKI("Load tariff " + tarifffile.FullName + " result " + (adv == null ? "NULL" : "OK"));
#endif
                if (adv == null)
                    continue;
                EnergyData p = new EnergyData(adv);
                if (!p.LoadDeviceDataCSV(fi.FullName))
                {
#if MSGBOXS
                    MB.OKI("Load CSV failed");
#endif
                    continue;
                }
                p.DataSheetFile = fi;
                p.TariffFile = tarifffile;
                p.tariff_name = adv.tariff_name;
                history.Add(p);
            }

            if(IsCurrentPresent())
            {
                Settings.I.advTariff = (Tariff)C.t.Clone();
                Settings.BroadcastChange();
            }
            if( history.Count == 0 ||
                !IsCurrentPresent())
            {
#if MSGBOXS
                MB.Warning("Current is absent");
#endif
                history.Insert(0, new EnergyData(Settings.I.advTariff));
            }
            MakeGlobalMap();

            if (OnChange != null)
                OnChange.Invoke(null, null);

            return true;
        }
        public static bool SaveAllCSVAndXML()
        {
            //if (!Merge())
            //    return;
            //return SaveAllCSVAndXML(CSV_FILE);
            DirectoryInfo di = new DirectoryInfo(REPOSITORY_PATH);
            if( di.Exists )
            {
                FileIO.DirectoryCopy(REPOSITORY_PATH, REPOSITORY_PATH + "Backup", true, true);
            }
            foreach (EnergyData e in history)
            {
                //e.t.valid_since = e.valid_since;
                //e.t.valid_until = e.valid_until;
//                 if (e.IsCurrent())
//                 {
//                     e.Save(EnergyData.PATH + "\\Current.csv");
//                 }
//                 else
//                 {
                    string path = EnergyData.REPOSITORY_PATH + "\\";
                    if (e.Save(path + e.t.tariff_name + ".csv"))
                        e.t.SaveToXML(path + e.t.tariff_name + ".xml");
//                 }
            }
            return true;
        }
        public static void NewPair(Tariff oldtariff)
        {
            TwoWorld tw = last_valid_e2;
            TwoWorld now;
            if( !tw.IsValid )
            {
                tw = new TwoWorld(DateTime.Now, DateTime.Now);
                now = tw;
            }
            else
            {
                now = new TwoWorld(last_valid_e2.PC2E2Time(DateTime.Now), DateTime.Now);
            }
//             if( !IsCurrentPresent() )
//             {
            EnergyData newone = new EnergyData(Settings.I.advTariff);
            newone.valid_since = now;
            newone.valid_until = now;
            history.Insert(0, newone);
//             }
            EnergyData.SaveAllCSVAndXML();
        }

        private void ClearAll()
        {
            kwh_hourly.Clear();
            kwh_daily.Clear();
            kwh_weekly.Clear();
            kwh_monthly.Clear();
            kw_max_monthly.Clear();
            when_kw_max_monthly.Clear();
            valid_kwhs.Clear();
            valid_costs.Clear();
            valid_carbons.Clear();
            //valid_until = new TwoWorld(DateTime.MaxValue, DateTime.MaxValue);
            //valid_since = new TwoWorld(DateTime.MinValue, DateTime.MinValue);
            valid_years = new List<int>();
            valid_months = new List<DateTime>();
            GlobalMaxInstantMonthly.Clear();
        }
        public void Reserve(int count)
        {
            kwh_hourly.Capacity = count;
            kwh_daily.Capacity = count;
            kwh_weekly.Capacity = count;
            kwh_monthly.Capacity = count;
            kw_max_monthly.Capacity = count;
        }
//         public bool Backup(string csvfile)
//         {
//             //backup_time = DateTime.Now;
//             return Save(csvfile);
//         }
        private void Add<T>(List<T> lst, int at, T x, T def)
        {
            //if (at != 0)
            {
                while (lst.Count != at)
                {
                    lst.Add(def);
                }
            }
            lst.Add(x);
        }
        public bool LoadDeviceDataCSV(string file)
        {
            CSV bk = new CSV();
            bk.Load(file);
            if (bk.lines == null)
            {
#if MSGBOXS
                MB.Warning("file is empty "+file);
#endif
                return false;
            }
            if (bk.lines.Count < 2)
            {
#if MSGBOXS
                MB.Warning("file has less than 2 lines " + file);
#endif
                return false;
            }

            ClearAll();
            string[] time = bk.lines[1];
            valid_until = new TwoWorld(DateTime.MaxValue, DateTime.MaxValue);
            valid_since = new TwoWorld(DateTime.MinValue, DateTime.MinValue);
            try
            {
                valid_since = new TwoWorld(DateTime.Parse(time[0]), 
                    DateTime.Parse(time[1]));
                valid_until = new TwoWorld(DateTime.Parse(time[2]), 
                    DateTime.Parse(time[3]));
                valid_since.ToLocal();
                valid_until.ToLocal();
                end_day_time = valid_until.E2Time;
                last_valid_e2 = new TwoWorld(valid_since);
                //tariff_name = time[3];
                //backup_time = DateTime.Parse(time[1]);
            }
            catch (System.Exception)
            {
#if MSGBOXS
                MB.Error("Date parse error at file " + file);
#endif
                return false;
            }
            if( !valid_since.IsValid ||
                !valid_until.IsValid )
            {
#if MSGBOXS
                MB.Error("Date error at file " + file);
#endif
                return true;
                //return false;
            }

            if (!valid_since.IsValid &&
                !valid_until.IsValid )
                if (valid_since.E2Time >= valid_until.E2Time)
                {
#if MSGBOXS
                    MB.Warning("file dates invalid " + file + "\r\n"+
                        valid_since.ToString() + " " + valid_until.ToString());
#endif
                    return false;
                }
            //Reserve(bk.lines.Count);
            CultureInfo ci = new CultureInfo("en-US");
            for (int i = 2; i < bk.lines.Count; i++)
            {
                string[] s = bk.lines[i];
                int idx = 1;
                // 每小时电量
                if (s[idx].Length != 0)
                {
                    float f = float.Parse(s[idx], ci);
                    //max_kwh_hourly = Math.Max(max_kwh_hourly, f);
                    Add<float>(kwh_hourly, i - 2, f, -1);
                }
                else
                    kwh_hourly.Add(-1);
                if(++idx >=s.Length) continue;

                // 每月最大即时功率
                if (s[idx].Length != 0)
                    Add<float>(kw_max_monthly, i - 2, float.Parse(s[idx], ci), -1);
                if (++idx >= s.Length) continue;

                // 每月最大即时功率发生的时间
                if (s[idx].Length != 0)
                {
                    DateTime dt;
                    if( DateTime.TryParse(s[idx], out dt) )
                        Add<DateTime>(when_kw_max_monthly, i - 2, 
                            dt, DateTime.MinValue);
                }
                if (++idx >= s.Length) continue;

                if (s[idx].Length != 0)
                    Add<float>(kwh_daily, i - 2, float.Parse(s[idx], ci), -1);
                if (++idx >= s.Length) continue;

                if (s[idx].Length != 0)
                    Add<float>(kwh_weekly, i - 2, float.Parse(s[idx], ci), -1);
                if (++idx >= s.Length) continue;

                if (s[idx].Length != 0)
                    Add<float>(kwh_monthly, i - 2, float.Parse(s[idx], ci), -1);
            }
            System.Diagnostics.Debug.Print("Counts: {0},{1},{2},{3},{4}",
                kwh_hourly.Count,
                kwh_daily.Count,
                kwh_weekly.Count,
                kwh_monthly.Count,
                kw_max_monthly.Count);

#if MSGBOXS
            MB.OKI("Loaded done " + file);
#endif
            Statistics();
#if MSGBOXS
            MB.OKI("Statistics done " + file);
#endif
            return true;
        }
        public void Simulate(string file)
        {
            devdata = 
                XMLUtil<DataFromE2>.LoadXml(file);
            if (devdata == null)
                return;
            Merge();
        }
        public void SavePreservedData()
        {
            DateTime e2 = valid_until.E2Time;
            string file = string.Format("{0}.{1}.{2} {3}.{4}.{5}.xml",
                e2.Year, e2.Month, e2.Day,
                e2.Hour, e2.Minute, e2.Second);
            devdata.tariff = C.TariffName();
            XMLUtil<DataFromE2>.SaveXml(
                Path.Combine(DATA_E2_PATH, file), devdata);
        }
        private static DateTime IndexToHour(int idx, DateTime latest_day1)
        {
            DateTime dt = DT.DateFloor(latest_day1);
            if (!DT.IsStrictlyValid(dt))
                return DateTime.MinValue;

            int days = idx/24;
            int hours = idx % 24;
//             if (idx !=0 && (hours == 0) )
//             {
//                 days++;
//                 hours = 0;
//             }
            return (DT.DateFloor(dt) - DT.Days(days) + DT.Hours(hours));
        }
        public int Hour2Index(DateTime hour)
        {
            return -1;
        }
        public void MergeHourly()
        {
            if (!IsCurrent())
                return;
            // 新数据长度 240 x 24 = 5760 最晚时间
            // 旧数据长度 N x 24          最早时间
            // 合并后数据长度：
            //   最早时间 ～ 最晚时间 小时数
            if( !DT.IsStrictlyValid(valid_until.E2Time) ||
                !DT.IsStrictlyValid(devdata.now_in_E2) )
            {
                //MB.Error("Invalid dates found while merging data! Aborting.");
                return;
            }
            float[] newdata = devdata.kwh_240days;
            List<float> olddata = kwh_hourly;

            // 新数据：刚刚从E2读取的数据
            // 新数据最晚时间
            DateTime newlate = DT.DateFloor(devdata.now_in_E2);
            DateTime newboundary = newlate + DT.OneDay;
            // 新数据最早时间, 最晚时间之前的240天
            DateTime newearly = newlate - DT.Days(240);
            // 旧数据：CSV中的数据
            // 旧数据最晚时间
            DateTime oldlate = DT.DateFloor(valid_until.E2Time);
            DateTime oldboundary = oldlate + DT.OneDay;
            // 旧数据最早时间
            DateTime oldearly = oldlate - DT.Days(kwh_hourly.Count / 24);
            if (kwh_hourly.Count == 0)
                oldearly = newearly;
            // 合并后最晚时间
            DateTime late = newlate;
            // 合并后最早时间
            DateTime early = oldearly;

            // 合并后记录数目
            int count = (int)(late - early).TotalHours;

            // 时间指针
            DateTime pointer = late;
            int idx1 = 0, idx2 = 0;

            List<float> lst = new List<float>(new float[count]);
            for (int i = 0; i < count; i++ )
            {
                pointer = IndexToHour(i, newlate);
                // 初始值
                lst[i] = -1;
                // 优先放新数据
                // newboundary非常重要，它本身是无效时间，只用于判断时间指针是否合法
                // added 2008-09-05 11:41:00
                // 有效时间之前不应该受到影响
                if (pointer >= valid_since.E2Time && // added 2008-09-05 11:41:00
                    (pointer < newboundary && pointer >= newearly) )
                {
                    if (idx1 < newdata.Length)
                        lst[i] = newdata[idx1];
                }
                else // 否则找旧数据
                {
                    if( pointer <oldboundary && pointer >= oldearly )
                    {
                        //idx2 = Hour2Index(pointer);
                        if (idx2>=0 && idx2 < olddata.Count)
                        {
                            lst[i] = olddata[idx2];
                        }
                    }
                }
                idx1++;
                if (pointer < oldboundary)
                    idx2++;
            }

            kwh_hourly = lst;

//             if (!DT.IsValid(devdata.save_time) ||
//                 !DT.IsValid(valid_until))
//                 return;
// 
// 
//             float[] newdata = devdata.kwh_240days;
//             List<float> olddata = kwh_hourly;
//             DateTime newend = DT.DateFloor(devdata.save_time);
//             DateTime newbegin = newend - new TimeSpan(240, 0, 0, 0);
//             DateTime oldend = DT.DateFloor(valid_until);
//             DateTime oldbegin = oldend - new TimeSpan(kwh_hourly.Count / 24, 0, 0, 0);
// 
//             List<float> newlist = new List<float>();
//             TimeSpan onehour = new TimeSpan(1, 0, 0);
//             // 初始值是最早的时间，倒序
//             DateTime pointer = (oldbegin<newbegin)?oldbegin:newbegin;
//             int newtail = newdata.Length - 1;
//             int oldtail = olddata.Count - 1;
//             // 以时间为线索，步长为小时
//             while(pointer < newend)
//             {
//                 // 优先在最新的数据中获取
//                 if( pointer >= newbegin &&
//                     pointer <= newend )
//                     newlist.Add(newdata[newtail--]);
//                 else
//                 {
//                     // 新数据中没有的部分，从原有数据中获取
//                     if (pointer >= oldbegin &&
//                         pointer <= oldend)
//                         newlist.Add(olddata[oldtail--]);
//                     else
//                     {
//                         newlist.Add(-1);
//                     }
//                 }
//                 pointer += onehour;
//             }
//             // 倒过来
//             newlist.Reverse();
//             kwh_hourly = newlist;
        }
        public void MergeInstantMaxPowerMonthly()
        {
            float[] newpower = devdata.max_power_13months;
            List<float> oldpower = kw_max_monthly;
#if NEW_FORMAT_MAX_POWER
            int[] newpowertime = devdata.when_max_power_13months;
            List<DateTime> oldpowertime = when_kw_max_monthly;
#endif
            
            // 新数据最晚时间
            DateTime newlate = DT.MonthFloor(devdata.now_in_E2);
            DateTime newboundary = DT.MonthCeiling(newlate);
            // 新数据最早时间, 最晚时间之前的13个月
            DateTime newearly = DT.BeforeMonths(newlate, 13);
            // 旧数据最晚时间
            DateTime oldlate = DT.MonthFloor(valid_until.E2Time);
            DateTime oldboundary = DT.MonthCeiling(oldlate);
            // 旧数据最早时间
            DateTime oldearly = DT.BeforeMonths(oldlate, kw_max_monthly.Count);
            if (kw_max_monthly.Count == 0)
                oldearly = newearly;

            // 合并后最晚时间
            DateTime late = newlate;
            // 合并后最早时间
            DateTime early = oldearly;

            int count = this.when_kw_max_monthly.Count;//DT.MonthDistance(early, late);
            count = Math.Max(count, devdata.when_max_power_13months.Length);
            DateTime pointer = late;
            List<float> lst = new List<float>(new float[count]);
            if( oldpowertime.Count < count )
            {
                oldpowertime.AddRange(new DateTime [count - oldpowertime.Count]);
            }
            List<DateTime> powertime = new List<DateTime>(new DateTime[count]);
            int idx1 = 0, idx2 = 0;

            for (int i = 0; i < lst.Count; i++ )
            {
                lst[i] = 0;
                // added 2008-09-05 11:41:00
                // 有效时间之前不应受到影响
                if( pointer >= valid_since.E2Time && // added 2008-09-05 11:41:00
                    pointer < newboundary && pointer >= newearly )
                {
                    if (idx1 < newpower.Length)
                    {
                        lst[i] = newpower[idx1];
                        if (newpowertime[idx1] != 0)
                        {
                            try
                            {
                                powertime[i] = new DateTime(
                                    pointer.Year,
                                    pointer.Month,
                                    BitOp.Byte2Int(newpowertime[idx1]), // day
                                    BitOp.Byte1Int(newpowertime[idx1]), // hour
                                    BitOp.Byte0Int(newpowertime[idx1]), // minute
                                    0,                    // second
                                    DateTimeKind.Local);
                            }
                            catch (Exception)
                            {
                                MB.Warning("Merging data: max power time invalid.");
                            }
                        }
                    }
                }
                else
                {
                    if( pointer <oldboundary && pointer >= oldearly )
                    {
                        if (idx2 < oldpower.Count)
                            lst[i] = oldpower[idx2];
                        if (idx2 < oldpowertime.Count)
                            powertime[i] = oldpowertime[idx2];
                    }
                }
                pointer = DT.PrevMonth(pointer);

                idx1++;
                if (pointer < oldboundary)
                    idx2++;
            }

            kw_max_monthly = lst;
            when_kw_max_monthly = powertime;
        }
        //public void UpdateSaveTime()
        //{
        //    valid_until = new TwoWorld(
        //        devdata.now_in_E2, 
        //        devdata.now_in_PC);
        //    //t.valid_until = devdata.now_in_E2;
        //}
        public void UpdateValidTime()
        {
            DateTime late1 = devdata.now_in_E2;
            DateTime late2 = devdata.now_in_PC;
            DateTime early1 = late1 - DT.Days(240);
            DateTime early2 = late2 - DT.Days(240);
            bool clear = false;
            if (!DT.IsStrictlyValid(valid_since.E2Time))
            {
                valid_since = new TwoWorld(early1, early2);
                clear = true;
            }
            //if( !DT.IsStrictlyValid(valid_until.E2Time ))
            {
                valid_until = new TwoWorld(late1, late2);
                valid_since = new TwoWorld(
                    valid_until.PC2E2Time(valid_since.PCTime),
                    valid_since.PCTime);
                //clear = true;
            }
            if (clear)
                ClearAll();

            last_valid_e2 = new TwoWorld(late1, late2);
        }
        public bool Merge()
        {
            //XMLUtil.SaveXml("devicedata.xml", adv);
            if (devdata == null)
                return false;

            UpdateValidTime();
            // this means no previous adv found
            // best case :)
//             if (!DT.IsStrictlyValid(valid_until))
//             {
//                 kwh_hourly.AddRange(devdata.kwh_240days);
//                 kwh_daily.AddRange(devdata.kwh_8days);
//                 kwh_weekly.AddRange(devdata.kwh_8weeks);
//                 kwh_monthly.AddRange(devdata.kwh_25months);
//                 kw_max_monthly.AddRange(devdata.max_power_13months);
//                 valid_until = devdata.save_time;
//                 //SaveAllCSVAndXML();
//                 return true;
//             }
//             else
            {
                //DateTime oldt = valid_until;
                //DateTime newt = devdata.save_time;
                //List<float> hourly = new List<float>();
                //List<float> daily = new List<float>();
                //List<float> weekly = new List<float>();
                //List<float> monthly = new List<float>();
                //List<float> max_monthly = new List<float>();

                //if (newt < oldt)
                //    return false;
                //if (newt == oldt)
                //    return true;

                //float[] newdata;
                //List<float> olddata;
                //UpdateSaveTime();
                MergeHourly();
                MergeInstantMaxPowerMonthly();


                //// hourly: newt < oldt < new_hourly_btm
                //TimeSpan HOURLY_SPAN = new TimeSpan(240, 0, 0, 0);
                //DateTime new_hourly_btm = newt - HOURLY_SPAN;
                //DateTime old_hourly_btm = oldt - new TimeSpan(kwh_hourly.Count, 0, 0);
                //// 240天内的每小时电量
                //// 0: 今天0:00~01:00，1:今天01:00~02:00... 23:今天23:00~00:00
                //// 24: 昨天0:00~01:00...
                //List<float> olddata = kwh_hourly;
                //float[] newdata = devdata.kwh_240days;
                //if (newt > oldt)
                //    // 从新的列表遍历
                //    hourly.AddRange(devdata.kwh_240days);
                //// 如果旧的数据和新数据有交叉
                //if (oldt > new_hourly_btm && old_hourly_btm < new_hourly_btm)
                //{
                //    // 如果老的数据中 还有 新数据没有的部分
                //    // 继续添加
                //    TimeSpan delta = new_hourly_btm - old_hourly_btm;
                //    int idx = (int)(kwh_hourly.Count - 24 * Math.Floor(delta.TotalDays));
                //    int cnt = 24 * (int)Math.Floor(delta.TotalDays);
                //    hourly.AddRange(kwh_hourly.GetRange(idx, cnt));
                //}
                //else // 旧的数据完全落后于新数据
                //{
                //    if (new_hourly_btm > oldt)
                //    {

                //    }
                //}

                // 日数据合并(8日新数据)
                //newdata = devdata.kwh_8days;
                //olddata = kwh_daily;
                //TimeSpan DAILY_SPAN = new TimeSpan(8, 0, 0, 0, 0);
                //DateTime new_daily_btm = newt - DAILY_SPAN;
                //DateTime old_daily_btm = oldt - new TimeSpan(kwh_daily.Count, 0, 0, 0);
                //if (newt > oldt)
                //    daily.AddRange(newdata);
                //if (old_daily_btm < new_daily_btm)
                //{
                //    TimeSpan delta = new_daily_btm - old_daily_btm;
                //    int idx = (int)(olddata.Count - Math.Floor(delta.TotalDays));
                //    int cnt = (int)Math.Floor(delta.TotalDays);
                //    daily.AddRange(olddata.GetRange(idx, cnt));

                //}
                //else
                //{

                //}

                //// 周数据合并(8周新数据)
                //newdata = devdata.kwh_8weeks;
                //olddata = kwh_weekly;
                //TimeSpan WEEKLY_SPAN = new TimeSpan(8 * 7, 0, 0, 0);
                //DateTime new_weekly_btm = newt - WEEKLY_SPAN;
                //DateTime old_weekly_btm = oldt - new TimeSpan(kwh_weekly.Count * 7, 0, 0, 0);
                //if (newt > oldt)
                //    weekly.AddRange(newdata);
                //if (old_weekly_btm < new_weekly_btm)
                //{
                //    TimeSpan delta = new_weekly_btm - old_weekly_btm;
                //    int idx = (int)(olddata.Count - Math.Floor(delta.TotalDays / 7));
                //    int cnt = (int)Math.Floor(delta.TotalDays / 7);
                //    weekly.AddRange(olddata.GetRange(idx, cnt));

                //}
                //else
                //{

                //}

                //// 月电量合并(25个月新数据)
                //olddata = kwh_monthly;
                //newdata = devdata.kwh_25months;
                //DateTime new_monthly_btm = BeforeMonths(newt, 25);
                //DateTime old_monthly_btm = BeforeMonths(oldt, olddata.Count);
                //if (newt > oldt)
                //    monthly.AddRange(newdata);
                //if (old_monthly_btm < new_monthly_btm)
                //{
                //    //TimeSpan delta = new_monthly_btm-old_monthly_btm;
                //    int totalmonths = TotalMonths(new_monthly_btm.Year, new_monthly_btm.Month,
                //        old_monthly_btm.Year, old_monthly_btm.Month);
                //    int idx = (int)(olddata.Count - totalmonths);
                //    int cnt = (int)totalmonths;
                //    monthly.AddRange(olddata.GetRange(idx, cnt));

                //}
                //else
                //{

                //}

                //// 月最大功率合并(13个月新数据)
                //olddata = kw_max_monthly;
                //newdata = devdata.max_power_13months;
                //DateTime new_max_btm = BeforeMonths(newt, 13);
                //DateTime old_max_btm = BeforeMonths(oldt, olddata.Count);
                //if (newt > oldt)
                //    max_monthly.AddRange(newdata);
                //if (old_max_btm < new_max_btm)
                //{
                //    int totalmonths = TotalMonths(new_max_btm.Year, new_max_btm.Month,
                //        old_max_btm.Year, old_max_btm.Month);
                //    int idx = (olddata.Count - totalmonths);
                //    int cnt = totalmonths;
                //    max_monthly.AddRange(olddata.GetRange(idx, cnt));
                //}

                ////kwh_hourly = hourly;
                //kwh_daily = daily;
                //kwh_weekly = weekly;
                //kwh_monthly = monthly;
                //kw_max_monthly = max_monthly;

                //valid_until = devdata.save_time;
                //t.valid_until = devdata.save_time;

                return true;
            }
        }
        private DateTime BeforeMonths(DateTime from, int months)
        {
            int month = from.Month - (months % 12);
            int year = from.Year - months / 12;
            if (month <= 0)
            {
                year--;
                month += 12;
            }
            return new DateTime(year, month, 1);
        }
        private DateTime AfterMonths(DateTime from, int months)
        {
            int month = from.Month + months;
            int year = from.Year + month / 12;
            month %= 13;
            if (month == 0)
                month = 1;
            return new DateTime(year, month, 1);
        }
        // year1 > year2 (2008, 2005)
        private int TotalMonths(int year1, int month1, int year2, int month2)
        {
            int dyear = year1 - year2;
            int dmonth = month1 - month2;
            if (dyear < 0)
                return 0;
            if (dmonth <= 0)
            {
                dyear--;
                dmonth += 12;
            }
            return dyear * 12 + dmonth;
        }
        public bool Save(string file)
        {
            int mx = kwh_hourly.Count;

            List<string[]> strings = new List<string[]>();
            strings.Add(new string[] {"Time","Hourly kWh","Monthly Max Instant KW", "When Max", "Daily kWh","Weekly kWh",
                "Monthly kWh", "Warning: DO NOT EDIT THIS FILE PLEASE!"});
            
            //valid_since_e2 = t.valid_since;
            //valid_until_e2 = t.valid_until;
            valid_since = new TwoWorld(
                valid_until.PC2E2Time(valid_since.PCTime),
                valid_since.PCTime);

//             valid_since.ToUniversal();
//             valid_until.ToUniversal();

            strings.Add(new string[] { 
                valid_since.E2Time.ToString("o", CultureInfo.InvariantCulture),
                valid_since.PCTime.ToString("o", CultureInfo.InvariantCulture),

                valid_until.E2Time.ToString("o", CultureInfo.InvariantCulture),
                valid_until.PCTime.ToString("o", CultureInfo.InvariantCulture),
                tariff_name, 
                "" 
                });
            if( !DT.IsStrictlyValid(GlobalLatest ) )
            {
                if (last_valid_e2.IsValid)
                    GlobalLatest = last_valid_e2.E2Time;
                else
                    GlobalLatest = DateTime.Today;
            }
            DateTime pointer = DT.DateFloor(GlobalLatest);
            if (!DT.IsValid(pointer))
                mx = 0;

            CultureInfo ci = new CultureInfo("en-US");
            for (int i = 0; i < mx; i++)
            {
                pointer = Index2Hour(i);

                string[] s = new string[6];
                int idx = 0;
                if (i % 24 == 0)
                {
                    s[idx] = string.Format("'{0:00}:00 {1}/{2}/{3:00}",
                    pointer.Hour, pointer.Year, ((Calendar.MonthShort)pointer.Month).ToString(), pointer.Day);
                }
                else
                {
                    s[idx] = string.Format("'{0:00}:00", pointer.Hour);
                }
                idx++;
                if (kwh_hourly[i] != -1)
                    s[idx] = kwh_hourly[i].ToString(ci);
                else
                    s[idx] = "0";
                idx++;
                if (i < kw_max_monthly.Count)
                    if (kw_max_monthly[i] != -1)
                        s[idx] = kw_max_monthly[i].ToString(ci);
                idx++;
                if (i < when_kw_max_monthly.Count)
                    if (when_kw_max_monthly[i] != DateTime.MinValue)
                        s[idx] = when_kw_max_monthly[i].ToString("o", CultureInfo.InvariantCulture);
                idx++;
                if (i < kwh_daily.Count)
                    if (kwh_daily[i] != -1)
                        s[idx] = kwh_daily[i].ToString(ci);
                idx++;
                if (i < kwh_weekly.Count)
                    if (kwh_weekly[i] != -1)
                        s[idx] = kwh_weekly[i].ToString(ci);
                idx++;
                if (i < kwh_monthly.Count)
                    if (kwh_monthly[i] != -1)
                        s[idx] = kwh_monthly[i].ToString(ci);
                idx++;
                strings.Add(s);

//                 if( IsEndOfDay(i) )
//                 {
//                     pointer -= DT.OneDay;
//                     pointer = DT.DateFloor(pointer);
//                 }
//                 else
//                     pointer += DT.OneHour;
            }
            CSV csv = new CSV();
            csv.lines = strings;
            csv.Save(file);
            return true;
        }

        List<float> valid_kwhs = new List<float>();
        List<float> valid_costs = new List<float>();
        List<float> valid_carbons = new List<float>();

        DateTime Index2Hour(int idx)
        {
//             if (time_table_hourly != null)
//                 if (time_table_hourly.Count > idx)
//                     return time_table_hourly[idx];
            return IndexToHour(idx, DT.DateFloor(end_day_time));
//             if (valid_until == DateTime.MaxValue)
//                 return DateTime.MaxValue;
//             DateTime pt = DT.DateFloor(valid_until);
//             int days = idx / 24;
//             pt -= new TimeSpan(days, 0, 0, 0);
//             return pt + new TimeSpan(idx%24, 0, 0);
        }
        public List<int> valid_years = new List<int>();
        public List<DateTime> valid_months = new List<DateTime>();

        public List<DateTime> time_table_hourly = new List<DateTime>();

        DateTime end_day_time = DateTime.MinValue;
        void Statistics()
        {
            if (!valid_until.IsValid)
                return;

            valid_years = new List<int>();
            valid_months = new List<DateTime>();
            time_table_hourly = new List<DateTime>(new DateTime[kwh_hourly.Count]);

            // 首先有效数据初始化为原始表格
            valid_kwhs = new List<float>(kwh_hourly);
            DateTime pointer = DT.DateFloor(end_day_time);
            DateTime validceiling = DT.DateCeiling(valid_until.E2Time);
            TimeSpan onehour = new TimeSpan(1,0,0);
            TimeSpan oneday = new TimeSpan(1, 0, 0, 0);

            int year = -1;
            int month = -1;
            // 逐个比较有效性，若不在有效时间内，则置无效值NaN
            for (int i = 0; i < valid_kwhs.Count; i++ )
            {
                pointer = IndexToHour(i, end_day_time);
                time_table_hourly[i] = pointer;

                // 早于最早时间，或者迟于最晚时间，则无效，置为-1
                if( pointer < validceiling &&
                    pointer >= valid_since.E2Time)
                {
                    // 有效数据
                    if( year != pointer.Year )
                    {
                        year = pointer.Year;
                        valid_years.Add(year);
                    }
                    if( month != pointer.Month )
                    {
                        month = pointer.Month;
                        valid_months.Add(pointer);
                    }
                    if (kwh_hourly[i] == 0)
                        valid_kwhs[i] = Is.UntouchedF;
                }
                else
                {
                    valid_kwhs[i] = Is.UntouchedF;
                }
            }

            // PRICE without overcharge
            valid_costs = new List<float>(valid_kwhs);
            for (int i = 0; i < valid_costs.Count; i++ )
            {
                if (Is.ValidF(valid_costs[i]))
                    valid_costs[i] = 0;
            }

            CalcOvercharge();
        }
        void CalcOvercharge()
        {
            int month = -1;
            //int idx = -1;
            //List<float> ml = new List<float>();

            // 首先计算每个有效月份的总电量，
            // 以判断是否超过限额
//             for (int i = 0; i < valid_kwhs.Count; i++ )
//             {
//                 DateTime now = Index2Hour(i);
//                 if( month != now.Month )
//                 {
//                     month = now.Month;
//                     idx++;
//                     ml.Add(0);
//                 }
//                 else
//                 {
//                 }
//                 if (Is.ValidF(valid_kwhs[i]))
//                 {
//                     //if (float.IsNaN(ml[idx]))
//                     //    ml[idx] = 0;
//                     ml[idx] += valid_kwhs[i];
//                 }
//             }
            month = -1;
            //idx = -1;
            // 再根据每月实际耗电量，比较限额
            // 计算超出的部分，并计费
            float accu = 0;
            int idx = valid_kwhs.Count;
            while(true)
            {
                idx -= 24;
                if (idx < 0)
                    break;
                for(int i=0; i<24; i++ )
                {
                    int thisidx = i + idx;
                    float value = valid_kwhs[thisidx];
                    if (!Is.ValidF(value) || value < 0)
                        continue;
                    DateTime thistime = Index2Hour(thisidx);
                    if( month != thistime.Month )
                    {
                        month = thistime.Month;
                        accu = 0;
                    }
                    accu += value;
                    valid_costs[thisidx] = t.HourlyFee(thistime, value, (accu >= t.over_limit) ? t.over_charge : 0);
                }
            }
//             for (int i = valid_kwhs.Count-1; i >= 0; i-=24)
//             {
//                 if (!Is.ValidF(valid_kwhs[i]) || valid_kwhs[i]<0)
//                     continue;
//                 DateTime thistime = Index2Hour(i);
//                 if (month != thistime.Month)
//                 {
//                     month = thistime.Month;
//                     //idx++;
//                     accu = 0;
//                 }
// 
//                 accu += valid_kwhs[i];
//                 valid_costs[i] = t.HourlyFee(thistime, valid_kwhs[i], (accu>=t.over_limit)?t.over_charge:0);
//             }
        }
//         static bool IsEndOfDay(int i)
//         {
//             return ((i + 1) % 24) == 0;
//         }
        static DateTime GlobalBegin = DateTime.MaxValue;
        static DateTime GlobalEnd = DateTime.MinValue;
        static DateTime GlobalFirst = DateTime.MinValue;
        static DateTime GlobalEarliest = DateTime.MaxValue;
        static DateTime GlobalLatest = DateTime.MinValue;
        static List<float> GlobalMap = new List<float>();
        static List<DateTime> GlobalTimeMap = new List<DateTime>();
        static List<float> GlobalCost = new List<float>();
        static List<TimeValuePair> GlobalMaxInstantMonthly = new List<TimeValuePair>();
        static EnergyData EarliestED()
        {
            foreach (EnergyData ed in history)
            {
                if (ed.valid_since.E2Time == GlobalEarliest)
                    return ed;
            }
            return null;
        }
        static EnergyData LatestED()
        {
            foreach(EnergyData ed in history)
            {
                if (ed.valid_until.E2Time == GlobalLatest)
                    return ed;
            }
            return null;
        }
        static void MakeGlobalMap()
        {
            // 统计时间范围
            foreach (EnergyData ed in history)
            {
#if MSGBOXS
                string s = ed.TariffName() + ": " + ed.valid_since.ToString() + " - " +
                    ed.valid_until.ToString();
                MB.OKI(s);
#endif
                if (!DT.IsStrictlyValid(ed.valid_since.E2Time) ||
                    !DT.IsStrictlyValid(ed.valid_until.E2Time))
                    continue;

                if (GlobalEarliest > ed.valid_since.E2Time)
                    GlobalEarliest = ed.valid_since.E2Time;
                if (GlobalLatest < ed.valid_until.E2Time)
                    GlobalLatest = ed.valid_until.E2Time;
            }
            GlobalBegin = DT.DateFloor(GlobalEarliest);
            GlobalFirst = DT.DateFloor(GlobalLatest);
            GlobalEnd = DT.DateCeiling(GlobalLatest);

            if (!DT.IsStrictlyValid(GlobalEnd) 
                /*GlobalEnd.Year < 1980 || GlobalEnd.Year > 2999 ||*/
                /*GlobalBegin.Year < 1980 || GlobalBegin.Year > 2999*/)
            {
                //MB.Error("Invalid time frames found, aborting");
                return;
            }
            if(!DT.IsStrictlyValid(GlobalBegin))
            {
                return;
            }

            // 计算空间
            int total_hours = (int)Math.Ceiling((GlobalEnd - GlobalBegin).TotalHours);
            int total_months = DT.MonthDistance(GlobalEnd, GlobalBegin)+1;
            GlobalTimeMap = new List<DateTime>(new DateTime[total_hours]);
            GlobalMaxInstantMonthly = new List<TimeValuePair>();
            for (int i = 0; i < total_months; i++ )
            {
                GlobalMaxInstantMonthly.Add(new TimeValuePair(DateTime.MinValue, float.MinValue));
            }
            for (int i = 0; i < total_hours; i++ )
            {
                GlobalTimeMap[i] = IndexToHour(i, GlobalFirst);
            }
            // 分配空间
            GlobalMap = new List<float>(total_hours);
            for (int i = 0; i < total_hours; i++ )
            {
                GlobalMap.Add(Is.UntouchedF);
            }
            GlobalCost = new List<float>(GlobalMap);

            // 插入数据
            foreach (EnergyData ed in history)
            {
                ed.InsertToGM();
            }

            CalcDaily();
            
            CalcOthers();
        }
        // 关键步骤
        // 单元某项位于全局表中的位置
        int GlobalMapIndex(int idx)
        {
            DateTime thistime = Index2Hour(idx);
            // 全局表0: GlobalFirst
            // ...
            // 全局表24: GlobalFirst - oneday
            // ...
//             TimeSpan ts = (GlobalFirst - thistime);
//             int days = 0;
//             if( ts.TotalDays > 0 )
//                 days = (int)Math.Ceiling(ts.TotalDays);
//             int hours = (thistime.Hour - GlobalFirst.Hour); // globalfirst.hour 总是0
//             int i = days * 24 + hours;

            for (int i = 0; i < GlobalTimeMap.Count; i++ )
            {
                if (thistime == GlobalTimeMap[i])
                    return i;
            }
            return -1;
        }
        void InsertToGM()
        {
            for (int i = 0; i < valid_kwhs.Count; i++ )
            {
                if( Is.ValidF(valid_kwhs[i]) )
                {
                    int x = GlobalMapIndex(i);
                    // 已经结束了
                    if (x<0 || x >= GlobalMap.Count)
                    {
                        System.Diagnostics.Debug.Print("InsertToGM(): Weird this={2}, x={0}, globalcount={1}",
                            x, GlobalMap.Count, this.ToString());
                        break;
                    }
                    if (!Is.ValidF(GlobalMap[x]))
                    {
                        GlobalMap[x] = 0;
                        GlobalCost[x] = 0;
                    }

                    GlobalMap[x] += valid_kwhs[i];
                    GlobalCost[x] += valid_costs[i];
                }
            }
            if (kw_max_monthly.Count == 0 ||
                when_kw_max_monthly.Count == 0)
                return;

            DateTime ptthis = valid_until.E2Time;
            DateTime ptglobal = GlobalEnd;
            if (ptthis > ptglobal)
                return;

            //ptglobal = DT.PrevMonth(ptglobal);
            int idxglobal = 0;
            while (!DT.SameMonth(ptglobal, ptthis))
            {
                ptglobal = DT.PrevMonth(ptglobal);
                idxglobal++;
            }
            for (int i = 0; i < when_kw_max_monthly.Count; i++)
            {
                if (!DT.IsStrictlyValid(when_kw_max_monthly[i]))
                    continue;
                if (kw_max_monthly[i] > GlobalMaxInstantMonthly[idxglobal].value )
                {
                    GlobalMaxInstantMonthly[idxglobal].value = kw_max_monthly[i];
                    GlobalMaxInstantMonthly[idxglobal].when = when_kw_max_monthly[i];
                }
                idxglobal++;
                ptthis = DT.PrevMonth(ptthis);
            }
        }

        static List<float> GlobalDaily = new List<float>();
        static List<float> GlobalWeekly = new List<float>();
        static List<float> GlobalMonthly = new List<float>();
        static List<float> GlobalYearly = new List<float>();

        static List<float> GlobalCostDaily = new List<float>();
        static List<float> GlobalCostWeekly = new List<float>();
        static List<float> GlobalCostMonthly = new List<float>();
        static List<float> GlobalCostYearly = new List<float>();

        public static float GlobalMax = 0;
        public static float GlobalMaxDaily = 0;
        public static float GlobalMaxWeekly = 0;
        public static float GlobalMaxMonthly = 0;
        public static float GlobalMaxYearly = 0;

        public static float GlobalMaxCost = 0;
        public static float GlobalMaxCostDaily = 0;
        public static float GlobalMaxCostWeekly = 0;
        public static float GlobalMaxCostMonthly = 0;
        public static float GlobalMaxCostYearly = 0;
        static void StoreIfValid(ref List<float> lst, float y)
        {
            int last = lst.Count - 1;
            if( Is.ValidF(y) )
            {
                if (!Is.ValidF(lst[last]))
                    lst[last] = 0;
                lst[last] += y;
            }
        }
        static DateTime GlobalMapTime(int idx)
        {
            return GlobalTimeMap[idx];
//             if (idx < 0 || idx >= GlobalMap.Count)
//                 return DateTime.MinValue;
//             DateTime pointer = GlobalFirst;
//             int days = idx / 24;
//             int hours = idx % 24;
//             pointer -= new TimeSpan(days, 0, 0, 0);
//             pointer += new TimeSpan(hours, 0, 0);
//             return pointer;
        }
        static void CalcDaily()
        {
            int day = -1;
            DateTime pointer;// = GlobalFirst;
            TimeSpan onehour = new TimeSpan(1, 0, 0);
            TimeSpan oneday = new TimeSpan(1, 0, 0, 0);
            int idx = 0;
            for (int i = 0; i < GlobalMap.Count; i++ )
            {
                pointer = GlobalMapTime(i);
//                 if (pointer != GlobalMapTime(i) )
//                 {
//                     System.Diagnostics.Debugger.Break();
//                 }
                if( Is.ValidF(GlobalMap[i]))
                {
                    if (GlobalMax < GlobalMap[i])
                        GlobalMax = GlobalMap[i];
                    if (GlobalMaxCost < GlobalCost[i])
                        GlobalMaxCost = GlobalCost[i];
                }
                // 每日数据
                if( day != pointer.Day )
                {
                    day = pointer.Day;
                    GlobalDaily.Add(Is.UntouchedF/*GlobalMap[i]*/);
                    GlobalCostDaily.Add(Is.UntouchedF/*GlobalCost[i]*/);
                    idx = GlobalDaily.Count - 1;
                }
//                 else
//                 {
                    if (Is.ValidF(GlobalMap[i]))
                    {
                        if (!Is.ValidF(GlobalDaily[idx]))
                            GlobalDaily[idx] = 0;
                        if (!Is.ValidF(GlobalCostDaily[idx]))
                            GlobalCostDaily[idx] = 0;

                        GlobalDaily[idx] += GlobalMap[i];
                        GlobalCostDaily[idx] += GlobalCost[i];
                        //StoreIfValid(ref GlobalDaily, GlobalMap[i]);
                        //StoreIfValid(ref GlobalCostDaily, GlobalCost[i]);
                    }
//                 }

//                 if (IsEndOfDay(i))
//                 {
//                     pointer = DT.DateFloor(pointer);
//                     pointer -= oneday;
//                 }
//                 else
//                     pointer += onehour;
            } // for

            GlobalMaxDaily = FloatList.Max(GlobalDaily);
            GlobalMaxCostDaily = FloatList.Max(GlobalCostDaily);

        } // function

        static void CalcOthers()
        {
            DateTime pointer = GlobalFirst;
            TimeSpan oneday = new TimeSpan(1, 0, 0, 0);
            int year = -1;
            int month = -1;
            int week = -1;

            int weekidx = -1;
            int yearidx = -1;
            int monthidx = -1;

            for (int i = 0; i < GlobalDaily.Count; i++ )
            {
                // 周
                if( week != DT.WhichWeekOfYear(pointer))
                {
                    weekidx++;
                    week = DT.WhichWeekOfYear(pointer);
                    GlobalWeekly.Add(0);
                    GlobalCostWeekly.Add(0);
                }

                // 月
                if (month != pointer.Month)
                {
                    monthidx++;
                    month = pointer.Month;
                    //GlobalMonthly.Add(GlobalDaily[i]);
                    //GlobalCostMonthly.Add(GlobalCostDaily[i]);
                    GlobalMonthly.Add(0);
                    GlobalCostMonthly.Add(0);
                }
                // 年
                if (year != pointer.Year)
                {
                    year = pointer.Year;
                    yearidx++;
                    //GlobalYearly.Add(GlobalDaily[i]);
                    //GlobalCostYearly.Add(GlobalCostDaily[i]);
                    GlobalYearly.Add(0);
                    GlobalCostYearly.Add(0);
                }
                if (Is.ValidF(GlobalDaily[i]))
                {
                    GlobalWeekly[weekidx] += GlobalDaily[i];
                    GlobalMonthly[monthidx] += GlobalDaily[i];
                    GlobalYearly[yearidx] += GlobalDaily[i];
                }
                if (Is.ValidF(GlobalCostDaily[i]))
                {
                    GlobalCostWeekly[weekidx] += GlobalCostDaily[i];
                    GlobalCostMonthly[monthidx] += GlobalCostDaily[i];
                    GlobalCostYearly[yearidx] += GlobalCostDaily[i];
                }

                pointer -= oneday;
            } // for

            GlobalMaxWeekly = FloatList.Max(GlobalWeekly);
            GlobalMaxMonthly = FloatList.Max(GlobalMonthly);
            GlobalMaxYearly = FloatList.Max(GlobalYearly);

            GlobalMaxCostWeekly = FloatList.Max(GlobalCostWeekly);
            GlobalMaxCostMonthly = FloatList.Max(GlobalCostMonthly);
            GlobalMaxCostYearly = FloatList.Max(GlobalCostYearly);
        }
        static int GlobalHourlyIndex(DateTime hour)
        {
            for (int i = 0; i < GlobalTimeMap.Count; i++ )
            {
                if (hour == GlobalTimeMap[i])
                    return i;
            }
            return -1;
//             if (hour < GlobalBegin || hour >= GlobalEnd)
//                 return -1;
// 
//             int idx = 0;
//             TimeSpan ts = GlobalFirst - hour;
//             if (ts.TotalDays > 0)
//                 idx = (int)Math.Ceiling(ts.TotalDays);
//             idx *= 24;
//             idx += (hour.Hour - GlobalFirst.Hour);
//             
//             if (idx >= GlobalMap.Count)
//                 return -1;
//             return idx;
        }
        public static float QueryGlobalEnergyHourly(DateTime hour)
        {
            if (GlobalMap == null)
                return -1;
            if (GlobalMap.Count == 0)
                return -1;
            int idx = GlobalHourlyIndex(hour);
            if (idx == -1)
                return -1;
            return GlobalMap[idx];
        }
        public static float QueryGlobalCostHourly(DateTime hour)
        {
            if (GlobalCost == null)
                return -1;
            if (GlobalCost.Count == 0)
                return -1;
            int idx = GlobalHourlyIndex(hour);
            if (idx == -1)
                return -1;
            return GlobalCost[idx];
        }
        static int DailyIndex(DateTime day)
        {
            if (GlobalDaily == null)
                return -1;
            if (GlobalDaily.Count == 0)
                return -1;
            double d = (DT.DateCeiling(GlobalEnd) - day).TotalDays;
            if (d <= -1)
                return -1;
            //d = Math.Abs(d);
            d = Math.Floor(d);
            int idx = (int)d;
            if (idx >= GlobalDaily.Count)
                return -1;

            return idx;
        }
        public static float QueryGlobalEnergyDaily(DateTime day)
        {
            int idx = DailyIndex(day);
            if (idx == -1)
                return -1;
            return GlobalDaily[idx];
        }
        public static float QueryGlobalCostDaily(DateTime day)
        {
            int idx = DailyIndex(day);
            if (idx == -1)
                return -1;
            return GlobalCostDaily[idx];
        }
        // 周
        static int WeekIndex(DateTime day)
        {
            if (GlobalWeekly == null)
                return -1;
            if (GlobalWeekly.Count == 0)
                return -1;
            if (day < GlobalBegin || day >= GlobalEnd)
                return -1;
            int idx = DT.WeekDistance(day, GlobalFirst);
            if (idx >= GlobalWeekly.Count)
                return -1;
            return idx;
        }
        public static float QueryGlobalEnergyWeekly(DateTime week)
        {
            int idx = WeekIndex(week);
            if (idx == -1)
                return -1;
            return GlobalWeekly[idx];
        }
        public static float QueryGlobalCostWeekly(DateTime week)
        {
            int idx = WeekIndex(week);
            if (idx == -1)
                return -1;
            return GlobalCostWeekly[idx];
        }
        // 月
        static int MonthIndex(DateTime month)
        {
            if (GlobalMonthly == null)
                return -1;
            if (GlobalMonthly.Count == 0)
                return -1;
            //if (month < GlobalBegin || month > GlobalEnd)
            //    return -1;
            int year = GlobalFirst.Year - month.Year;
            int m = GlobalFirst.Month - month.Month;
            int idx = year * 12 + m;
            if (year < 0)
                return -1;
            if (idx < 0 || idx >= GlobalMonthly.Count)
                return -1;

            return idx;
        }
        public static float QueryGlobalEnergyMonthly(DateTime month)
        {
            int idx = MonthIndex(month);
            if (idx == -1)
                return -1;
            return GlobalMonthly[idx];
        }
        public static float QueryGlobalCostMonthly(DateTime month)
        {
            int idx = MonthIndex(month);
            if (idx == -1)
                return -1;
            return GlobalCostMonthly[idx];
        }

        static int YearIndex(DateTime year)
        {
            if (GlobalYearly == null)
                return -1;
            if (GlobalYearly.Count == 0)
                return -1;
            //if (year < GlobalBegin || year > GlobalEnd)
            //    return -1;
            int y = GlobalFirst.Year - year.Year;
            if (y < 0 || y >= GlobalYearly.Count)
                return -1;

            return y;
        }
        public static float QueryGlobalEnergyYearly(DateTime year)
        {
            int idx = YearIndex(year);
            if (idx == -1)
                return -1;
            return GlobalYearly[idx];
        }
        public static float QueryGlobalCostYearly(DateTime year)
        {
            int idx = YearIndex(year);
            if (idx == -1)
                return -1;
            return GlobalCostYearly[idx];
        }

        public static float QueryGlobalEnergyTotal()
        {
            if (GlobalYearly == null)
                return -1;
            if (GlobalYearly.Count == 0)
                return -1;
            return FloatList.Sum(GlobalYearly);
        }
        public static float QueryGlobalCostTotal()
        {
            if (GlobalCostYearly == null)
                return -1;
            if (GlobalCostYearly.Count == 0)
                return -1;
            return FloatList.Sum(GlobalCostYearly);
        }
        public static float QueryAverageTotal()
        {
            if (GlobalCost == null)
                return -1;
            if (GlobalCost.Count == 0)
                return -1;
            float et = QueryGlobalEnergyTotal();
            float ct = QueryGlobalCostTotal();
            if (et == 0)
                return -1;
            return ct / et;
        }
        static float QueryGlobalAverage(List<float> lst)
        {
            return FloatList.Average(lst);
        }
        public static float QueryGlobalHourlyAverageEnergy()         { return QueryGlobalAverage(GlobalMap); }
        public static float QueryGlobalHourlyAverageCost()     { return QueryGlobalAverage(GlobalCost); }
        public static float QueryGlobalHourlyAverageCarbon()   { return Settings.I.carbon * QueryGlobalAverage(GlobalMap); }

        public static float QueryGlobalDailyAverage()          { return QueryGlobalAverage(GlobalDaily); }
        public static float QueryGlobalDailyAverageCost()      { return QueryGlobalAverage(GlobalCostDaily);}
        public static float QueryGlobalDailyAverageCarbon()    { return Settings.I.carbon*QueryGlobalAverage(GlobalDaily);}

        public static float QueryGlobalWeeklyAverage() { return QueryGlobalDailyAverage() * 7;/*return QueryGlobalAverage(GlobalWeekly);*/}
        public static float QueryGlobalWeeklyAverageCost() { return QueryGlobalDailyAverageCost() * 7;/*return QueryGlobalAverage(GlobalCostWeekly);*/}
        public static float QueryGlobalWeeklyAverageCarbon() { return QueryGlobalDailyAverageCarbon() * 7;/*return Settings.I.carbon*QueryGlobalAverage(GlobalWeekly);*/ }

        public static float QueryGlobalMonthlyAverage() { return QueryGlobalDailyAverage() * 30;/*return QueryGlobalAverage(GlobalMonthly);*/ }
        public static float QueryGlobalMonthlyAverageCost() { return QueryGlobalDailyAverageCost() * 30;/*return QueryGlobalAverage(GlobalCostMonthly);*/ }
        public static float QueryGlobalMonthlyAverageCarbon() { return QueryGlobalDailyAverageCarbon() * 30;/*return Settings.I.carbon * QueryGlobalAverage(GlobalMonthly);*/ }

        public static List<List<float>> QueryGlobalWeekdayAverage(int year/*周数据不区别年份*/)
        {
            if (GlobalMap == null)
                return null;
            if (GlobalMap.Count == 0)
                return null;
            List<List<float>> lst = new List<List<float>>();
            List<List<int>> count = new List<List<int>>();
            for (int i = 0; i < 7; i++ )
            {
                lst.Add(new List<float>());
                count.Add(new List<int>());
                for (int j = 0; j < 24; j++ )
                {
                    lst[i].Add(Is.UntouchedF);
                    count[i].Add(0);
                }
            }

            DateTime pointer;// = GlobalFirst;
            for (int i = 0; i < GlobalMap.Count; i++ )
            {
                pointer = GlobalMapTime(i);
                // 周数据不区别年份
                //if (year != pointer.Year)
                //    continue;
                if (Is.ValidF(GlobalMap[i]))
                {
                    int idx1, idx2;
                    idx1 = (int)pointer.DayOfWeek;
                    idx2 = pointer.Hour;
                    if (Is.ValidF(lst[idx1][idx2]))
                        lst[idx1][idx2] += GlobalMap[i];
                    else
                        lst[idx1][idx2] = GlobalMap[i];
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
            for (int i = 0; i < 7; i++ )
            {
                for (int j = 0; j < 24; j++ )
                {
                    if (count[i][j] != 0)
                        lst[i][j] /= count[i][j];
                }
            }
            return lst;
        }
        // 画月度曲线用
        public static List<List<float>> QueryGlobalMonthsAverage(
            int year, 
            out List<TimeValuePair> max)
        {
            max = null;
            if (GlobalMap.Count == 0)
                return null;
            max = new List<TimeValuePair>();
            List<List<float>> lst = new List<List<float>>();
            List<List<int>> cnt = new List<List<int>>();
            for (int i = 0; i < 12; i++ )
            {
                lst.Add(new List<float>());
                cnt.Add(new List<int>());
                max.Add(new TimeValuePair(DateTime.MinValue, float.MinValue));
                for (int j = 0; j < 24; j++ )
                {
                    lst[i].Add(0);
                    cnt[i].Add(0);
                }
            }

            DateTime pointer;// = GlobalFirst;
            for (int i = 0; i < GlobalMap.Count; i++)
            {
                pointer = GlobalMapTime(i);
                if (pointer.Year != year)
                    continue;
                if (Is.ValidF(GlobalMap[i]))
                {
                    int idx1, idx2;
                    idx1 = (int)pointer.Month-1;
                    idx2 = pointer.Hour;
                    lst[idx1][idx2] += GlobalMap[i];
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

            // 最大功率值
            for (int i = 0; i < GlobalMaxInstantMonthly.Count; i++ )
            {
                TimeValuePair pair = GlobalMaxInstantMonthly[i];
                DateTime when = pair.when;
                float value = pair.value;
                if( DT.IsStrictlyValid(when) )
                {
                    if( when.Year == year )
                    {
                        if(value > max[when.Month-1].value )
                        {
                            max[when.Month-1] = pair;
                        }
                    }
                }
            }

            return lst;
        }

        public static DateTime QueryGlobalLastDayValid()
        {
            return GlobalFirst;
        }

        public static DateTime QueryGlobalLastCollectTime()
        {
//             DateTime last = DateTime.MinValue;
//             foreach (EnergyData ed in history)
//             {
//                 if( ed.valid_until != DateTime.MinValue )
//                 {
//                     if (last < ed.valid_until)
//                         last = ed.valid_until;
//                 }
//             }
//             return last;
            return GlobalLatest;
        }

        public bool IsMonthly()
        {
            return valid_months != null && valid_years==null;
        }
        public bool IsYearly()
        {
            return valid_months == null && valid_years != null;
        }
        public bool IsPartial()
        {
            if (valid_months != null && valid_years != null)
                return false;
            if (valid_months == null && valid_years == null)
                return false;
            return true;
        }

        public float total_kwh = 0;

        // 不带计费信息的年度EnergyData，用于比较其他Tariff
        public List<EnergyData> YearlyCopies()
        {
            List<EnergyData> yearly = new List<EnergyData>();
            for (int i = 0; i < valid_years.Count; i++ )
            {
                int year = valid_years[i];
                EnergyData e = (EnergyData)this.Clone();
                DateTime from = new DateTime(year, 1, 1);
                DateTime to = DT.DateCeiling(new DateTime(year, 12, 31));
                bool need_restat = false;
                // 如果起始时间早于这一年，设为这一年
                if (e.valid_since.E2Time < from)
                {
                    e.valid_since.E2Time = from; 
                    need_restat = true;
                }
                // 如果结束时间晚于这一年，设为这一年
                if (e.valid_until.E2Time > to)
                {
                    e.valid_until.E2Time = to;
                    need_restat = true;
                }
                if( need_restat )
                    e.Statistics();
                e.total_kwh = FloatList.Sum(e.valid_kwhs);
                if (Is.ValidF(e.total_kwh) && e.total_kwh != 0)
                {
                    e.valid_months = null;
                    yearly.Add(e);
                }
            }

            return yearly;
        }
        // 不带计费信息的月度EnergyData，用于比较其他Tariff
        public List<EnergyData> MonthlyCopies()
        {
            List<EnergyData> monthly = new List<EnergyData>();
            for (int i = 0; i < valid_months.Count; i++)
            {
                DateTime m = valid_months[i];
                EnergyData e = (EnergyData)this.Clone();
                DateTime from = DT.MonthFloor(m);
                DateTime to = DT.MonthCeiling(m);
                bool need_restat = false;
                // 如果起始时间早于这一月，设为这一月
                if (e.valid_since.E2Time < from)
                {
                    e.valid_since.E2Time = from;
                    need_restat = true;
                }
                if (e.valid_until.E2Time > to)
                {
                    e.valid_until.E2Time = to;
                    need_restat = true;
                }
                
                if( need_restat )
                    e.Statistics();
                e.total_kwh = FloatList.Sum(e.valid_kwhs);
                if (Is.ValidF(e.total_kwh) && e.total_kwh != 0)
                {
                    e.valid_years = null;
                    monthly.Add(e);
                }
            }

            return monthly;
        }
        public List<float> MonthlyCost(int year, out float total, out float max)
        {
            total = 0;
            max = float.MinValue;
            List<float> monthly = new List<float>();
            for (int i = 0; i < 12; i++ )
            {
                monthly.Add(float.NaN);
            }

            for (int i = 0; i < valid_costs.Count; i++ )
            {
                DateTime pointer = Index2Hour(i);
                if (pointer.Year != year)
                    continue;
                if (!Is.ValidF(valid_costs[i]) ||
                    valid_costs[i] < 0)
                    continue;
                total += valid_costs[i];
                //if (max < valid_costs[i])
                //    max = valid_costs[i];

                int m = pointer.Month-1;
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
            for (int i = 0; i < valid_costs.Count; i++ )
            {
                if( Is.ValidF(valid_costs[i]) &&
                    valid_costs[i]>=0 )
                    sum += valid_costs[i];
            }
            return sum;
        }
        public void SetTariff(Tariff tt)
        {
            t = tt;
            Statistics();
        }
        public string TariffName()
        {
            if (t != null)
                return t.tariff_name;
            return null;
        }
        public string Currency()
        {
            if (t != null)
                return t.price_currency;
            return null;
        }
        public static Tariff[] Tariffs()
        {
            DirectoryInfo di = new DirectoryInfo(REPOSITORY_PATH);
            if( !di.Exists )
                return null;
            FileInfo[] tarifffiles = di.GetFiles("*.xml");
            if (tarifffiles == null)
                return null;
            if (tarifffiles.Length == 0)
                return null;
            Tariff[] AllTariffs = new Tariff[tarifffiles.Length];
            for(int i=0; i<tarifffiles.Length; i++)
            {
                AllTariffs[i] = Tariff.LoadTariffXML(tarifffiles[i].FullName);
            }
            return AllTariffs;
        }
    }

    public class TimeValuePair
    {
        public DateTime when;
        public float value;
        public TimeValuePair(DateTime w, float v) { when = w; value = v; }
        public override string ToString()
        {
            return string.Format("{0} {1}", value, when.ToString("o"));
        }
    }
    public static class BitOp
    {
        public static byte Byte0Int(int x) { return (byte)(x & 0xFF); }
        public static byte Byte1Int(int x) { return (byte)((x>>8) & 0xFF); }
        public static byte Byte2Int(int x) { return (byte)((x>>16) & 0xFF); }
        public static byte Byte3Int(int x) { return (byte)((x>>24) & 0xFF); }
    }
}
