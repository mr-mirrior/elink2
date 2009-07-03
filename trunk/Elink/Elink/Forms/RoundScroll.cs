using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class RoundScroll : UserControl
    {
        int min = 0;
        int max = 100;
        int pos = 0;
        float step = 0.1f;

        Color fore = Color.FromArgb(0, 98, 170);
        Color back = Color.LightGray;
        public event EventHandler Scrolled;

        public RoundScroll()
        {
            InitializeComponent();
            t1.Tick += OnTimer;
            t1.Interval = 100;
        }
        [DefaultValue(typeof(int), "0"), Category("Appearance"), Description("最小值")]
        public int Min { get { return min; } set { min = value; Redraw(); } }
        [DefaultValue(typeof(int), "100"), Category("Appearance"), Description("最大值")]
        public int Max { get { return max; } set { max = value; Redraw(); } }
        [DefaultValue(typeof(int), "0"), Category("Appearance"), Description("当前位置")]
        public int Pos { get { return pos; } set { pos = value; Reposition(); /*InformScroll();*/ } }
        [DefaultValue(typeof(Color), "0, 98, 170"), Category("Appearance"), Description("前景色")]
        public Color Fore { get { return fore; } set { fore = value; Redraw(); } }
        [DefaultValue(typeof(Color), "LightGray"), Category("Appearance"), Description("背景色")]
        public Color Back { get { return back; } set { back = value; Redraw(); } }
        public void Reposition()
        {
            Rectangle rc = ClientRectangle;
            //rc.Width -= 2;
            rc.Height -= 2;

            rndBack.Height = rc.Height;
            rndFore.Height = rc.Height - 2;

            // 过滤
            pos = Math.Max(pos, min);
            pos = Math.Min(pos, max);

            int safe_blank = 8;
            int left = safe_blank;
            int right = rndBack.Right - safe_blank;
            if (right <= left)
                return;

            int visBackWidth = rc.Width - 2 * safe_blank;
            int visForeWidth = 88;
            int wholeWidth = visBackWidth - visForeWidth;
            step = (float)wholeWidth / (max - min);

            if (step < 0.0001)
                return;

            int x, y;
            x = rndFore.Location.X;
            y = rndFore.Location.Y;

            x = left + (int)Math.Round(step * pos);
            rndFore.Location = new Point(x, y);

            //InformScroll();

        }
        public void Redraw()
        {
            Reposition();
            rndFore.DownColor = fore;
            rndBack.BackColor = back;
        }

        private void RoundScroll_Resize(object sender, EventArgs e)
        {
            Redraw();
        }
        bool isDown = false;
        Point downPos = new Point();
        private void rndFore_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isDown = true;
                downPos = e.Location;
            }
        }
        Timer t1 = new Timer();
        private void OnTimer(object sender, EventArgs e)
        {
            t1.Stop();
            if (Scrolled != null)
            {
                Scrolled.Invoke(this, new EventArgs());
            }
        }
        private void InformScroll()
        {
            t1.Start();
        }
        private void rndFore_MouseMove(object sender, MouseEventArgs e)
        {
            if( isDown )
            {
                int dx = e.Location.X - downPos.X;
                float dpos = dx / step;
                if( Math.Abs(dpos) >= 1 )
                {
                    pos += (int)Math.Round(dpos);
                    Reposition();
                }
            }
        }

        private void rndFore_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                isDown = false;
                //Reposition();
                InformScroll();
            }
        }

        private void rndBack_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void rndBack_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //string s = "";
                //s = string.Format("rndFore: {0}, e.Pos: {1}", rndFore.ClientRectangle.ToString(), e.Location.ToString());
                //MB.OKI(s);

                if (e.Y > rndFore.Top &&
                    e.Y < (rndFore.Bottom))
                {
                    if (e.X < rndFore.Left)
                    {
                        pos--;
                        Reposition();
                        InformScroll();
                    }
                    if (e.X > (rndFore.Right))
                    {
                        pos++;
                        Reposition();
                        InformScroll();
                    }
                }
            }

        }

    }
}
