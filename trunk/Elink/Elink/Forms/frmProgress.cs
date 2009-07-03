using System;
using System.Windows.Forms;
namespace Elink
{
    public delegate bool BoolInvokeDelegate();
    public delegate void InvokeDelegate();
    public delegate void InvokeDelegateInt(int x);
    public delegate void InvokeDelegateString(string x);
    public delegate void InvokeDelegateEvent(object sender, EventArgs e);


    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
        }
        private void SetPos(int value)
        {
            int pos = Math.Max(progressBar.Minimum, value);
            pos = Math.Min(progressBar.Maximum, value);
            progressBar.Value = pos;
        }
        private void SetCaption(string s)
        {
            lbCaption.Text = s;
        }
        public int Position
        {
            get { return progressBar.Value; }
            set
            {
                this.BeginInvoke(new InvokeDelegateInt(SetPos), value);
            }
        }
        public string Caption
        {
            get { return lbCaption.Text; }
            set { this.BeginInvoke(new InvokeDelegateString(SetCaption), value); }
        }
        public void Finish()
        {
            InvokeDelegate d = new InvokeDelegate(delegate()
            {
                this.DialogResult = DialogResult.OK;
                Close();
            });
            this.BeginInvoke(d);
        }
        BoolInvokeDelegate autorun = null;
        public void AutoRun(BoolInvokeDelegate d)
        {
            autorun = d;
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            if (autorun != null)
                if (!autorun())
                {
                    this.DialogResult = DialogResult.Abort;
                    this.Close();
                }
        }
    }
}
