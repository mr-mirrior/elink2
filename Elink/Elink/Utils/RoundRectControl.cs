using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Windows.Forms;

namespace Elink
{
    public class RoundRectControl: Button, ICloneable//System.Windows.Forms.Label
    {
        private static List<RoundRectControl> RoundGroup = new List<RoundRectControl>();
        public RoundRectControl()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            RoundGroup.Add(this);
            MouseEnter += OnEnter;
            MouseLeave += OnLeave;
            this.Resize += OnResize;
            this.Click += OnClick;
        }
        protected override void Dispose(bool disposing)
        {
            RoundGroup.Remove(this);
            base.Dispose(disposing);
        }
        //private const string All = "All";
        //private const string Top = "Top";
        //private const string Bottom = "Bottom";
        //private string curvecorners = All;
        public enum CURVE_CORNERS
        {
            All,
            Top,
            Bottom
        };
        private CURVE_CORNERS curvecorners = CURVE_CORNERS.All;
        [DefaultValue(typeof(CURVE_CORNERS), "All"), Category("Appearance"), Description("圆角方向，可选范围：All/Top/Bottom")]
        public CURVE_CORNERS CurveCorners
        {
            get { return curvecorners; }
            set
            {
                if (!curvecorners.Equals(value))
                {
                    curvecorners = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    Reset();
                    this.Refresh();
                }
            }
        }
        private GraphicsPath CreateMyRoundRectangle(Rectangle rc, int radius)
        {
            Rectangle rec = rc;
            GraphicsPath gp = null;
            switch(curvecorners)
            {
                case CURVE_CORNERS.All:
                    if( is_vertical )
                    {
                        gp = CreateVRoundRectangle(rec, radius);
                    }
                    else
                        gp = CreateRoundRectangle(rec, radius);
                    break;
                case CURVE_CORNERS.Bottom:
                    gp = CreateBottomRadialPath(rec, radius);
                    break;
                case CURVE_CORNERS.Top:
                    gp =CreateTopRoundRectangle(rec, radius);
                    break;
                default:
                    gp = CreateTopRoundRectangle(rec, radius);
                    break;
            }
            return gp;
        }
        public static GraphicsPath CreateVRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
            path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
            path.AddLine(l, t + h - radius, l, t + radius); // left
            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.CloseFigure();
            return path;
        }
        public static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;
            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d-1, d-1, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h); // right
            path.AddLine(l + w, t + h, l, t + h); // bottom
            path.AddLine(l, t + h, l, t + radius); // left
            path.CloseFigure();
            return path;
        }

        private static GraphicsPath CreateBottomRadialPath(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int r = l + w;
            int b = t + h;
            int d = radius << 1;
            //path.StartFigure();
            path.AddArc(l, t, d, d, 90, 90);
            path.AddLine(l, b-d, l, t);
            path.AddLine(l, t, r, t);
            path.AddLine(r, b - d, r, t);
            path.AddArc(r - d, t, d, d, 0, 90);
            path.AddLine(l + d, b, r - d, b);
            path.CloseFigure();
            return path;
        }
        public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;
            if(w==h)
            {
                path.AddArc(rectangle, 0, 360);
                //path.CloseAllFigures();
                return path;
            }
            if ((w % 2) == 0)
                d -= 1;
            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
            path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
            path.AddLine(l, t + h - radius, l, t + radius); // left
            path.CloseFigure();
            return path;
        }
        private void Reset()
        {
            mybrush = null;
            mypath = null;
            mybrush = new SolidBrush(this.BackColor);
            Rectangle rc = this.ClientRectangle;
            if (curvecorners != CURVE_CORNERS.Top)
            {
                rc.Width-=2;
                rc.Height-=2;
            }
            int c = curve;
            if (curve < 1)
                c = rc.Height / 2;
            if (is_vertical)
                c = rc.Width / 2;
            mypath = CreateMyRoundRectangle(rc, c);
            //this.Region = new Region(mypath);
        }

        SolidBrush mybrush;
        GraphicsPath mypath;
        private void InitializeComponent()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            Reset();
        }
       
        private int curve = 0;
        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        /// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns>
        [DefaultValue(typeof(int), "-1"), Category("Appearance"), Description("圆边半径，-1表示高度的一半")]
        public int Curve
        {
            get { return curve; }
            set
            {
                if (!curve.Equals(value))
                {
                    curve = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    Reset();
                    this.Refresh();
                }
            }
        }
        Color backbackcolor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("圆角外面的颜色")]
        public Color BackBackColor
        {
            get { return backbackcolor; }
            set
            {
                if (!backbackcolor.Equals(value))
                {
                    backbackcolor = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    Reset();
                    this.Refresh();
                }
            }
        }
        Color gradientcolor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("BackColor到xxx的渐变颜色")]
        public Color GradientColor
        {
            get { return gradientcolor; }
            set
            {
                if (!gradientcolor.Equals(value))
                {
                    gradientcolor = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }
        private Color downcolor = Color.FromArgb(0, 98, 170);
        [DefaultValue(typeof(Color), "0, 98, 170"), Category("Appearance"), Description("保持按下的背景色")]
        public Color DownColor
        {
            get { return downcolor; }
            set
            {
                if (!downcolor.Equals(value))
                {
                    downcolor = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }
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
        private int group = 0;
        [DefaultValue(typeof(int), "0"), Category("Appearance"), Description("组别")]
        public int Group
        {
            get { return group; }
            set
            {
                if (!group.Equals(value))
                {
                    group = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    //this.Refresh();
                }
            }
        }
        private bool allowGradient = false;
        [DefaultValue(typeof(int), "false"), Category("Appearance"), Description("是否开启颜色渐变")]
        public bool AllowGradient
        {
            get { return allowGradient; }
            set
            {
                if (!allowGradient.Equals(value))
                {
                    allowGradient = value;
                    Refresh();
                }
            }
        }
        private bool allowgroup = false;
        [DefaultValue(typeof(int), "false"), Category("Appearance"), Description("是否分组")]
        public bool AllowGroup
        {
            get { return allowgroup; }
            set
            {
                if (!allowgroup.Equals(value))
                {
                    allowgroup = value;
                    //if (allowgroup)
                    //    this.Click += OnClick;
                    //else
                    //{
                    //    this.Click -= OnClick;
                    //    Group = 0;
                    //}
                }
            }
        }
        private void DrawFocusRect(PaintEventArgs e)
        {
            /*
            if (false && this.Focused && this.TabStop)
            {
                GraphicsPath shrink = new GraphicsPath();
                Rectangle rc = this.ClientRectangle;
                rc.Width -= 2;
                rc.Height -= 2;
                const int delta = 2;
                rc = new Rectangle(rc.Left + delta, rc.Top + delta, rc.Width - delta * 2, rc.Height - delta * 2);
                int c = curve;
                if (curve < 1)
                    c = rc.Height / 2;
                shrink = CreateRoundRectangle(rc, c);

                Pen pen = new Pen(Color.White);
                pen.DashStyle = DashStyle.Dash;
                pen.Width = 2;
                pen.DashPattern = new float[] { 3, 1 };

                e.Graphics.DrawPath(pen, shrink);
            }
             */
        }
        private class TransparentControl : Control
        {
            protected override void OnPaintBackground(PaintEventArgs pevent) { }
            protected override void OnPaint(PaintEventArgs e) { }
        }

        private Button imageButton;
        private void DrawForegroundFromButton(PaintEventArgs pevent)
        {
            if (imageButton == null)
            {
                imageButton = new Button();
                imageButton.Parent = new TransparentControl();
                imageButton.SuspendLayout();
                imageButton.BackColor = Color.Transparent;
                imageButton.FlatAppearance.BorderSize = 0;
                imageButton.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                imageButton.SuspendLayout();
            }
            imageButton.AutoEllipsis = AutoEllipsis;
            //if (Enabled)
            //{
            //    imageButton.ForeColor = ForeColor;
            //}
            //else
            //{
            //    imageButton.ForeColor = Color.FromArgb((3 * ForeColor.R + backColor.R) >> 2,
            //        (3 * ForeColor.G + backColor.G) >> 2,
            //        (3 * ForeColor.B + backColor.B) >> 2);
            //}
            imageButton.Font = Font;
            imageButton.RightToLeft = RightToLeft;
            //if (imageButton.Image != Image && imageButton.Image != null)
            //{
            //    imageButton.Image.Dispose();
            //}
            //if (Image != null)
            //{
            //    imageButton.Image = Image;
            //    if (!Enabled)
            //    {
            //        Size size = Image.Size;
            //        float[][] newColorMatrix = new float[5][];
            //        newColorMatrix[0] = new float[] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f };
            //        newColorMatrix[1] = new float[] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f };
            //        newColorMatrix[2] = new float[] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f };
            //        float[] arr = new float[5];
            //        arr[3] = 1f;
            //        newColorMatrix[3] = arr;
            //        newColorMatrix[4] = new float[] { 0.38f, 0.38f, 0.38f, 0f, 1f };
            //        System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix(newColorMatrix);
            //        System.Drawing.Imaging.ImageAttributes disabledImageAttr = new System.Drawing.Imaging.ImageAttributes();
            //        disabledImageAttr.ClearColorKey();
            //        disabledImageAttr.SetColorMatrix(matrix);
            //        imageButton.Image = new Bitmap(Image.Width, Image.Height);
            //        using (Graphics gr = Graphics.FromImage(imageButton.Image))
            //        {
            //            gr.DrawImage(Image, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, GraphicsUnit.Pixel, disabledImageAttr);
            //        }
            //    }
            //}
            //imageButton.ImageAlign = ImageAlign;
            //imageButton.ImageIndex = ImageIndex;
            //imageButton.ImageKey = ImageKey;
            //imageButton.ImageList = ImageList;
            imageButton.Padding = Padding;
            imageButton.Size = Size;
            imageButton.Text = Text;
            imageButton.TextAlign = TextAlign;
            imageButton.TextImageRelation = TextImageRelation;
            imageButton.UseCompatibleTextRendering = false;// UseCompatibleTextRendering;
            imageButton.UseMnemonic = UseMnemonic;
            imageButton.ResumeLayout();
            InvokePaintBackground(imageButton, pevent);
            InvokePaint(imageButton, pevent);
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint(e);
            OnPaintBackground(e);
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            if (backbackcolor != Color.Transparent)
                e.Graphics.Clear(backbackcolor);
            if (RightBackBackColor != Color.Transparent)
                e.Graphics.FillRectangle(new SolidBrush(RightBackBackColor), new Rectangle(Width / 2, 0, Width / 2, Height));
            if (e.Graphics.SmoothingMode != SmoothingMode.AntiAlias)
            {
                e.Graphics.CompositingMode = CompositingMode.SourceOver;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            }
            //using (SolidBrush trbr = new SolidBrush(Color.Transparent))
            //    e.Graphics.FillRectangle(trbr, ClientRectangle);
            //return;
            if (null == mypath || null == mybrush )
                Reset();
            Color back = this.BackColor;
            if (this.TabStop && this.Focused && FocusBackColor!=Color.Transparent)
                back = FocusBackColor;
            if (this.AllowGroup && this.IsDown)
                back = DownColor;
            if (this.is_hover_enabled && is_enter)
                back = DownColor;
            if (!IsHoverEnabled && IsDownEnabled && IsDown)
            {
                back = DownColor;
            }

            Brush backBr = new SolidBrush(back);
            if( allowGradient )
            {
                float angle = is_vertical ? 90 : 0;
                backBr = new LinearGradientBrush(ClientRectangle, BackColor, GradientColor, angle);
            }
            e.Graphics.FillPath(backBr, mypath);
            DrawFocusRect(e);
            DrawText(e.Graphics);
        }
        private void DrawText(Graphics g)
        {
            Rectangle rc = this.ClientRectangle;
            rc.Height -= 2;
            rc.Offset(this.Padding.Left, this.Padding.Top);
            rc.Width -= this.Margin.Right;
            StringFormat sf = ConvertAlign(this.TextAlign);
            g.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rc, sf);
        }
        public static StringFormat ConvertAlign(ContentAlignment a)
        {
            StringFormat sf = new StringFormat();
            switch(a)
            {
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            Reset();
            Refresh();
            base.OnSizeChanged(e);
        }

        private bool is_down_enabled = false;
        [DefaultValue(typeof(bool), "false"), Category("Appearance"), Description("是否允许保持按下")]
        public bool IsDownEnabled
        {
            get { return is_down_enabled; }
            set
            {
                if (!is_down_enabled.Equals(value))
                {
                    is_down_enabled = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }
        private bool is_down = false;
        [DefaultValue(typeof(bool), "false"), Category("Appearance"), Description("是否保持按下")]
        public bool IsDown
        {
            get { return is_down; }
            set
            {
                if (!is_down.Equals(value))
                {
                    is_down = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }
        public void OnClick(Object sender, EventArgs e)
        {
            //if( is_down )
            //{
            //    IsDown = false;
            //    return;
            //}
            if (AllowGroup)
            {
                if (IsDown)
                    return;
                IsDown = true;
                foreach (RoundRectControl i in RoundGroup)
                {
                    if (i == this)
                        continue;
                    if (i.AllowGroup)
                    {
                        if (i.Group == Group)
                        {
                            i.IsDown = false;
                        }
                    }
                }
            }
            else
                if (IsDownEnabled)
                {
                    IsDown = !IsDown;
                }
        }
        private bool is_hover_enabled = true;
        [DefaultValue(typeof(bool), "true"), Category("Appearance"), Description("是否允许Hover功能")]
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
        private Color rightbackbackcolor = Color.White;
        [DefaultValue(typeof(Color), "White"), Category("Appearance"), Description("右角背景色")]
        public Color RightBackBackColor
        {
            get { return rightbackbackcolor; }
            set
            {
                if (!rightbackbackcolor.Equals(value))
                {
                    rightbackbackcolor = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    //Reset();
                    this.Refresh();
                }
            }
        }

        private bool is_enter = false;
        public void OnEnter(Object sender, EventArgs e)
        {
            if (!is_hover_enabled)
                return;

            is_enter = true;
            Refresh();
        }
        public void OnLeave(Object sender, EventArgs e)
        {
            if (!is_hover_enabled)
                return;
            is_enter = false;
            Refresh();
        }
        private bool is_vertical = false;
        [DefaultValue(typeof(bool), "false"), Category("Appearance"), Description("垂直方向")]
        public bool Vertical
        {
            get { return is_vertical; }
            set
            {
                if(!is_vertical.Equals(value))
                {
                    is_vertical = value;
                    Reset();
                    Refresh();
                }
            }
        }

        public object Clone()
        {
            RoundRectControl copy = new RoundRectControl();
            copy.is_vertical = is_vertical;
            copy.curve = curve;
            copy.allowGradient = allowGradient;
            copy.allowgroup = allowgroup;
            copy.backbackcolor = backbackcolor;
            copy.rightbackbackcolor = rightbackbackcolor;
            copy.BackColor = BackColor;
            copy.Location = new Point(Location.X, Location.Y);
            copy.Width = Width;
            copy.Height = Height;
            copy.IsDown = IsDown;
            copy.IsDownEnabled = IsDownEnabled;
            copy.IsHoverEnabled = IsHoverEnabled;
            copy.Text = Text;
            copy.TextAlign = TextAlign;
            copy.DownColor = DownColor;
            copy.Visible = Visible;

            return copy;
        }
        
        object userDefined;
        public object TagDouble { get { return userDefined; } set { userDefined = value; } }

        public void OnResize(object sender, EventArgs e)
        {
            if( this.is_vertical )
            {
                if (this.Height < this.Width)
                    this.Size = new Size(this.Width, this.Width);
            }
            else
            {
                //if (this.Width < 16 )
                //{
                //    this.Size = new Size(16, this.Height);
                //}
            }
        }
    }
}
