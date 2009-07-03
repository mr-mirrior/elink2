using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class PercentageLarge : UserControl
    {
        public PercentageLarge()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            goodcolor = lbPercent.ForeColor;
            badcolor = dotBad.BackColor;
            IPercentage.I.Inform += OnReload;
        }
        Color nacolor = Color.Gray;
        Color goodcolor = Color.FromArgb(0, 98, 170);//Color.Green;
        Color badcolor = Color.OrangeRed;//Color.Maroon;
        private void OnReload(object sender, EventArgs e)
        {
            IPercentage p = sender as IPercentage;
            if (p.NA)
            {
                lbPercent.ForeColor = nacolor;
                lbPercent.Text = "N/A";
                return;
            }
            if (p.Good)
                lbPercent.ForeColor = goodcolor;
            else
                lbPercent.ForeColor = badcolor;
            lbPercent.Text = p.Percentage;

            dotGood.BackColor = goodcolor;
            dotBad.BackColor = badcolor;

            string tip = p.Tip;

            toolTip1.SetToolTip(lbPercent, tip);

        }
    }
}
