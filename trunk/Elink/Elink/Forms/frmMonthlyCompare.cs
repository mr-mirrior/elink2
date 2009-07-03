using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class frmMonthlyCompare : Form
    {
        EnergyData real = null;
        EnergyData simulation = null;
        public frmMonthlyCompare(EnergyData r, EnergyData s)
        {
            real = r;
            simulation = s;
            Global.GoMultiLanguage();
            InitializeComponent();
            //this.TopMost = true;
        }
        private void SetMax(float f)
        {
            if( f==0.0f )
                return;
            float u = f / 4;
            float step = u;
            lb1.Text = step.ToString("0.000");
            step += u;
            lb2.Text = step.ToString("0.000");
            step += u;
            lb3.Text = step.ToString("0.000");
            step += u;
            lb4.Text = step.ToString("0.000");
        }
        /// <summary>
        /// 更新柱状图
        /// </summary>
        /// <param name="r">真实价格</param>
        /// <param name="s">模拟价格</param>
        private void UpdateCylinders(float r, float s)
        {
            //if (r == 0)
            //    return;
            if (!Is.ValidF(r) || !Is.ValidF(s))
                return;

            float max = Math.Max(r, s);
            SetMax(max);

            Rectangle rc = panel1.ClientRectangle;
            Rectangle rcb = plBounds.ClientRectangle;
            float h1 = (max - r) / max; // 0
            float h2 = (max - s) / max; // 0.3
            h1 = 1 - h1;    // 1
            h2 = 1 - h2;    // 0.7
            h1 *= rcb.Height;
            h2 *= rcb.Height;

            realprice.Height = (int)h1;
            realprice.Top = dashLabel1.Top - (int)h1 - ((r==0)?realprice.Height:0);
            simprice.Height = (int)h2;
            simprice.Top = dashLabel1.Top - (int)h2;

            if (realprice.Bottom > dashLabel1.Top)
                realprice.Top = dashLabel1.Top - realprice.Height;
            if (simprice.Bottom > dashLabel1.Top)
                simprice.Top = dashLabel1.Top - simprice.Height;
        }
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        // 比率 = (模拟价格-真实价格)/真实价格
        private void LoadPage()
        {
            if (real == null || simulation == null)
                return;

            float total_real = real.TotalCost();
            float total_simulation = simulation.TotalCost();

            float difference = total_simulation - total_real;
            float ratio = 0;
            if( total_real != 0.0 )
            {
                if (difference < 0)
                    ratio = difference * 100 / total_simulation;
                else
                    ratio = difference * 100 / total_real;
            }

            string currency = Settings.I.currency;
            DateTime month = real.valid_since.E2Time;
            lbMonth.Text = month.ToString("Y");
            lbRealName.Text = real.TariffName();
            lbSimName.Text = simulation.TariffName();
//             this.Text = string.Format(
//                 //GetString("s2000", "{1} in {0} with simulation"), 
//                 lbMonth.Text, lbSimName.Text);
            lbReal.Text = currency+total_real.ToString("0.00");
            lbSim.Text = currency+total_simulation.ToString("0.00");
            lbCurrency.Text = Settings.I.currency;

            if( difference < 0 )
            {
                // good
                pic.Image = Properties.Resources.good_smile;
                lbDifference.ForeColor = lbRatio.ForeColor = eColor.MyBlue;
                //difference = -difference;
                ratio = -ratio;
                lbDifference.Text = difference.ToString("0.00") + currency;
                lbRatio.Text = ratio.ToString("0.0") + GetString("s3001", "% Cheaper");
                // 16% más barato
                lbConclusion.Text = GetString("s3004", "Good");
                lbConclusion.ForeColor = eColor.MyBlue;
            }
            else
                if( difference > 0 )
                {
                    // bad
                    pic.Image = Properties.Resources.bad_cry;
                    lbDifference.ForeColor = lbRatio.ForeColor = Color.OrangeRed;
                    lbDifference.Text = difference.ToString("+0.00") + currency;
                    lbRatio.Text = ratio.ToString("0.0") +  GetString("s3002", "% More expensive");
                    // 4% más caro
                    lbConclusion.Text = GetString("s3005", "Bad");
                    lbConclusion.ForeColor = Color.OrangeRed;
                }
                else
                {
                    // equals
                    pic.Image = Properties.Resources.just_soso;
                    lbDifference.ForeColor = lbRatio.ForeColor = Color.Silver;
                    lbDifference.Text = difference.ToString("+0.00") + currency;
                    lbRatio.Text = ratio.ToString("0.0") + "%";
                    // Similar
                    lbConclusion.Text = GetString("s3003", "Similar");
                    lbConclusion.ForeColor = Color.DimGray;
                }

            UpdateCylinders(total_real, total_simulation);
        }
        private void MonthlyCompare_Load(object sender, EventArgs e)
        {
            //pic.Image = Properties.Resources.good_smile;
            LoadPage();
        }

        private void MonthlyCompare_Resize(object sender, EventArgs e)
        {
            Rectangle rc = ClientRectangle;
            Rectangle rcPanel = panel1.ClientRectangle;

            int x, y;
            x = rc.Width - rcPanel.Width;
            y = rc.Height - rcPanel.Height;
            x /= 2;
            y /= 2;

            panel1.Location = new Point(x, y);

        }

        private void frmMonthlyCompare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();

        }

        private void frmMonthlyCompare_Move(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.Print(this.Location.ToString());
        }
    }
}
