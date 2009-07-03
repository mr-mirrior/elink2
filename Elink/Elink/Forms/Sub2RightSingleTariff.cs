using System;
using System.Drawing;
using System.Windows.Forms;

namespace Elink
{
    public partial class Sub2RightSingleTariff : UserControl, ITariff
    {
        public Sub2RightSingleTariff()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
        }

        public Color AllbackColor 
        {
            set
            {
                lb211.BackColor = lb212.BackColor = lb213.BackColor = value;
                rnd211.BackBackColor = rnd211.RightBackBackColor = value;
            }
        }
        public bool IsFocused
        {
            get
            {
                return tbPayForElec.Focused;
            }
        }
        public void SetCapture(EventHandler enter, EventHandler leave)
        {
            foreach(Control c in Controls)
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
                return double.Parse(tbPayForElec.Text);
            }
            set
            {
                tbPayForElec.Text = value.ToString();
            }
        }

//         bool IsValid
//         {
//             get
//             {
//                 if (tbPayForElec.Text.Length == 0)
//                     return false;
//                 if (!SubRightSettings.IsDouble(tbPayForElec.Text))
//                     return false;
//                 return true;
//             }
//         }
//         void CommonValidate(Control c, string err, bool check, ref bool result)
//         {
//             result = true;
//             result &= check;
//             if (!result)
//                 ep.SetError(c, err);
//             else
//                 ep.SetError(c, "");
//             ep.SetIconPadding(c, 0);
//         }
//         public bool CheckValid()
//         {
//             bool check = true;
//             CommonValidate(tbPayForElec, "Price cannot be empty", tbPayForElec.Text.Length != 0, ref check);
//             if (!check)
//                 return false;
//             CommonValidate(tbPayForElec, "Price should be numerical", SubRightSettings.IsDouble(tbPayForElec.Text), ref check);
//             return check;
//         }
//         private void tbPayForElec_Validating(object sender, CancelEventArgs e)
//         {
//             CheckValid();
//         }

        private void Sub2RightSingleTariff_Load(object sender, EventArgs e)
        {
            SubRightSettings.Instance.OnPriceChange += OnPriceChange;
            lb213.Text = Settings.I.currency + "/kWh";
        }
        private void OnPriceChange(object sender, EventArgs e)
        {
            lb213.Text = Settings.I.currency + "/kWh";
        }
    }
}
