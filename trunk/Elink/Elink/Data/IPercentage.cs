using System;
using System.Collections.Generic;
using System.Text;

namespace Elink
{
    class IPercentage
    {
        public static readonly IPercentage I = new IPercentage();

        public EventHandler Inform;
        private float percentage = float.NaN;
        public float real = 0;
        public float target = 0;
        public string period;
        public float Percent
        {
            get
            {
                return Math.Abs(percentage);
            }
            set
            {
                if( value == float.PositiveInfinity ||
                    value == float.NegativeInfinity ||
                    value == float.NaN )
                {
                    value = float.NaN;
                }
                if( value < 0 )
                {
                    good = true;
                    percentage = -value;
                }
                else
                {
                    good = false;
                    percentage = value;
                }
                //percentage = Math.Min(100, value);
                InformChange();
            }
        }
        public float AbsPercent
        {
            get
            {
                return Math.Abs(percentage);
            }
        }
        private bool good = true;
        public bool Good
        {
            get { return good; }
            set
            {
                if (value != good)
                {
                    good = value;
                    InformChange();
                }
            }
        }
        public bool NA
        {
            get { return percentage==float.PositiveInfinity|| percentage==float.NegativeInfinity || 
                percentage==float.MinValue || percentage==float.MinValue||
                float.IsNaN(percentage) || float.IsNaN(real) || float.IsNaN(target); }
        }
        private string GetString(string key, string def)
        {
            return Global.GetString(key,def);
        }
        public string Tip
        {
            get
            {
                if (NA)
                    return "N/A";
                else
                return string.Format(
                    GetString("s3000", "Target: {1:0.00}¤/day. Actual result: {0:0.00}¤/day"),
                    real, target, period);
            }
        }
        public string Percentage
        {
            get
            {
                if (NA)
                    return "N/A";
                return AbsPercent.ToString("0.0")+"%";
            }
        }
        public void InformChange()
        {
            if (Inform != null)
                Inform.Invoke(this, new EventArgs());
            //foreach (EventHandler e in Inform.GetInvocationList())
            //{
            //    e.Invoke(this, new EventArgs());
            //}
            /*
            if (percentage == -1)
            {
                lbPercent.ForeColor = nacolor;
                lbPercent.Text = "n/a";
                return;
            }
            if (good)
                lbPercent.ForeColor = goodcolor;
            else
                lbPercent.ForeColor = badcolor;
            lbPercent.Text = string.Format("{0:D}%", percentage);
             */
        }
    }
}
