using System;
using System.Collections.Generic;
using System.Text;

namespace Elink
{
    public class TwoWorld: ICloneable
    {
        private DateTime e2;
        private DateTime pc;
        public object Clone()
        {
            return this.MemberwiseClone();
        }
        public bool IsValid
        {
            get { return DT.IsStrictlyValid(e2) && DT.IsStrictlyValid(pc); }
        }
        public TwoWorld()
        {
            e2 = DateTime.MinValue;
            pc = DateTime.MinValue;
        }

        public TwoWorld(TwoWorld tw)
        {
            e2 = DateTime.FromBinary(tw.e2.ToBinary());
            pc = DateTime.FromBinary(tw.pc.ToBinary());
        }
        public TwoWorld(DateTime e, TimeSpan pctoe2)
        {
            e2 = e;
            pc = e + pctoe2;
        }
        public TimeSpan Diff()
        {
            return pc - e2;
        }
        public TwoWorld(DateTime e, DateTime p)
        {
            e2 = e;
            pc = p;
        }
        public DateTime E2Now()
        {
            return PC2E2Time(DateTime.Now);
        }
        public DateTime E2Time 
        { 
            get { return e2; } 
            set
            {
                if (!IsValid)
                    return;
                TimeSpan diff = e2 - value;
                e2 = value;
                pc -= diff;
            }
        }
        public DateTime PCTime { get { return pc; } set { pc = value; } }
        public DateTime PC2E2Time(DateTime pctime)
        {
            TimeSpan diff = pctime - pc;
            return e2.Add(diff);
        }
        public override string ToString()
        {
            return e2.ToString("", System.Globalization.CultureInfo.InvariantCulture) + " " +
                (e2-pc).ToString();
        }
        public void ToLocal()
        {
            if (!IsValid)
                return;
            if (e2.Kind != DateTimeKind.Local)
                e2 = e2.ToLocalTime();
            if (pc.Kind != DateTimeKind.Local)
                pc = pc.ToLocalTime();
        }
//         public void ToUniversal()
//         {
//             if (!IsValid)
//                 return;
//             if (e2.Kind != DateTimeKind.Utc)
//                 e2 = e2.ToUniversalTime();
//             if (pc.Kind != DateTimeKind.Utc)
//                 pc = pc.ToUniversalTime();
//         }
    }

    public class DataFromE2
    {
        // this is been set in start of CollectData() of SerialCOM
        // in universal format
        public DateTime now_in_E2;
        public DateTime now_in_PC;
        public float firmware_version = 0;
        public float version = 1.1f;
        public string tariff;

        // 3x24x240, kwh in each hour for 240 days
        public float[] kwh_240days = new float[24 * 240];
        // 3x8, kwh for each of 8 days
        public float[] kwh_8days = new float[8];
        // 3x8, kwh for each of 8 weeks
        public float[] kwh_8weeks = new float[8];
        // 3x25, kwh for each of 25 months
        public float[] kwh_25months = new float[25];
        // 3x13, max instant power for each of 13 months
        public float[] max_power_13months = new float[13];

#if NEW_FORMAT_MAX_POWER
        public int[] when_max_power_13months = new int [13];
#endif

        // weekday: based pointer 1
        public unsafe void StoreHourly(byte* data, int day)
        {
            if (day < 1 || day > 71)
                return;
            //valid_until = DateTime.Now;
            for (int i = 0; i < 24; i++)
            {
                float x = Serial.ELMsg.CombineFloat(data[i * 3 + 0], data[i * 3 + 1], data[i * 3 + 2]);
                kwh_240days[(day - 1) * 24 + i] = x;
                if (x > 10)
                    System.Diagnostics.Debug.Print("{0}, {1:X} {2:X} {3:X}",
                        x, data[i * 3 + 0], data[i * 3 + 1], data[i * 3 + 2]);
            }
        }
    }

}
