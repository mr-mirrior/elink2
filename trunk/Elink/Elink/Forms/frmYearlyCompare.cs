using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class frmYearlyCompare : Form
    {
        EnergyData real = null;
        EnergyData simulation = null;
        RoundRectControl[][] cylinders;
        Color clRecord = Color.DimGray;
        Color clSim = eColor.MyBlue;
        Color clNA = Color.LightGray;
        public frmYearlyCompare(EnergyData r, EnergyData s)
        {
            real = r;
            simulation = s;
            Global.GoMultiLanguage();
            InitializeComponent();

            cylinders = new RoundRectControl[2][];
            cylinders[0] = new RoundRectControl[12] {
                jan, feb, mar, apr, may, jun,
                jul, aug, sep, oct, nov, dec
            };
            cylinders[1] = new RoundRectControl[12]{
                jan1, feb1, mar1, apr1, may1, jun1,
                jul1, aug1, sep1, oct1, nov1, dec1
            };
            //this.TopMost = true;
        }
        float maxvalue = 0.5f;
        private void SetMax(float f)
        {
            if (f == 0.0f)
                return;
            maxvalue = f;
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
        private void SetCylinder(int c, int idx, float v)
        {
            RoundRectControl r = cylinders[c][idx];

            Color cl = clRecord;
            if (c == 1)
                cl = clSim;
            if (!Is.ValidF(v))
            {
                cl = clNA;
                v = 0;
                toolTip1.SetToolTip(r, "N/A");
            }
            r.BackColor = cl;

            Rectangle rcb = plBounds.ClientRectangle;
            float h = (maxvalue - v) / maxvalue; // 0
            h = 1 - h;    // 1
            h *= rcb.Height;

            r.Height = (int)h;
            r.Top = dashLabel1.Top - (int)h;
            if (r.Bottom > dashLabel1.Top)
                r.Top = dashLabel1.Top - r.Height;
        }
        private void UpdateCylinders(List<float> r, List<float> s, float real, float sim, string currency)
        {
            if( r.Count != 12 || s.Count != 12 )
            {
                MB.Error("Weird! less than 12 months?");
                return;
            }
            if (!Is.ValidF(real) || !Is.ValidF(sim))
                return;

            float max = Math.Max(real, sim);
            float w1 = real / max; // 0
            float w2 = sim / max; // 0.3
            w1 *= lbReal.Left - lbRealName.Right;
            w2 *= lbReal.Left - lbRealName.Right;

            realprice.Width = (int)w1;
            //realprice.Left = lbReal.Left - (int)w1 + ((real == 0) ? realprice.Width : 0);
            simprice.Width = (int)w2;
            //simprice.Left = lbReal.Left - (int)w2 + ((sim == 0) ? simprice.Width : 0);


            for (int i = 0; i < 12; i++ )
            {
                float v1 = r[i];
                float v2 = s[i];
                if (Is.ValidF(v1))
                {
                    //cylinders[0][i].BackColor = eColor.MyBlue;
                    toolTip1.SetToolTip(cylinders[0][i], currency+v1.ToString());
                }
                else
                {
                    //cylinders[0][i].BackColor = eColor.NA;
                    //v1 = 0;
                    toolTip1.SetToolTip(cylinders[0][i], "N/A");
                }

                if (Is.ValidF(s[i]))
                {
                    //cylinders[1][i].BackColor = Color.OrangeRed;
                    toolTip1.SetToolTip(cylinders[1][i], currency+v2.ToString());
                }
                else
                {
                    //cylinders[1][i].BackColor = eColor.NA;
                    //v2 = 0;
                    toolTip1.SetToolTip(cylinders[1][i], "N/A");
                }
                SetCylinder(0, i, v1);
                SetCylinder(1, i, v2);
            }
        }
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        private void LoadPage()
        {
            if (real == null || simulation == null)
                return;

            int year = real.valid_since.E2Time.Year;
            float total_real = 0;
            float total_sim = 0;
            float max_real = 0;
            float max_sim = 0;
            List<float> monthly_real = real.MonthlyCost(year, out total_real, out max_real);
            List<float> monthly_simulation = simulation.MonthlyCost(year, out total_sim, out max_sim);
            if (monthly_real == null || monthly_simulation == null)
                return;
            string currency = Settings.I.currency;
            lbRealName.Text = real.TariffName();
            lbSimName.Text = simulation.TariffName();
            lbYear.Text = year.ToString();

            lbReal.Text = currency+total_real.ToString("0.00");
            lbSim.Text = currency+total_sim.ToString("0.00");
            lbCurrency.Text = currency;

            float diff = total_sim - total_real;
            float ratio = 0;
            //if (diff == 0.0f)
            //    return;
            if (total_real != 0.0)
            {
                if (diff < 0)
                    ratio = diff * 100 / total_sim;
                else
                    ratio = diff * 100 / total_real;
            }
            if (diff < 0)
            {
                // good
                pic.Image = Properties.Resources.good_smile;
                lbDifference.ForeColor = lbRatio.ForeColor = eColor.MyBlue;
                //difference = -difference;
                ratio = -ratio;
                lbDifference.Text = diff.ToString("0.00") + currency;
                lbRatio.Text = ratio.ToString("0.0") + GetString("s3001", "% Cheaper");
                lbConclusion.Text = GetString("s3004", "Good");
                lbConclusion.ForeColor = eColor.MyBlue;
            }
            else
                if( diff > 0 )
                {
                    // bad
                    pic.Image = Properties.Resources.bad_cry;
                    lbDifference.ForeColor = lbRatio.ForeColor = Color.OrangeRed;
                    lbDifference.Text = diff.ToString("+0.00") + currency;
                    lbRatio.Text = ratio.ToString("0.0") + GetString("s3002", "% More expensive");
                    lbConclusion.Text = GetString("s3005", "Bad");
                    lbConclusion.ForeColor = Color.OrangeRed;
                }
                else
                {
                    // equals
                    pic.Image = Properties.Resources.just_soso;
                    lbDifference.ForeColor = lbRatio.ForeColor = Color.Silver;
                    lbDifference.Text = diff.ToString("0.00") + currency;
                    lbRatio.Text = ratio.ToString("0.0" + "%");
                    lbConclusion.Text = GetString("s3003", "Similar");
                    lbConclusion.ForeColor = Color.DimGray;
                }
            float mx = Math.Max(max_real, max_sim);
            if( Is.ValidF(mx) )
                SetMax(mx);
            UpdateCylinders(monthly_real, monthly_simulation, total_real, total_sim, currency);
        }

        private void InitPage()
        {
            for (int i = 0; i < 12; i++ )
            {
                SetCylinder(0, i, float.NaN);
                SetCylinder(1, i, float.NaN);
            }
        }
        private void frmYearlyCompare_Load(object sender, EventArgs e)
        {
            InitPage();
            LoadPage();
        }

        private void frmYearlyCompare_Resize(object sender, EventArgs e)
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

        private void frmYearlyCompare_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
