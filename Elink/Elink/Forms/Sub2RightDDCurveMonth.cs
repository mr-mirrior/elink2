using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Elink
{
    public partial class Sub2RightDDCurveMonth : UserControl, IDisplayData
    {
        public Sub2RightDDCurveMonth()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
//             cylinders = new RoundRectControl[] {
//                 jan, feb, mar, apr, may, jun,
//                 jul, aug, sep, oct, nov, dec
//             };
//             UpdateData();
//             values = new float[12][];
//             for (int i = 0; i < 12; i++ )
//             {
//                 values[i] = new float[24];
//             }
            months = new CheckBox[] { ck1, ck2, ck3, ck4, ck5, ck6, 
                ck7,ck8,ck9,ck10,ck11,ck12};
            maxBalls = new RoundFrame[12];
            for (int i = 0; i < 12; i++ )
            {
                maxBalls[i] = new RoundFrame();
                maxBalls[i].Size = new Size(16,16);
                maxBalls[i].FillColor = months[i].ForeColor;
                maxBalls[i].Visible = false;
                panel1.Controls.Add(maxBalls[i]);
                maxBalls[i].BringToFront();
                maxBalls[i].FrameColor = Color.White;
                //plBounds.Controls.Add(maxBalls[i]);
            }
        }
        RoundFrame[] maxBalls;
        CheckBox[] months;
        float maxValue = 0.5f;
        public float MaxValue
        {
            get { return maxValue; }
            set { if (!Is.ValidF(value)) value = 0; maxValue = value; SetUnits(); }
        }
        void SetUnits()
        {
            double u = maxValue / 3;
            double pt = u;
            lb1.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb2.Text = string.Format("{0:0.000}", pt);
            pt += u;
            lb3.Text = string.Format("{0:0.000}", pt);
        }
        public string Unit { get { return lbUnit.Text; } set { /*lbUnit.Text = value;*/ } }
        List<List<float>> values = null;
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        void LocateBall(int i)
        {
            if (monthmax == null)
                return;
            if (i < 0 || i > 11)
                return;
            if (gpmonth[i] == null)
            {
                maxBalls[i].Visible = false;
                return;
            }
            string tip = null;
            if (monthmax[i].value != float.MinValue)
            {
                tip = string.Format(
                    GetString("s4000", "{0:F} {1:0.000}kW"),
                    monthmax[i].when, monthmax[i].value);
                maxBalls[i].Visible = true;
                toolTip1.SetToolTip(maxBalls[i], tip);
                // relocate
                float percent = monthmax[i].value / maxValue;
                float h = percent * plBounds.Height;
                float x = /*plBounds.Left + */monthmax[i].when.Hour*((float)plBounds.Width / 24);
                float y = plBounds.Height - h;

                x -= maxBalls[i].Width/2;
                y -= maxBalls[i].Height/2;
                x += plBounds.Left;
                y += plBounds.Top;
                maxBalls[i].Location = new Point((int)x, (int)y);
                maxBalls[i].Tag = monthmax[i];
                maxBalls[i].MouseUp -= OnClickMaxBall;
                maxBalls[i].MouseUp += OnClickMaxBall;
            }
            else
            {
                toolTip1.SetToolTip(maxBalls[i], null);
                maxBalls[i].Visible = false;
            }
        }
        GraphicsPath[] gpmonth = new GraphicsPath[12];
        GraphicsPath CreatePath(int month)
        {
            if (month < 0 || month> 11)
                return null;
            GraphicsPath gp = new GraphicsPath();
            Rectangle rc = plBounds.ClientRectangle;
            float xstep = (float)rc.Width / 24;
            float ystep = (float)(rc.Height / maxValue);
            float x = 0, y;
            List<PointF> pts = new List<PointF>();
            for (int i = 0; i < 24; i++)
            {
                float f = values[month][i];
                if (Is.ValidF(f) && f>=0)
                {
                    y = f * ystep;
                    pts.Add(new PointF(x + plBounds.Left, plBounds.Bottom - y));
                }
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

            for (int i = 0; i < 12; i++ )
            {
                if (!months[i].Checked || gpmonth[i] == null)
                {
                    maxBalls[i].Visible = false;
                    continue;
                }
                using (Pen p = new Pen(Color.Black))
                {
                    p.Width = 4f;
                    p.Color = Color.FromArgb(0x80, months[i].ForeColor);
                    p.LineJoin = LineJoin.Miter;
                    p.MiterLimit = 0;
                    g.DrawPath(p, gpmonth[i]);
                }
            }
        }
        List<TimeValuePair> monthmax = null;
        public void UpdateData()
        {

            values = //EnergyDataNew.CurveMonthsAverage(now.Year, out monthmax);
                EnergyData.QueryGlobalMonthsAverage(now.Year, out monthmax);
            float mx = EnergyData.GlobalMax;//EnergyDataNew.max_e;
            if( monthmax != null)
            {
                for (int i = 0; i < monthmax.Count; i++)
                {
                    if (mx < monthmax[i].value)
                        mx = monthmax[i].value;
                }
            }
            MaxValue = mx;

            UpdateGraphs();

        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void Sub2RightDDCurveMonth_Load(object sender, EventArgs e)
        {
            now = DateTime.Now;
            UpdateData();

            if (monthmax == null)
                return;
            //for (int i = 0; i < 12; i++ )
            //{
            //    if (/* DT.IsStrictlyValid(monthmax[i].when)&&*/monthmax[i].when.Month == DateTime.Now.Month)
            //    {
                    months[DateTime.Now.Month-1].Checked = true;
            //    }
            //}
        }
        public void UpdateGraphs()
        {
            if (values == null)
                return;

            for (int i = 0; i < months.Length; i++)
            {
                if (gpmonth[i] != null)
                {
                    gpmonth[i].Dispose();
                    gpmonth[i] = null;
                }
                if (months[i].Checked)
                {
                    //                     UpdateData();
                    gpmonth[i] = CreatePath(i);
                }
                LocateBall(i);
            }
            Refresh();
        }
        
        private void ck12_CheckedChanged(object sender, EventArgs e)
        {
            //panel1.Visible = false;
            UpdateGraphs();
            //panel1.Visible = true;
        }

        int mode = 1;
        public void SetMode(int m)
        {
            // 0: NO SMOOTH
            // 1: SMOOTH
            if (m < 0 || m > 1)
                return;
            mode = m;
            UpdateGraphs();
        }

        DateTime now;
        public void SetDay(DateTime d)
        {
            now = d;
            lbYear.Text = now.Year.ToString();
            UpdateData();
        }

        private void OnClickMaxBall(object sender, MouseEventArgs e)
        {
            if( e.Button == MouseButtons.Left )
            {
                RoundFrame rf = (RoundFrame)sender;
                TimeValuePair tv = (TimeValuePair)rf.Tag;
                JumpWindows.TriggerGoTime(tv.when);
            }
        }
    }
}
