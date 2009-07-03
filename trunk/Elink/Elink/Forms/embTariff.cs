using System;
using System.Windows.Forms;

namespace Elink
{
    public partial class embTariff : UserControl
    {
        TariffData data;
        //double defaultPrice;
        public embTariff(TariffData d, string currency)
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            
            label3.Text = currency + "/kWh";
            data = d;
            //defaultPrice = price;
        }
        const string TITLE = "No.";
        int index = 0;
        public int Index { get { return index; } set { if (index != value) { index = value; } } }
        public void LoadPage()
        {
            groupBox1.Text = TITLE + (index+1).ToString();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            //textBox1.Text = defaultPrice.ToString();
            //cbPrice.DataSource = Settings.I.advTariff.price_alias;

            checkBox1.Checked = data.IsByTime;
            checkBox2.Checked = data.IsByWeek;
            checkBox3.Checked = data.IsByDate;
            try
            {
                textBox1.Text = data.Price.ToString();
                if( data.IsByTime )
                {
                    time1.Text = data.TimeStart.ToString("HH:mm");
                    time2.Text = data.TimeEnd.ToString("HH:mm");
                }
                if( data.IsByWeek )
                {
                    comboBox1.SelectedIndex = (int)data.WeekStart;
                    comboBox1.SelectedIndex = (int)data.WeekEnd;
                }
                if( data.IsByDate)
                {
                    dateTimePicker1.Value = data.DateStart;
                    dateTimePicker2.Value = data.DateEnd;
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            time1.Enabled =
                time2.Enabled = checkBox1.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = comboBox2.Enabled = checkBox2.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = dateTimePicker2.Enabled = checkBox3.Checked;
        }
        bool isValid = true;
        public bool IsByTime { get { return checkBox1.Checked; } }
        public bool IsByWeek { get { return checkBox2.Checked; } }
        public bool IsByDate { get { return checkBox3.Checked; } }
        public bool IsValid { get { return isValid; } }
        public TariffData SaveData()
        {
            data = new TariffData();
            isValid = IsByTime | IsByWeek | IsByDate;
            if (!isValid)
                return null;
            try
            {
                data.Price = cbPrice.SelectedIndex;//double.Parse(textBox1.Text);
                // Time
                if (IsByTime)
                {
                    data.IsByTime = true;
                    data.TimeStart = DateTime.Parse(time1.Text);
                    data.TimeEnd = DateTime.Parse(time2.Text);
                }
                // Week days
                if (IsByWeek)
                {
                    data.IsByWeek = true;
                    data.WeekStart = (DayOfWeek)comboBox1.SelectedIndex;
                    data.WeekEnd = (DayOfWeek)comboBox2.SelectedIndex;
                }
                // Date
                if (IsByDate)
                {
                    data.IsByDate = true;
                    data.DateStart = dateTimePicker1.Value;
                    data.DateEnd = dateTimePicker2.Value;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            return data;
        }
        public void OnPriceAlias(object sender, EventArgs e)
        {
            PriceAliasEvent f = (PriceAliasEvent)e;
            cbPrice.Items.Clear();
            cbPrice.Items.AddRange(f.alias);
            //if (Settings.I.advTariff.tariffs == 2)
            //    cbPrice.Items.RemoveAt(2);
            cbPrice.SelectedIndex = data.Price;
        }
    }

    public class TariffData
    {
        //static double normalPrice = 0;
        //public static double NormalPrice { get { return normalPrice; } set { normalPrice = value; } }

        int price = 0;

        bool byTime = false;
        bool byWeek = false;
        bool byDate = false;
        //public double Price { get { return price; } set { price = value; } }
        public int Price { get { return price; } set { price = value; } }
        public bool IsByTime { get { return byTime; } set { byTime = value; } }
        public bool IsByWeek { get { return byWeek; } set { byWeek = value; } }
        public bool IsByDate { get { return byDate; } set { byDate = value; } }
        DateTime tmStart = new DateTime();
        DateTime tmEnd = new DateTime();
        DateTime dtStart = new DateTime();
        DateTime dtEnd = new DateTime();
        DayOfWeek wkStart = DayOfWeek.Monday;
        DayOfWeek wkEnd = DayOfWeek.Saturday;
        public DateTime TimeStart { get { return tmStart; } set { tmStart = value; } }
        public DateTime TimeEnd { get { return tmEnd; } set { tmEnd = value; } }
        public DateTime DateStart { get { return dtStart; } set { dtStart = value; } }
        public DateTime DateEnd { get { return dtEnd; } set { dtEnd = value; } }
        public DayOfWeek WeekStart { get { return wkStart; } set { wkStart = value; } }
        public DayOfWeek WeekEnd { get { return wkEnd; } set { wkEnd = value; } }

        public TariffData()
        {
            //price = NormalPrice;
        }
        public bool IsMatch(DateTime hour, out int price_idx)
        {
            price_idx = 0;
            bool yes = true;

            if( byTime )
            {
                DateTime timeFrom = new DateTime(hour.Year, hour.Month, hour.Day,
                    tmStart.Hour, tmStart.Minute, tmStart.Second);
                DateTime timeTo = new DateTime(hour.Year, hour.Month, hour.Day,
                    tmEnd.Hour, tmEnd.Minute, tmEnd.Second);
                if (timeFrom > timeTo)
                    timeFrom -= new TimeSpan(1, 0, 0, 0);

                if (hour >= timeFrom && hour <= timeTo)
                    yes = true;
                return false;
            }
            if( byDate )
            {
                DateTime dateFrom = new DateTime(hour.Year, tmStart.Month, tmStart.Day);
                DateTime dateTo = new DateTime(hour.Year, tmEnd.Month, tmEnd.Day);
                if (hour >= dateFrom && hour <= dateTo)
                    yes = true;
                else
                    return false;
            }

            if( byWeek )
            {
                DayOfWeek week1 = wkStart;
                DayOfWeek week2 = wkEnd;
                if (hour.DayOfWeek >= week1 && hour.DayOfWeek <= week2)
                    yes = true;
                else
                    return false;
            }

            price_idx = price;
            return yes;
        }

    }
}
