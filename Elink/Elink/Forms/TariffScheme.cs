using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Elink
{
    public partial class TariffScheme : UserControl, ITariff, ITariffEmb
    {
        public TariffScheme()
        {
            InitializeComponent();
        }

        public Color AllbackColor
        {
            set
            {
//                 lb211.BackColor = lb212.BackColor = lb213.BackColor = value;
//                 rnd211.BackBackColor = rnd211.RightBackBackColor = value;
            }
        }
        public bool IsFocused
        {
            get
            {
                return false;
            }
        }
        public void SetCapture(EventHandler enter, EventHandler leave)
        {
            foreach (Control c in Controls)
            {
                c.MouseEnter += enter;
                c.MouseLeave += leave;
            }
            this.MouseEnter += enter;
            this.MouseLeave += leave;
        }

        public double Price
        {
            get
            {
                /*return double.Parse(tbPayForElec.Text);*/
                return 0;
            }
            set
            {
                /*tbPayForElec.Text = value.ToString();*/
            }
        }
        private void OnPriceChange(object sender, EventArgs e)
        {
            //lb213.Text = Settings.I.currency + "/kWh";
            lbUnit1.Text = lbUnit2.Text = lbUnit3.Text = lbUnit4.Text = Settings.I.currency + "/kWh";
        }

        private void TariffScheme_Load(object sender, EventArgs e)
        {
            SubRightSettings.Instance.OnPriceChange += OnPriceChange;
            this.BackColor = Color.Transparent;
            OnPriceChange(null, null);

            LoadPage();
        }

        private void SingleTariff(bool e)
        {
            tbSingle.Enabled = e;
            //cbSingle.CheckState = e?CheckState.Checked:CheckState.Unchecked;

            //cbDual.CheckState = e?CheckState.Unchecked:CheckState.Checked;
            tbPeak.Enabled = tbOffPeak.Enabled =
                tbPricePeak.Enabled = tbPriceOffPeak.Enabled =
                tbPeakFrom.Enabled = tbPeakTo.Enabled =
                tbOffFrom.Enabled = tbOffTo.Enabled =
                !e;
        }

        private void cbSingle_CheckedChanged(object sender, EventArgs e)
        {
            SingleTariff(cbSingle.Checked);
            cbDual.Checked = !cbSingle.Checked;
        }

        private void cbDual_CheckedChanged(object sender, EventArgs e)
        {
            SingleTariff(!cbDual.Checked);
            cbSingle.Checked = !cbDual.Checked;
        }
        public string TariffName { get { return tf.tariff_name; } set { tf.tariff_name = value; } }
        public DateTime UpdateTime { get { return tf.last_update; } set { tf.last_update = value; } }
        Tariff tf= new Tariff();
        public void SetData(Tariff t)
        {
            tf = t.Clone() as Tariff;
        }
        public void LoadPage()
        {
            SingleTariff(tf.tariffs==1);
            cbSingle.Checked = (tf.tariffs==1);
            cbDual.Checked = !cbSingle.Checked;

            tbSingle.Text = tf.energy_rates_p1.ToString();

            tbPeak.Text = tf.price_alias[0];
            tbOffPeak.Text = tf.price_alias[1];

            tbPricePeak.Text = tf.price_values[0].ToString();
            tbPriceOffPeak.Text = tf.price_values[1].ToString();

            tbPeakFrom.Text = tf.p1_time1;
            tbPeakTo.Text = tf.p1_time2;
            tbOffFrom.Text = tf.p3_time1;
            tbOffTo.Text = tf.p3_time2;

            tbMonthly.Text = tf.meter_rent.ToString();
        }
        public Tariff SaveData()
        {
            tf.tariffs = (cbSingle.Checked) ? 1 : 2;
            float.TryParse(tbSingle.Text, out tf.energy_rates_p1);
            tf.price_alias[0] = tbPeak.Text;
            tf.price_alias[1] = tbOffPeak.Text;

            float.TryParse(tbPricePeak.Text, out tf.price_values[0]);
            float.TryParse(tbPriceOffPeak.Text, out tf.price_values[1]);

            tf.p1_time1 = tbPeakFrom.Text;
            tf.p1_time2 = tbPeakTo.Text;
            tf.p3_time1 = tbOffFrom.Text;
            tf.p3_time2 = tbOffTo.Text;

            float.TryParse(tbMonthly.Text, out tf.meter_rent);

            tf.VAT = 0;
            tf.elec_tax = 0;
            tf.power_fee = 0;
            tf.over_limit = 999999;
            tf.over_charge = 0;

            return tf;
        }
        public void SaveToXML(string xml)
        {
            tf = SaveData();
            if (tf != null)
                tf.SaveToXML(xml);
        }
        public void LoadFromXML(string xml)
        {
            tf = Tariff.LoadTariffXML(xml);
            LoadPage();
        }

    }
}
