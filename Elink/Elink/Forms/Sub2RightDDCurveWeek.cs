using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Elink
{
    public partial class Sub2RightDDCurveWeek : UserControl, IDisplayData
    {
        public Sub2RightDDCurveWeek()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
//             cylinders = new RoundRectControl[] {
//                 jan, feb, mar, apr, may, jun,
//                 jul, aug, sep, oct, nov, dec
//             };
//             UpdateData();
            weekdays = new CheckBox[] { ck1, ck2, ck3, ck4, ck5, ck6, ck7};
            thisyear = DateTime.Today.Year;
        }
        List<List<float>> values = null;
        float maxValue = 0.5f;
        public float MaxValue
        {
            get { return maxValue; }
            set { if (!Is.ValidF(value)) value = 0; maxValue = value; SetUnits(); }
        }
        CheckBox[] weekdays;
        void SetUnits()
        {
            double u = maxValue / 3;
            double pt = u;
            lb1.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb2.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb3.Text = string.Format("{0:0.000}", pt);
//             pt += u;
//             lb4.Text = string.Format("{0:0.000}", pt);
//             pt += u;
//             lb5.Text = string.Format("{0:0.000}", pt);
        }
        //DateTime start;
        public string Unit { get { return lbUnit.Text; } set { /*lbUnit.Text = value;*/ } }
//         public void SetValue(int weekday, int hour, float value)
//         {
//             value = Math.Max(value, 0);
//             value = Math.Min(value, maxValue);
//             values[weekday][hour] = (float)value;
//         }
        GraphicsPath[] gpweekdays = new GraphicsPath[7];
        private GraphicsPath CreatePath(int day)
        {
            if (day < 0 || day > 6)
                return null;
            GraphicsPath gp = new GraphicsPath();
            Rectangle rc = plBounds.ClientRectangle;
            float xstep = (float)rc.Width / 24;
            float ystep = (float)(rc.Height / maxValue);
            float x = 0, y;
            List<PointF> pts = new List<PointF>();
            for (int i = 0; i < 24; i++ )
            {
                float f = values[day][i];
                if (Is.ValidF(f) && f >= 0)
                {
                    y = f * ystep;
                    pts.Add(new PointF(x + plBounds.Left, plBounds.Bottom - y));
                }
                else
                    pts.Add(new PointF(x + plBounds.Left, plBounds.Bottom));
                x += xstep;
            }
            if (pts.Count == 0)
                return null;
            if (mode==1)
            {
                gp.AddCurve(pts.ToArray()/*, .3f*/);
            }
            else
            {
                gp.AddLines(pts.ToArray());
            }
            return gp;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (values == null)
                return;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (int i = 0; i < weekdays.Length; i++ )
            {
                if (!weekdays[i].Checked || gpweekdays[i]==null )
                    continue;
                using (Pen p = new Pen(Color.Black))
                {
                    p.Width = 4f;
                    p.Color = Color.FromArgb(0x80,weekdays[i].ForeColor);
                    p.LineJoin = LineJoin.Miter;
                    p.MiterLimit = 0;
                    g.DrawPath(p, gpweekdays[i]);
                }
            }
        }
        public void UpdateData()
        {
            panel1.Visible = false;

            values = EnergyData.QueryGlobalWeekdayAverage(0);
            MaxValue = EnergyData.GlobalMax;

            UpdateGraphs();
//             Random rnd = new Random((int)DateTime.Now.Ticks);
//             for (int j = 0; j < 7; j++)
//             {
//                 for (int i = 0; i < 24; i++)
//                 {
//                     float x = (float)(rnd.Next((int)(maxValue * 1000))) / 1000;
//                     //int y = rnd.Next(12);
//                     SetValue(j, i, x);
//                 }
//                 //System.Threading.Thread.Sleep(500);
//             }

            panel1.Visible = true;
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }
        public void UpdateGraphs()
        {
            if (values == null)
                return;
            for (int i = 0; i < weekdays.Length; i++)
            {
                if( gpweekdays[i] != null )
                {
                    gpweekdays[i].Dispose();
                    gpweekdays[i] = null;
                }
                if (weekdays[i].Checked)
                {
//                     UpdateData();
                    gpweekdays[i] = CreatePath(i);
                }
            }
            Refresh();
        }
        private void ck1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraphs();
        }

        int mode = 1;
        public void SetMode(int m)
        {
            // 0: NO SMOOTH
            // 1: SMOOTH
            if (m < 0 || m > 2)
                return;
            mode = m;
            UpdateGraphs();
        }
        int thisyear = 0;
        public void SetDay(DateTime d)
        {
            thisyear = d.Year;
            lbYear.Text = thisyear.ToString();
            UpdateData();
        }

        private void Sub2RightDDCurveWeek_Load(object sender, EventArgs e)
        {
            SetDay(DateTime.Today);
            //UpdateData();

            if (values == null)
                return;


                    weekdays[(int)DateTime.Now.DayOfWeek].Checked = true;          
        }

    }
}
