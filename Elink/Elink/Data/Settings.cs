using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.IO;

namespace Elink
{
    public class Settings
    {
        public static Settings I 
        { 
            get 
            {
//                 if (theOne == null)
//                     Load();
                 if (theOne == null)
                     theOne = new Settings();
                return theOne; 
            } 
        }
        public static bool IsValid()
        {
            return theOne != null;
        }
        private static event EventHandler SettingsChanging;
        public static void AddListener(EventHandler d) {SettingsChanging -=d; SettingsChanging += d;}
        public static void BroadcastChange() { if (SettingsChanging != null) SettingsChanging.Invoke(null, null); }
        private static Settings theOne = null;
        private Settings(){/*You cannot create me BABE *^_^*/}
        public string lang;
        public string user_name = "INDIQUE AQUÍ SU NOMBRE DE USUARIO";
        public string country = "España";
        public string currency = "£";
        public float average_consumption = 1.8f;
        public float carbon = 0.5f; // kg.CO2/kWh
        public float target = 0;   // %
        public int target_type = 0;
        public int target_period = 0;
        public bool single_tariff = false; // 这个如果是true，tariffs应该是1
        public DateTime backup_time = DateTime.MinValue;
        public DateTime collect_time = DateTime.MinValue;
        public float firmware_version = 1.0f;
        public float version = 1.1f;

        public Tariff advTariff = new Tariff();
        const string config_file = "Config.xml";
        public void Save()
        {
            advTariff.last_update = DateTime.Now;
            XMLUtil<Settings>.SaveXml(config_file, this);
        }
        public static void Load()
        {
            try
            {
                theOne = XMLUtil<Settings>.LoadXml(config_file);
                //             if( theOne == null )
                //                 MB.Warning("Settings load failed!");
                if (theOne == null)
                {
                    theOne = new Settings();
                    theOne.Save();
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Print(e.Message);
                return;
            }
        }
//         public float SimplePrice
//         {
//             get { return advTariff.energy_rates_p1; }
//             set { advTariff.energy_rates_p1 = value; }
//         }
    }
    public class Tariff: ICloneable
    {
        #region General Info
        public string tariff_name;
        public string full_path = "";
        public float version = 1.1f;
        public DateTime last_update = DateTime.MinValue;
        public TwoWorld valid_since = new TwoWorld();
        public TwoWorld valid_until = new TwoWorld();
        //public DateTime valid_since = DateTime.MinValue;
        //public DateTime valid_until = DateTime.MaxValue;

        public float max_power = 5.5f;        // kW
        public float power_fee = 1.642355f;     // $/kWh/Month
        // POWER COST = Max. Power x Power fee = Cost/month
        // --------------------------------
        public float energy_rates_p1 = 0.106888f; // $/kWh
        public int number_periods = 0;     // index of Pn
        // --------------------------------
        public float meter_rent = 0.57f;       // $/Month
        public float elec_tax = 5.11f;         // %
        public float VAT = 16;             // %
        // --------------------------------
        public float over_limit = 500;     // kWh/Month
        public const float OVERCHARGEDEFAULT = 0.027403f;
        public float over_charge = 0.027403f;  // %/kWh        
        #endregion

        #region More General
        public int tariffs = 1;         // 1 2 or 3
//         public List<string> price_alias = new List<string>();// {"On Peak","Off Peak","Others"};
//         public List<float> price_values = new List<float>();// {1.5f, 1.3f, 1.0f};
        public string[] price_alias = new string[]{ "On Peak", "Off Peak", "Others" };
        public float[] price_values = new float[] { 0.138076f, 0.054208f, 1.0f };
        public string price_currency = "¥";
        #endregion

        #region Simple Tariffs
        public bool simple = true;
        public int simple_periods = 4;   // 2 or 4
        public int p1_price = 0;
        public int p2_price = 0;
        public int p3_price = 1;
        public string p1_time1 = "12:00";
        public string p1_time2 = "22:00";
        public string p1_time3 = "13:00";
        public string p1_time4 = "23:00";
        public string p3_time1 = "22:00";
        public string p3_time2 = "12:00";
        public string p3_time3 = "23:00";
        public string p3_time4 = "13:00";
        #endregion

        #region Advanced Tariffs
        public List<TariffData> adv = new List<TariffData>();
        #endregion 

//         public void InitPrices()
//         {
//             if (price_alias.Count != 0 || price_values.Count != 0)
//                 return;
//             price_alias = new List<string>();
//             price_alias.AddRange(new string[] {"On Peak","Off Peak","Others"});
//             price_values = new List<float>();
//             price_values.AddRange(new float[] { 1.5f, 1.3f, 1.0f });
//         }
        public Tariff()
        {
            if (Settings.IsValid())
                price_currency = Settings.I.currency;
            //InitPrices();
        }
        public Object Clone()
        {
            object o = this.MemberwiseClone();
            Tariff t = (Tariff)o;
            t.valid_since = (TwoWorld)this.valid_since.Clone();
            t.valid_until = (TwoWorld)this.valid_until.Clone();
            return t;
        }
        public override string ToString()
        {
            return tariff_name;
        }
        public void Save()
        {
            if (!Global.IsSpanish())
                this.over_charge = 0;

            SaveToXML(full_path);
        }
        public bool SameName(string s)
        {
            if (this.tariff_name == null)
                return false;

            return this.tariff_name.Equals(s,
                StringComparison.OrdinalIgnoreCase);
        }
        public bool SameName(Tariff t)
        {
            return SameName(t.tariff_name);
        }
        public override bool Equals(Object obj)
        {
            if (this.GetType() != obj.GetType()) return false;
            Tariff t = (Tariff)obj;
            return (this.SameName(t)) &&
                t.adv.Count == this.adv.Count &&
                t.last_update == this.last_update &&
                t.p1_time1 == this.p1_time1 &&
                t.p1_time2 == this.p1_time2 &&
                t.p3_time1 == this.p3_time1;
        }
        public override int GetHashCode()
        {
            return 0;
        }

        public bool SaveToXML(string xml)
        {
            if (xml == null)
                return false;
            if (xml.Length == 0)
                return false;
            if (xml[0] == '.')
                return false;

            price_currency = Settings.I.currency;
            last_update = DateTime.Now;
            //last_update.ToUniversalTime();
            if( !valid_until.IsValid )
            {
                valid_until = new TwoWorld();
            }
            full_path = xml;
            //tariff_name = Path.GetFileNameWithoutExtension(xml);
            return XMLUtil<Tariff>.SaveXml(xml, this);
        }
        public static Tariff LoadTariffXML(string xml)
        {
            Tariff adv = XMLUtil<Tariff>.LoadXml(xml);
            if( adv == null )
                return null;
            adv.last_update = adv.last_update.ToLocalTime();

            // 英国模式无overcharge
            if (!Global.IsSpanish())
                adv.over_charge = 0;

//             if( adv.price_alias.Count !=3 ||
//                 adv.price_values.Count != 3)
//             {
//                 System.Diagnostics.Debug.Print("Weird! price is not 3");
//                 //adv.price_alias.RemoveRange(0, 3);
//                 //adv.price_values.RemoveRange(0, 3);
//             }
            //adv.tariff_name = Path.GetFileNameWithoutExtension(xml);
            return adv;
        }
        public string FullPath()
        {
            //string path1 = Path.Combine(Directory.GetCurrentDirectory(), tariff_name);
            //path1 = Path.ChangeExtension(path1, "xml");
            string path2 = Path.Combine(EnergyData.REPOSITORY_PATH, tariff_name);
            path2 = Path.ChangeExtension(path2, "xml");
            //FileInfo fi1 = new FileInfo(path1);
            FileInfo fi2 = new FileInfo(path2);
            //if (fi1.Exists)
            //    return fi1.FullName;
            //if (fi2.Exists)
            return fi2.FullName;
        }
        public void Rename(string newname)
        {
            Tariff t = (Tariff)this.Clone();
            t.tariff_name = newname;
            string fullpath = t.FullPath();
            //if (fullpath == null)
            //    return;
            t.SaveToXML(fullpath);
        }

        public bool Delete()
        {
            if (SameName(Settings.I.advTariff))
                return false;

            string csv = Path.ChangeExtension(tariff_name, "csv");
            FileInfo ficsv = new FileInfo(csv);
            FileInfo ficsvpath = new FileInfo(Path.Combine(EnergyData.REPOSITORY_PATH, csv));
            if (ficsv.Exists || ficsvpath.Exists)
                return false;

            string file = Path.ChangeExtension(tariff_name, "xml");
            FileInfo fi = new FileInfo(file);
            if( fi.Exists )
            {
                fi.Delete();
            }
            else
            {
                file = Path.Combine(EnergyData.REPOSITORY_PATH, file);
                fi = new FileInfo(file);
                if (fi.Exists)
                    fi.Delete();
            }
            return true;
        }
        private float MaxPowerFeePerHour()
        {
            return max_power * power_fee / 30 / 24;
        }
        private float MeterRentPerHour()
        {
            return meter_rent / 30 / 24;
        }
        private float SingleTariffFeePerHour(float energy, float overcharge)
        {
            return energy * (energy_rates_p1+overcharge);
        }
        private float MultiplyTheRestPerHour(float cost)
        {
            cost += MaxPowerFeePerHour();
//             cost += over_charge_per_hour;
            cost *= (100 + elec_tax)/100;
            cost += MeterRentPerHour();
            cost *= (100 + VAT)/100;
            return cost;
        }
        // SUMMER: Last Sunday of March - Last Sunday of October
        // WINTER: OTHERWISE
        private bool IsSummer(DateTime day)
        {
            DateTime last_sunday_of_march = new DateTime(day.Year, 3, 31);
            int dayofweek = (int)last_sunday_of_march.DayOfWeek;
            last_sunday_of_march -= new TimeSpan(dayofweek, 0, 0, 0);

            DateTime last_sunday_of_oct = new DateTime(day.Year, 10, 31);
            int dayofweek2 = (int)last_sunday_of_oct.DayOfWeek;
            last_sunday_of_oct -= new TimeSpan(dayofweek2, 0, 0, 0);

            if (day >= last_sunday_of_march && day <= last_sunday_of_oct)
                return true;

            return false;
        }
        private float SimplePeriodsHourlyFee(DateTime hour, float energy, float overcharge)
        {
            // 2 Simple periods
            if( simple_periods == 2 )
            {
                DateTime t1 = DateTime.Parse(p1_time1);
                DateTime t2 = DateTime.Parse(p1_time2);
                t1 = new DateTime(hour.Year, hour.Month, hour.Day, t1.Hour, t1.Minute, t1.Second);
                t2 = new DateTime(hour.Year, hour.Month, hour.Day, t2.Hour, t2.Minute, t2.Second);
                // 21:00 ~ 09:00
                if (t1 > t2)
                    t2 += new TimeSpan(1, 0, 0, 0);

                float ratio1 = price_values[p1_price];
                float ratio2 = price_values[p2_price];
                // within P1
                if( hour >= t1 && hour < t2 )
                {
                    return energy * ratio1;
                }
                else
                {
                    return energy * ratio2;
                }
            }
            else // 4
            {
                // SUMMER: Last Sunday of March - Last Sunday of October
                // WINTER: OTHERWISE
                
                bool is_summer = IsSummer(hour);
                DateTime t1 = DateTime.Parse(p1_time1);
                DateTime t2 = DateTime.Parse(p1_time2);
                DateTime t3 = DateTime.Parse(p1_time3);
                DateTime t4 = DateTime.Parse(p1_time4);
                t1 = new DateTime(hour.Year, hour.Month, hour.Day, t1.Hour, t1.Minute, t1.Second);
                t2 = new DateTime(hour.Year, hour.Month, hour.Day, t2.Hour, t2.Minute, t2.Second);
                t3 = new DateTime(hour.Year, hour.Month, hour.Day, t3.Hour, t3.Minute, t3.Second);
                t4 = new DateTime(hour.Year, hour.Month, hour.Day, t4.Hour, t4.Minute, t4.Second);
                // 21:00 ~ 09:00
                if (t1 > t2)
                    t2 += new TimeSpan(1, 0, 0, 0);
                if (t3 > t4)
                    t4 += new TimeSpan(1, 0, 0, 0);

                float ratio1 = price_values[p1_price];
                float ratio2 = price_values[p3_price];

                float final_price = ratio1;
                if (!is_summer)
                {
                    if (hour >= t1 && hour < t2)
                    {
                        final_price = ratio1;
                    }
                    else
                    {
                        final_price = ratio2;
                    }
                }
                else
                {
                    if (hour >= t3 && hour < t4)
                    {
                        final_price = ratio1;
                    }
                    else
                    {
                        final_price = ratio2;
                    }
                }
                return (final_price+overcharge) * energy;
            }
        }
        private float AdvancedPeriodsHourlyFee(DateTime hour, float energy, float overchage)
        {
            if (adv.Count == 0)
                return -1;
            foreach(TariffData td in adv )
            {
                int price_idx = 0;
                if( td.IsMatch(hour, out price_idx) )
                {
                    return energy * (price_values[price_idx]+overchage);
                }
            }
            return 0;
        }
        // Overcharge has to be calculated elsewhere
        public float HourlyFee(DateTime hour, float energy, float overcharge)
        {
            float cost = 0;

            if(tariffs==1)
                cost = SingleTariffFeePerHour(energy, overcharge);
            else
            {
                if( simple )
                    cost = SimplePeriodsHourlyFee(hour, energy, 0/*overcharge*/);
                else
                    cost = AdvancedPeriodsHourlyFee(hour, energy, 0/*overcharge*/);
            }
            cost = MultiplyTheRestPerHour(cost);
            return cost;
        }
    }

    public static class XMLUtil<T> where T:class
    {
        public static bool SaveXml(string xml, T value)
        {
            string path = Path.GetFullPath(xml);
            if (path == null)
                return false;
            DirectoryInfo di = new DirectoryInfo(Path.GetDirectoryName(path));
            if(!di.Exists)
            {
                di.Create();
            }
            disc1.XML.ObjectXMLSerializer<T>.Save(value, xml);
            return true;
        }
        public static T LoadXml(string xml)
        {
            if (!File.Exists(xml))
                return null;
            return disc1.XML.ObjectXMLSerializer<T>.Load(xml);
        }
    }
}
