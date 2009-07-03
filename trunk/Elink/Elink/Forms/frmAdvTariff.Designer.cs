namespace Elink
{
    partial class frmAdvTariff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdvTariff));
            this.plAdv = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbTariffName = new System.Windows.Forms.Label();
            this.tbTariff3 = new System.Windows.Forms.TextBox();
            this.lbTariff3 = new System.Windows.Forms.Label();
            this.tbPrice3 = new System.Windows.Forms.TextBox();
            this.tbTariff2 = new System.Windows.Forms.TextBox();
            this.tbTariff1 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.cbPeriods = new System.Windows.Forms.ComboBox();
            this.lbTariff2 = new System.Windows.Forms.Label();
            this.lbTariff1 = new System.Windows.Forms.Label();
            this.tbPrice2 = new System.Windows.Forms.TextBox();
            this.tbPrice1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdvanced = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbTariffs = new System.Windows.Forms.ComboBox();
            this.btnRet = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.plSimple = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.plAdv.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // plAdv
            // 
            this.plAdv.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plAdv.Controls.Add(this.flowLayoutPanel1);
            resources.ApplyResources(this.plAdv, "plAdv");
            this.plAdv.Name = "plAdv";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbTariffName);
            this.panel2.Controls.Add(this.tbTariff3);
            this.panel2.Controls.Add(this.lbTariff3);
            this.panel2.Controls.Add(this.tbPrice3);
            this.panel2.Controls.Add(this.tbTariff2);
            this.panel2.Controls.Add(this.tbTariff1);
            this.panel2.Controls.Add(this.label21);
            this.panel2.Controls.Add(this.cbPeriods);
            this.panel2.Controls.Add(this.lbTariff2);
            this.panel2.Controls.Add(this.lbTariff1);
            this.panel2.Controls.Add(this.tbPrice2);
            this.panel2.Controls.Add(this.tbPrice1);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnAdvanced);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.cbTariffs);
            this.panel2.Controls.Add(this.btnRet);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // lbTariffName
            // 
            resources.ApplyResources(this.lbTariffName, "lbTariffName");
            this.lbTariffName.ForeColor = System.Drawing.Color.OrangeRed;
            this.lbTariffName.Name = "lbTariffName";
            // 
            // tbTariff3
            // 
            resources.ApplyResources(this.tbTariff3, "tbTariff3");
            this.tbTariff3.Name = "tbTariff3";
            this.tbTariff3.ReadOnly = true;
            this.toolTip1.SetToolTip(this.tbTariff3, resources.GetString("tbTariff3.ToolTip"));
            this.tbTariff3.Click += new System.EventHandler(this.Tariff_Click);
            this.tbTariff3.Leave += new System.EventHandler(this.Tariff_Leave);
            // 
            // lbTariff3
            // 
            resources.ApplyResources(this.lbTariff3, "lbTariff3");
            this.lbTariff3.Name = "lbTariff3";
            // 
            // tbPrice3
            // 
            resources.ApplyResources(this.tbPrice3, "tbPrice3");
            this.tbPrice3.Name = "tbPrice3";
            // 
            // tbTariff2
            // 
            resources.ApplyResources(this.tbTariff2, "tbTariff2");
            this.tbTariff2.Name = "tbTariff2";
            this.tbTariff2.ReadOnly = true;
            this.toolTip1.SetToolTip(this.tbTariff2, resources.GetString("tbTariff2.ToolTip"));
            this.tbTariff2.Click += new System.EventHandler(this.Tariff_Click);
            this.tbTariff2.Leave += new System.EventHandler(this.Tariff_Leave);
            // 
            // tbTariff1
            // 
            resources.ApplyResources(this.tbTariff1, "tbTariff1");
            this.tbTariff1.Name = "tbTariff1";
            this.tbTariff1.ReadOnly = true;
            this.toolTip1.SetToolTip(this.tbTariff1, resources.GetString("tbTariff1.ToolTip"));
            this.tbTariff1.Click += new System.EventHandler(this.Tariff_Click);
            this.tbTariff1.Leave += new System.EventHandler(this.Tariff_Leave);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // cbPeriods
            // 
            this.cbPeriods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriods.FormattingEnabled = true;
            this.cbPeriods.Items.AddRange(new object[] {
            resources.GetString("cbPeriods.Items"),
            resources.GetString("cbPeriods.Items1")});
            resources.ApplyResources(this.cbPeriods, "cbPeriods");
            this.cbPeriods.Name = "cbPeriods";
            this.cbPeriods.SelectedIndexChanged += new System.EventHandler(this.cbPeriods_SelectedIndexChanged);
            // 
            // lbTariff2
            // 
            resources.ApplyResources(this.lbTariff2, "lbTariff2");
            this.lbTariff2.Name = "lbTariff2";
            // 
            // lbTariff1
            // 
            resources.ApplyResources(this.lbTariff1, "lbTariff1");
            this.lbTariff1.Name = "lbTariff1";
            // 
            // tbPrice2
            // 
            resources.ApplyResources(this.tbPrice2, "tbPrice2");
            this.tbPrice2.Name = "tbPrice2";
            // 
            // tbPrice1
            // 
            resources.ApplyResources(this.tbPrice1, "tbPrice1");
            this.tbPrice1.Name = "tbPrice1";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btnAdvanced
            // 
            resources.ApplyResources(this.btnAdvanced, "btnAdvanced");
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.UseVisualStyleBackColor = true;
            this.btnAdvanced.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // cbTariffs
            // 
            this.cbTariffs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cbTariffs, "cbTariffs");
            this.cbTariffs.FormattingEnabled = true;
            this.cbTariffs.Items.AddRange(new object[] {
            resources.GetString("cbTariffs.Items"),
            resources.GetString("cbTariffs.Items1")});
            this.cbTariffs.Name = "cbTariffs";
            this.cbTariffs.SelectedIndexChanged += new System.EventHandler(this.tbTariff_SelectedIndexChanged);
            // 
            // btnRet
            // 
            resources.ApplyResources(this.btnRet, "btnRet");
            this.btnRet.Name = "btnRet";
            this.btnRet.UseVisualStyleBackColor = true;
            this.btnRet.Click += new System.EventHandler(this.btnRet_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // plSimple
            // 
            resources.ApplyResources(this.plSimple, "plSimple");
            this.plSimple.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.plSimple.Name = "plSimple";
            // 
            // frmAdvTariff
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.button2;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.plSimple);
            this.Controls.Add(this.plAdv);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Name = "frmAdvTariff";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmAdvTariff_Load);
            this.plAdv.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel plAdv;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbTariffs;
        private System.Windows.Forms.Button btnAdvanced;
        private System.Windows.Forms.Label lbTariff2;
        private System.Windows.Forms.Label lbTariff1;
        private System.Windows.Forms.TextBox tbPrice2;
        private System.Windows.Forms.TextBox tbPrice1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel plSimple;
        private System.Windows.Forms.Button btnRet;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ComboBox cbPeriods;
        private System.Windows.Forms.TextBox tbTariff1;
        private System.Windows.Forms.TextBox tbTariff2;
        private System.Windows.Forms.TextBox tbTariff3;
        private System.Windows.Forms.Label lbTariff3;
        private System.Windows.Forms.TextBox tbPrice3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lbTariffName;
    }
}