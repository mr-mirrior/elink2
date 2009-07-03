using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class TariffAdvanced : UserControl, ITariff, ITariffEmb
    {
        public TariffAdvanced()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            cb23.SelectedIndex = 0;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }
        private void OnPriceChange(object sender, EventArgs e)
        {
            label4.Text = Settings.I.currency + "/kW/" + Global.GetString("s1005","Month");
            label7.Text = Settings.I.currency + "/kWh";
            label12.Text = Settings.I.currency + "/" +Global.GetString("s1005", "Month");
            label21.Text = Settings.I.currency + "/kWh";
        }
        public Color AllbackColor 
        {
            set
            {
                this.BackColor = value;
                //lb231.BackColor = value;
                //rnd231.BackBackColor = rnd231.RightBackBackColor = value;
                //btnDetails.BackBackColor = btnDetails.RightBackBackColor = value;
            }
        }
        public bool IsFocused
        {
            get
            {
                return cb23.Focused || this.Focused || btnDetails.Focused ||
                    tbMaxPower.Focused ||
                    tbPowerFee.Focused ||
                    tbP1.Focused ||
                    tbMeterRent.Focused ||
                    tbElecTax.Focused ||
                    tbVAT.Focused ||
                    tbLimit.Focused ||
                    tbCharge.Focused;
            }
        }
        public void SetCapture(EventHandler enter, EventHandler leave)
        {
            this.MouseEnter -= enter;
            this.MouseLeave -= leave;
            this.MouseEnter += enter;
            this.MouseLeave += leave;
            foreach (Control c in Controls)
            {
                c.MouseEnter -= enter;
                c.MouseLeave -= leave;
                c.MouseEnter += enter;
                c.MouseLeave += leave;
                if (c.GetType() == typeof(Panel))
                {
                    Panel p = (Panel)c;
                    foreach (Control cc in p.Controls)
                    {
                        cc.MouseEnter -= enter;
                        cc.MouseLeave -= leave;
                        cc.MouseEnter += enter;
                        cc.MouseLeave += leave;
                    }
                }
            }
        }

        private void cb23_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (adv_tariff == null)
                return;

            if (cb23.Text.Length == 0)
                return;
            int tariffs = int.Parse(cb23.Text);
            if (tariffs == 3)
            {
                adv_tariff.simple = false;
            }
            else
                adv_tariff.simple = true;

            if (tariffs == 1)
            {
                plP1.Visible = true;
                plMulti.Visible = false;
                adv_tariff.tariffs = 1;
                adv_tariff.over_charge = old_overcharge;
                LoadPage();
            }
            else 
            {
                plP1.Visible = false;
                plMulti.Visible = true;
                adv_tariff.tariffs = tariffs;
                adv_tariff.over_charge = 0;
                this.BeginInvoke(new InvokeDelegate(delegate() { btnDetails_Click(this, null); }));
            }

            //LoadPage();
//             if(tariffs > adv_tariff.tariffs)
//             {
//             }
        }
        public int Count { set { if (value < 0) return; cb23.SelectedIndex = value; } }
        //List<TariffData> adv = new List<TariffData>();
        //public List<TariffData> Data { get { return adv; } set { adv = value; } }

        Tariff adv_tariff = null;
        Tariff old_tariff = null;
        float old_overcharge = 0;
        public void SetData(Tariff tg)
        {
            TariffName = tg.tariff_name;
            adv_tariff = (Tariff)tg.Clone();
            old_tariff = (Tariff)tg.Clone();
            if (tg.over_charge != 0)
                old_overcharge = tg.over_charge;
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            if (adv_tariff == null)
                return;

            frmAdvTariff dlg = new frmAdvTariff(adv_tariff/*, SubRightSettings.Instance.Price*/);
            //dlg.Count = Int32.Parse(cb23.SelectedItem as string);
            //adv_tariff.over_charge = 0;
            LoadPage();
            if (DialogResult.OK != dlg.ShowDialog())
            {
                if (old_tariff != null)
                    adv_tariff = old_tariff;
                LoadPage();
                return;
            }
            Tariff t = dlg.SaveData();
            //t.over_charge = 0;
            Settings.I.single_tariff = false;

            if (t == null)
            {
                MB.Warning(
                    "Invalid tariff settings found, discarded from saving."+
                    "Please try again");
                return;
            }
            adv_tariff = t;

            if (adv_tariff.tariffs != 1)
                adv_tariff.over_charge = 0;

            LoadPage();

            SubRightSettings.Instance.DetailOK();
        }

        private void TariffAdvanced_Load(object sender, EventArgs e)
        {
            SubRightSettings.Instance.OnPriceChange += OnPriceChange;
            OnPriceChange(null, null);
            //RefreshCount();
            LoadPage();

        }
        public void LoadPage()
        {
            if (adv_tariff == null)
                return;
            //if (adv_tariff.tariff_name == null)
            //    return;

            tbMaxPower.Text = adv_tariff.max_power.ToString();
            tbPowerFee.Text = adv_tariff.power_fee.ToString();

            tbMeterRent.Text = adv_tariff.meter_rent.ToString();
            tbElecTax.Text = adv_tariff.elec_tax.ToString();
            tbVAT.Text = adv_tariff.VAT.ToString();

            tbLimit.Text = adv_tariff.over_limit.ToString();

            cb23.Text = adv_tariff.tariffs.ToString();
                //adv_tariff.simple ? adv_tariff.simple_periods.ToString() :
                //adv_tariff.adv.Count.ToString();

            tbP1.Text = adv_tariff.energy_rates_p1.ToString();

            if (adv_tariff.tariffs == 1)
            {
                plMulti.Visible = false;
                plP1.Visible = true;
                //adv_tariff.over_charge = 0.027403f;
                //if (tbCharge.Text.Equals("0"))
                //    adv_tariff.over_charge = Tariff.OVERCHARGEDEFAULT;
            }
            else
            {
                //adv_tariff.over_charge = 0;
                cbMulti.Items.Clear();
                for (int i = 0; i < adv_tariff.tariffs; i++ )
                {
                    //adv_tariff.InitPrices();
                    cbMulti.Items.Add(string.Format("\"{0}\": {1:0.00}{2}", 
                        adv_tariff.price_alias[i],
                        adv_tariff.price_values[i],
                        adv_tariff.price_currency));
                }
                cbMulti.SelectedIndex = 0;
                plP1.Visible = false;
                plMulti.Visible = true;
            }
            tbCharge.Text = adv_tariff.over_charge.ToString();

        }
        public Tariff SaveData()
        {
            if (adv_tariff == null)
                adv_tariff = new Tariff();

            //tbMaxPower.Text = adv_tariff.max_power.ToString("0.00");
            adv_tariff.max_power = float.Parse(tbMaxPower.Text);
            //tbPowerFee.Text = adv_tariff.power_fee.ToString("0.00");
            adv_tariff.power_fee = float.Parse(tbPowerFee.Text);

            //tbMeterRent.Text = adv_tariff.meter_rent.ToString("0.00");
            adv_tariff.meter_rent = float.Parse(tbMeterRent.Text);
            //tbElecTax.Text = adv_tariff.elec_tax.ToString("0.00");
            adv_tariff.elec_tax = float.Parse(tbElecTax.Text);
            //tbVAT.Text = adv_tariff.VAT.ToString("0.00");
            adv_tariff.VAT = float.Parse(tbVAT.Text);

            //tbLimit.Text = adv_tariff.over_limit.ToString("0.00");
            adv_tariff.over_limit = float.Parse(tbLimit.Text);
            //tbCharge.Text = adv_tariff.over_charge.ToString("0.00");
            adv_tariff.over_charge = float.Parse(tbCharge.Text);

            //cb23.Text = adv_tariff.tariffs.ToString();
            //adv_tariff.tariffs = int.Parse(cb23.Text);

            //tbP1.Text = adv_tariff.energy_rates_p1.ToString("0.00");
            if( adv_tariff.tariffs == 1 )
                adv_tariff.energy_rates_p1 = float.Parse(tbP1.Text);
            else
            {
                //adv_tariff.adv = 
//                 comboBox1.Items.Clear();
//                 for (int i = 0; i < adv_tariff.tariffs; i++)
//                 {
//                     comboBox1.Items.Add(string.Format("\"{0}\": {1:0.00}{2}",
//                         adv_tariff.price_alias[i],
//                         adv_tariff.price_values[i],
//                         adv_tariff.price_currency));
//                 }
//                 comboBox1.SelectedIndex = 0;
//                 plP1.Visible = false;
//                 plMulti.Visible = true;
            }
            adv_tariff.tariff_name = TariffName;
            return adv_tariff;
        }

        private void comboBox1_MouseEnter(object sender, EventArgs e)
        {
        }

        private void comboBox1_MouseLeave(object sender, EventArgs e)
        {

        }
        public string TariffName
        {
            get { if (adv_tariff != null) return adv_tariff.tariff_name; return null; }
            set { if (adv_tariff != null) adv_tariff.tariff_name = value; }
        }
        public DateTime UpdateTime
        {
            get { if (adv_tariff != null) return adv_tariff.last_update; return DateTime.MinValue; }
            set { if (adv_tariff != null) adv_tariff.last_update = value; }
        }
        public void SaveToXML(string xml)
        {
            adv_tariff = SaveData();
            if (adv_tariff != null)
                adv_tariff.SaveToXML(xml);
        }
        public void LoadFromXML(string xml)
        {
            adv_tariff = Tariff.LoadTariffXML(xml);
            LoadPage();
        }

        private void TariffAdvanced_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.S)
            {
                adv_tariff.SaveToXML("current.txt");
                Tariff t = Tariff.LoadTariffXML("current.txt");
            }
        }
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        private void roundFrame9_Click(object sender, EventArgs e)
        {
            if (!MB.OKCancelQ(
    GetString("s6000", "Are you sure to reset all the values to default?")))
                return;

            adv_tariff = new Tariff();
            adv_tariff.max_power = 0;
            adv_tariff.power_fee = 0;
            adv_tariff.meter_rent = 0;
            adv_tariff.elec_tax = 0;
            adv_tariff.VAT = 0;
            adv_tariff.over_limit = 0;
            adv_tariff.over_charge = 0;

            LoadPage();

        }

        private void TariffAdvanced_VisibleChanged(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void TariffAdvanced_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
//             TextBox[] check = new TextBox[] {tbMaxPower,tbPowerFee, 
//                 tbP1, tbMeterRent,tbElecTax,tbVAT,tbLimit, tbCharge };
//             for (int i = 0; i < check.Length; i++ )
//             {
// 
//             }
        }

    }
}
