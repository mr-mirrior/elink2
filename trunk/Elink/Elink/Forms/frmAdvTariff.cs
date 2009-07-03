using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Elink
{
    public partial class frmAdvTariff : Form
    {
        Tariff adv_tariff;
        List<TariffData> data;
        public frmAdvTariff(Tariff _adv)
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            //label3.Text = SubRightSettings.Instance.Currency + "/kWh";
            adv_tariff = (Tariff)_adv.Clone();
            data = adv_tariff.adv;
            //TariffData.NormalPrice = price;

            OnPriceAliasChange += simple.OnPriceAlias;

            //Text += " for " + adv_tariff.tariff_name;
            lbTariffName.Text = adv_tariff.tariff_name;
        }
        embSimplePeriods simple = new embSimplePeriods();
        //int cntTariff = 1;
        //public int Count { get { return cntTariff; } set { cntTariff = value; LoadPage(); } }
        private void UpdateAdvPeriods()
        {
            int idx = data.Count;//Math.Max(adv.Count, 1);
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < idx; i++)
            {
                embTariff e = new embTariff(data[i], adv_tariff.price_currency);
                e.Index = i;
                e.LoadPage();
                flowLayoutPanel1.Controls.Add(e);
                OnPriceAliasChange += e.OnPriceAlias;
            }
            flowLayoutPanel1.ResumeLayout();
            
            //OnPriceAliasChange.Invoke(this, null);
            InformPriceAlias();
        }
        private void InformPriceAlias()
        {
            string[] x = new string[adv_tariff.tariffs];
            for (int i = 0; i < adv_tariff.tariffs; i++ )
            {
                x[i] = adv_tariff.price_alias[i];
            }
            OnPriceAliasChange.Invoke(this, new PriceAliasEvent(x));
        }
        public void LoadPage()
        {
            UpdateAdvPeriods();

            lbTariff1.Text = lbTariff2.Text = lbTariff3.Text =
                adv_tariff.price_currency + "/kWh";
            string[] s = adv_tariff.price_alias/*.ToArray()*/;
            if (s.Length >= 2)
            {
                tbTariff1.Text = s[0];
                tbTariff2.Text = s[1];
                tbTariff3.Text = s[2];
                cbTariffs.SelectedIndex = adv_tariff.tariffs == 2 ? 0 : 1;

                tbPrice1.Text = adv_tariff.price_values[0].ToString();
                tbPrice2.Text = adv_tariff.price_values[1].ToString();
                tbPrice3.Text = adv_tariff.price_values[2].ToString();
            }
            if (adv_tariff.tariffs > 2)
            {
            }
            
            if( adv_tariff.simple )
            {
                cbPeriods.SelectedItem = adv_tariff.simple_periods.ToString();
                if (adv_tariff.simple_periods == 2)
                {
                    simple.Go2();
                }
                else
                {
                    simple.Go4();
                }

                simple.Price1 = adv_tariff.p1_price;
                //simple.Price2 = adv_tariff.p2_price;
                simple.Price3 = adv_tariff.p3_price;
                simple.P1Time1 = adv_tariff.p1_time1;
                simple.P1Time2 = adv_tariff.p1_time2;
                simple.P1Time3 = adv_tariff.p1_time3;
                simple.P1Time4 = adv_tariff.p1_time4;
                simple.P3Time1 = adv_tariff.p3_time1;
                simple.P3Time2 = adv_tariff.p3_time2;
                simple.P3Time3 = adv_tariff.p3_time3;
                simple.P3Time4 = adv_tariff.p3_time4;

                // has to be 2 if simple mode
                cbTariffs.SelectedIndex = 0;
            }
            else
            {
                GoMore();
            }
//             string s1 = "";
//             string s2 = "";
//             if (DT.IsStrictlyValid(adv_tariff.last_update)
//                 s1 = adv_tariff.valid_since.ToString("g");
//             //if (adv_tariff.valid_until != DateTime.MaxValue)
//             //    s2 = adv_tariff.valid_until.ToString("g");
//             tbValidSince.Text = s1;
//             tbValidUntil.Text = s2;
            //cbTariffs.SelectedIndex = idx==0?0:idx-1;
            //textBox1.Text = TariffData.NormalPrice.ToString();
        }
        private void SetTariffCount(int count)
        {
            if (data.Count == count)
                return;
            if (count < 0)
                return;
            if( data.Count > count )
            {
                while(data.Count != count )
                {
                    data.Remove(data[data.Count-1]);
                }
                //LoadPage();
                UpdateAdvPeriods();
                return;
            }
            while(data.Count != count)
            {
                data.Add(new TariffData());
            }
            UpdateAdvPeriods();
        }
        private void tbTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Count = Int32.Parse(comboBox1.SelectedItem as string);
            //SetTariffCount(cbTariffs.SelectedIndex + 1);
            UpdateTariffs();
        }
        public Tariff SaveData()
        {
//             if (tbValidSince.Text.Length == 0)
//                 adv_tariff.valid_since = DateTime.MinValue;
//             else
//             if( !DateTime.TryParse(tbValidSince.Text, out adv_tariff.valid_since) )
//             {
//                 return null;
//             }

//             if (tbValidUntil.Text.Length == 0)
//                 adv_tariff.valid_until = DateTime.MaxValue;
//             else
//             if( !DateTime.TryParse(tbValidUntil.Text, out adv_tariff.valid_until) )
//             {
//                 return null;
//             }
//             adv_tariff.price_alias = new List<string>();
            adv_tariff.price_alias = new string[]{ tbTariff1.Text, tbTariff2.Text, tbTariff3.Text };
            adv_tariff.tariffs = (this.cbTariffs.SelectedIndex == 0) ? 2 : 3;
            try
            {
                //adv_tariff.price_values.Clear();
                adv_tariff.price_values[0] = (float.Parse(tbPrice1.Text));
                adv_tariff.price_values[1] = (float.Parse(tbPrice2.Text));
                adv_tariff.price_values[2] = (float.Parse(tbPrice3.Text));
            }
            catch (System.Exception)
            {

            }

            //adv_tariff.simple = plSimple.Visible;
            adv_tariff.simple_periods = (cbPeriods.SelectedIndex == 0) ? 2 : 4;
            adv_tariff.p1_price = simple.Price1;
            adv_tariff.p2_price = simple.Price2;
            adv_tariff.p3_price = simple.Price3;
            adv_tariff.p1_time1 = simple.P1Time1;
            adv_tariff.p1_time2 = simple.P1Time2;
            adv_tariff.p1_time3 = simple.P1Time3;
            adv_tariff.p1_time4 = simple.P1Time4;
            if (adv_tariff.simple_periods==4)
            {
                 adv_tariff.p3_time1 = simple.P3Time1;
                 adv_tariff.p3_time2 = simple.P3Time2;
                 adv_tariff.p3_time3 = simple.P3Time3;
                 adv_tariff.p3_time4 = simple.P3Time4;
            }


            List<TariffData> lst = new List<TariffData>();
            //for(int theOne=0; theOne<flowLayoutPanel1.Controls.Count; theOne++ )
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                embTariff adv = c as embTariff;
                TariffData data = adv.SaveData();
                if (data != null)
                    lst.Add(data);
            }
            adv_tariff.adv = lst;

            return adv_tariff;
        }

        // OK button
        private void button1_Click(object sender, EventArgs e)
        {
            adv_tariff.simple = plSimple.Visible;
        }

        private void frmAdvTariff_Load(object sender, EventArgs e)
        {
            plAdv.Dock = DockStyle.Fill;

            plSimple.Dock = DockStyle.Fill;
            plSimple.Controls.Add(simple);
            simple.Visible = true;
            cbPeriods.SelectedIndex = 0;
            //panel4.Dock = DockStyle.Fill;

            LoadPage();
        }
        private void GoMore()
        {
            plAdv.Visible = true;
            plSimple.Visible = false;

            btnAdvanced.Visible = false;
            btnRet.Visible = true;

            //cbTariffs.Enabled = true;

            UpdateCBPeriods();
        }
        private void btnMore_Click(object sender, EventArgs e)
        {
            GoMore();
        }
        private void btnRet_Click(object sender, EventArgs e)
        {
            btnAdvanced.Visible = true;
            btnRet.Visible = false;
            cbTariffs.Enabled = false;

            plAdv.Visible = false;
            plSimple.Visible = true;
            UpdateCBPeriods();
        }
        private void OnCurrencyChange(object sender, EventArgs e)
        {
        //    PriceEvent f = e as PriceEvent;
        //    label3.Text = f.Currency + "/kWh";
        }
        private void UpdateTariffs()
        {
            List<string> lst = new List<string>();
            lst.Add(tbTariff1.Text);
            lst.Add(tbTariff2.Text);
            lst.Add(tbTariff3.Text);
            bool show3 = false;
            if( cbTariffs.SelectedIndex == 0 ) // 2
            {
                adv_tariff.tariffs = 2;
            }
            else // 3
            {
                adv_tariff.tariffs = 3;
                show3 = true;
            }
            tbTariff3.Visible = tbPrice3.Visible = lbTariff3.Visible = show3;
            adv_tariff.price_alias = lst.ToArray();
            InformPriceAlias();
        }
        private void UpdateSimple()
        {
            if (cbPeriods.SelectedIndex == 0)
                simple.Go2();
            else
                simple.Go4();
        }
        private int AdvPeriods()
        {
            if (cbPeriods.Text.Length == 0)
                return 0;
            return int.Parse(cbPeriods.Text);
        }
        private void UpdateAdv()
        {
            SetTariffCount(AdvPeriods());
        }
        private void cbPeriods_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsSimpleNow)
                UpdateSimple();
            else // advanced
            {
                UpdateAdv();
            }
        }
        private bool IsSimpleNow { get { return btnAdvanced.Visible; } }

        private void UpdateCBPeriods()
        {
            if( IsSimpleNow )
            {
                cbPeriods.Items.Clear();
                cbPeriods.Items.AddRange(new string[] { "2", "4" });
                cbPeriods.SelectedIndex = 0;
                UpdateSimple();

                cbTariffs.SelectedIndex = 0;
                UpdateTariffs();
            }
            else // advanced
            {
                cbPeriods.Items.Clear();
                cbPeriods.Items.AddRange(new string[] { "2", "3", "4", "5", "6" });
                cbPeriods.SelectedIndex = Math.Max(0, data.Count - 2);
                UpdateAdv();
            }
        }

        private void Tariff_Click(object sender, EventArgs e)
        {
            TextBox c = sender as TextBox;
            c.ReadOnly = false;
        }

        private void Tariff_Leave(object sender, EventArgs e)
        {
    
            TextBox c = sender as TextBox;
            c.ReadOnly = true;
            UpdateTariffs();
        }

        public event EventHandler OnPriceAliasChange;

        private void tbValidUntil_MouseClick(object sender, MouseEventArgs e)
        {
//             // CTRL+左键
//             if( e.Button == MouseButtons.Left && 
//                 ModifierKeys == Keys.Control )
//             {
//                 tbValidUntil.ReadOnly = false;
//             }
        }
    }






    public class PriceAliasEvent:EventArgs
    {
        public string[] alias;
        public PriceAliasEvent(string[] als)
        {
            alias = als;
        }
    }

}
