using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class DashLabel : Label
    {
        public DashLabel()
        {
            InitializeComponent();
        }

        public DashLabel(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        int dashlength = 1;
        [Category("Appearance"), Description("虚线长度")]
        public int DashLength
        {
            get { return dashlength; }
            set
            {
                //if (!dashlength.Equals(value))
                {
                    dashlength = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        int width = 1;
        [Category("Appearance"), Description("宽度")]
        public int DashWidth
        {
            get { return width; }
            set
            {
                if (value != 0)
                {
                    width = value;
                    //OnBackColorChanged(EventArgs.Empty);
                    this.Refresh();
                }
            }
        }
        
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Rectangle rc = this.ClientRectangle;
            Pen p = new Pen(this.ForeColor);
            if (dashlength != 0)
            {
                p.DashStyle = DashStyle.Dash;
                p.DashPattern = new float[] { dashlength, dashlength };
            }
            if (width != 0)
                p.Width = width;
            //p.DashPattern
            e.Graphics.DrawLine(p, rc.Left, rc.Top, rc.Right, rc.Top);
        }
    }
}
