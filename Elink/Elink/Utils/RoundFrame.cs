using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Elink
{
    public partial class RoundFrame : Label
    {
        public RoundFrame()
        {
            InitializeComponent();
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            BackColor = Color.Transparent;

            this.MouseEnter += OnMouseEnter;
            this.MouseLeave += OnMouseLeave;
        }

//         Color frame = Color.Black;
//         Color fill = Color.White; 
//         [DefaultValue(typeof(Color), "Black"), Category("Appearance"), Description("边框颜色")]
//         public Color FrameColor
//         {
//             get { return frame; }
//             set { frame = value; }
//         }
//         [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("填充颜色")]
//         public Color FillColor
//         {
//             get { return frame; }
//             set { frame = value; }
//         }
        private void DrawCaption(Graphics g)
        {
            if (Text.Length == 0)
                return;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(Text, this.Font, new SolidBrush(ForeColor),
                ClientRectangle,
                sf);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            int curvehere = curve;
            Rectangle rc = ClientRectangle;
            if (curve == -1 || curve==0)
                curvehere = rc.Height / 2;
            rc.Width -= 1;
            rc.Height -= 1;

            if (rc.Width <= 0 || rc.Height <= 0 || curvehere <=0 )
                return;

            //rc.Offset(1, 1);
            GraphicsPath gp = RoundRectControl.CreateRoundRectangle(rc, curvehere);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Color enable = inside;
            if (is_hover_enabled && is_mouse_over)
                enable = focusbackcolor;
            if (!this.Enabled)
                enable = Color.Gainsboro;
            SolidBrush bb = new SolidBrush(enable);
            
            using(bb)
                e.Graphics.FillPath(bb, gp);
            if (FrameColor != Color.Transparent)
            {
                using (Pen bf = new Pen(FrameColor, 1.2f))
                {
                    bf.Width = thick;
                    bf.EndCap = LineCap.ArrowAnchor;
                    //bf.CustomEndCap.BaseCap = LineCap.Custom;
                    //bf.CustomEndCap.BaseInset = thick*2;
                    //bf.CustomEndCap.WidthScale = thick * 3;
                    e.Graphics.DrawPath(bf, gp);
                }
            }
            gp.Dispose();

            DrawCaption(e.Graphics);
        }
        Color inside = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("内部填充色")]
        public Color FillColor
        {
            get { return inside; }
            set
            {
                if (!inside.Equals(value))
                {
                    inside = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        Color framecolor = Color.DimGray;
        [DefaultValue(typeof(Color), "DimGray"), Category("Appearance"), Description("Frame Color")]
        public Color FrameColor
        {
            get { return framecolor; }
            set
            {
                if (!framecolor.Equals(value))
                {
                    framecolor = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        private float thick = 1.2f;
        [DefaultValue(typeof(float), "1.2"), Category("Appearance"), Description("Thickness of outline")]
        public float Thickness
        {
            get { return thick; }
            set
            {
                if (value <= 0)
                    return;
                if (!thick.Equals(value))
                {
                    thick = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        private int curve = 0;
        [DefaultValue(typeof(int), "0"), Category("Appearance"), Description("圆边半径，-1表示高度的一半")]
        public int Curve
        {
            get { return curve; }
            set
            {
                if (!curve.Equals(value))
                {
                    curve = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    this.Refresh();
                }
            }
        }

        //private string text = "";
//         [EditorBrowsable(EditorBrowsableState.Always)]
//         [DefaultValue(typeof(int), "button"), Category("Appearance"), Description("Text")]
//         public override string Text
//         {
//             get { return base.Text; }
//             set
//             {
//                 if (!base.Text.Equals(value))
//                 {
//                     base.Text = value;
//                     //OnBackColorChanged(EventArgs.Empty);
//                     this.Refresh();
//                 }
//             }
//         }

        private Color focusbackcolor = Color.Transparent;//Color.FromArgb(64,64,64);
        [DefaultValue(typeof(Color), "Transparent"), Category("Appearance"), Description("聚焦背景色")]
        public Color FocusBackColor
        {
            get { return focusbackcolor; }
            set
            {
                if (!focusbackcolor.Equals(value))
                {
                    focusbackcolor = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }

        private bool is_hover_enabled = false;
        [DefaultValue(typeof(bool), "False"), Category("Appearance"), Description("是否允许Hover功能")]
        public bool IsHoverEnabled
        {
            get { return is_hover_enabled; }
            set
            {
                if (!is_hover_enabled.Equals(value))
                {
                    is_hover_enabled = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }

        private bool is_mouse_over = false;
        private void OnMouseEnter(object sender, EventArgs e)
        {
            if (!is_hover_enabled)
                return;
            is_mouse_over = true;
            Refresh();
        }
        private void OnMouseLeave(object sender, EventArgs e)
        {
            if (!is_hover_enabled)
                return; 
            is_mouse_over = false;
            Refresh();
        }
    }
}
