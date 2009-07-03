using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Elink
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        public void Switch()
        {
#if !DEBUG
            int last = 4000;
            int interval = 1000;

            this.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(last);

            this.Hide();
            Application.DoEvents();
            System.Threading.Thread.Sleep(interval);

            pictureBox1.Visible = false;
            pictureBox2.Visible = true;

            this.Show();
            Application.DoEvents();
            System.Threading.Thread.Sleep(last);
            this.Hide();
#endif
        }
    }
}
