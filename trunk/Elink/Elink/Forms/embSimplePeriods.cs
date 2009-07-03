using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Elink
{
    public partial class embSimplePeriods : UserControl
    {
        public embSimplePeriods()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
        }

        private void UpdatePrices(string[] alias)
        {
            //if (Settings.I.advTariff.price_alias.Length == 0)
            //    return;
            if (alias.Length == 0)
                return;

            ComboBox[] cbs = new ComboBox[] { cb1,cb2,cb3};
            foreach (ComboBox cb in cbs)
            {
                cb.Items.Clear();
                cb.Items.AddRange(alias);
                cb.SelectedIndex = 0;
            }
            cb1_SelectionChangeCommitted(null, null);
        }
        private void embSimplePeriods_VisibleChanged(object sender, EventArgs e)
        {
//             if (this.Visible)
//             {
//                 UpdatePrices();
//             }
        }
        public void Go2()
        {
            pl4.Visible = false;
            cb2.Visible = true;
            label4.Visible = false;
            label5.Visible = false;
            mtb21.Enabled = mtb22.Enabled = false;
            cb1_SelectionChangeCommitted(null, null);
            mtb11_Leave(null, null);
        }
        public void Go4()
        {
            cb2.Visible = false;
            pl4.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            mtb21.Enabled = mtb22.Enabled = true;
            mtb21.Text = Settings.I.advTariff.p1_time3;
            mtb22.Text = Settings.I.advTariff.p1_time4;
            mtb41.Text = Settings.I.advTariff.p3_time3;
            mtb42.Text = Settings.I.advTariff.p3_time4;
            cb1_SelectionChangeCommitted(null, null);
            mtb11_Leave(null, null);
            mtb21_Leave(null, null);
        }

        private void embSimplePeriods_Load(object sender, EventArgs e)
        {
            //cbPeriods.SelectedIndex = 0;
            //UpdateSimple();
            //UpdatePrices();
        }
        // Summer: Last Sunday of March ~ Last Sunday of October
        // Winter: The other time

        public void OnPriceAlias(object sender, EventArgs e)
        {
            PriceAliasEvent f = (PriceAliasEvent)e;
            UpdatePrices(f.alias);
        }
        private bool Is2 { get { return !cb3.Visible; } }
        
        private void cb1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int idx = 1 & (cb1.SelectedIndex + 1);
            if (Is2)
            {
                if (cb2.Items.Count > idx)
                    cb2.SelectedIndex = idx;
            }
            else
            {
                if (cb3.Items.Count > idx)
                    cb3.SelectedIndex = idx;
            }
        }


        private void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cb3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int idx = 1 & (cb3.SelectedIndex + 1);
            if (!Is2)
            {
                if (cb1.Items.Count > idx)
                    cb1.SelectedIndex = idx;
            }

        }
        private void cb3_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void Update2()
        {
            if (!mtb11.MaskCompleted || !mtb12.MaskCompleted)
                return;
            string s1 = mtb11.Text;
            string s2 = mtb12.Text;
            DateTime t1 = DateTime.Parse(s1);
            DateTime t2 = DateTime.Parse(s2);
            mtb21.Text = s2;
            mtb22.Text = s1;
        }
        private void Update4()
        {
            if (!mtb31.MaskCompleted || !mtb32.MaskCompleted)
                return;
            string s1 = mtb11.Text;
            string s2 = mtb12.Text;
            string s3 = mtb21.Text;
            string s4 = mtb22.Text;

            DateTime t1 = DateTime.Parse(s1);
            DateTime t2 = DateTime.Parse(s2);
            DateTime t3 = DateTime.Parse(s3);
            DateTime t4 = DateTime.Parse(s4);

            mtb31.Text = s2;
            mtb32.Text = s1;
            mtb41.Text = s4;
            mtb42.Text = s3;
            //mtb41.Text = s2;
            //mtb42.Text = s1;
        }
        private void mtb11_Leave(object sender, EventArgs e)
        {
            Update2Or4();
        }

        private void mtb21_Leave(object sender, EventArgs e)
        {
            Update2Or4();
        }
        private void Update2Or4()
        {
            if (Is2)
                Update2();
            else
                Update4();
        }
        public int Price1
        {
            get { return cb1.SelectedIndex; }
            set { cb1.SelectedIndex = value; Update2Or4(); }
        }
        public int Price2
        {
            get { return cb2.SelectedIndex; }
            //set { cb2.SelectedIndex = value; }
        }
        public int Price3
        {
            get { return cb3.SelectedIndex; }
            set { cb3.SelectedIndex = value; Update2Or4(); }
        }
        public string P1Time1
        {
            get { return mtb11.Text; }
            set { mtb11.Text = value; Update2Or4(); }
        }
        public string P1Time2
        {
            get { return mtb12.Text; }
            set { mtb12.Text = value; Update2Or4(); }
        }
        public string P1Time3
        {
            get { return mtb21.Text; }
            set { mtb21.Text = value; Update2Or4(); }
        }
        public string P1Time4
        {
            get { return mtb22.Text; }
            set { mtb22.Text = value; Update2Or4(); }
        }
        public string P3Time1
        {
            get { return mtb31.Text; }
            set { mtb31.Text = value; Update2Or4(); }
        }
        public string P3Time2
        {
            get { return mtb32.Text; }
            set { mtb32.Text = value; Update2Or4(); }
        }
        public string P3Time3
        {
            get { return mtb41.Text; }
            set { mtb41.Text = value; Update2Or4(); }
        }
        public string P3Time4
        {
            get { return mtb42.Text; }
            set { mtb42.Text = value; Update2Or4(); }
        }

        private void embSimplePeriods_Validating(object sender, CancelEventArgs e)
        {
            Update2Or4();
        }


    }
}
