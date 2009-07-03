namespace Elink
{
    partial class frmMonthlyCompare
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonthlyCompare));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbConclusion = new System.Windows.Forms.Label();
            this.lbDifference = new System.Windows.Forms.Label();
            this.dashLabel5 = new Elink.DashLabel(this.components);
            this.simprice = new Elink.RoundRectControl();
            this.realprice = new Elink.RoundRectControl();
            this.dashLabel4 = new Elink.DashLabel(this.components);
            this.dashLabel3 = new Elink.DashLabel(this.components);
            this.dashLabel2 = new Elink.DashLabel(this.components);
            this.dashLabel1 = new Elink.DashLabel(this.components);
            this.dash1 = new Elink.DashLabel(this.components);
            this.plBounds = new System.Windows.Forms.Panel();
            this.lbRatio = new System.Windows.Forms.Label();
            this.lbCurrency = new System.Windows.Forms.Label();
            this.lb3 = new System.Windows.Forms.Label();
            this.lb2 = new System.Windows.Forms.Label();
            this.lb1 = new System.Windows.Forms.Label();
            this.lb0 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.lbSim = new System.Windows.Forms.Label();
            this.lbSimName = new System.Windows.Forms.Label();
            this.lbReal = new System.Windows.Forms.Label();
            this.lbRealName = new System.Windows.Forms.Label();
            this.lb4 = new System.Windows.Forms.Label();
            this.lbMonth = new System.Windows.Forms.Label();
            this.rndFrame = new Elink.RoundFrame();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbConclusion);
            this.panel1.Controls.Add(this.lbDifference);
            this.panel1.Controls.Add(this.dashLabel5);
            this.panel1.Controls.Add(this.simprice);
            this.panel1.Controls.Add(this.realprice);
            this.panel1.Controls.Add(this.dashLabel4);
            this.panel1.Controls.Add(this.dashLabel3);
            this.panel1.Controls.Add(this.dashLabel2);
            this.panel1.Controls.Add(this.dashLabel1);
            this.panel1.Controls.Add(this.dash1);
            this.panel1.Controls.Add(this.plBounds);
            this.panel1.Controls.Add(this.lbRatio);
            this.panel1.Controls.Add(this.lbCurrency);
            this.panel1.Controls.Add(this.lb3);
            this.panel1.Controls.Add(this.lb2);
            this.panel1.Controls.Add(this.lb1);
            this.panel1.Controls.Add(this.lb0);
            this.panel1.Controls.Add(this.pic);
            this.panel1.Controls.Add(this.lbSim);
            this.panel1.Controls.Add(this.lbSimName);
            this.panel1.Controls.Add(this.lbReal);
            this.panel1.Controls.Add(this.lbRealName);
            this.panel1.Controls.Add(this.lb4);
            this.panel1.Controls.Add(this.lbMonth);
            this.panel1.Controls.Add(this.rndFrame);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // lbConclusion
            // 
            this.lbConclusion.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.lbConclusion, "lbConclusion");
            this.lbConclusion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.lbConclusion.Name = "lbConclusion";
            // 
            // lbDifference
            // 
            this.lbDifference.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lbDifference, "lbDifference");
            this.lbDifference.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.lbDifference.Name = "lbDifference";
            // 
            // dashLabel5
            // 
            this.dashLabel5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dashLabel5.DashLength = 0;
            this.dashLabel5.DashWidth = 2;
            this.dashLabel5.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.dashLabel5, "dashLabel5");
            this.dashLabel5.Name = "dashLabel5";
            // 
            // simprice
            // 
            this.simprice.AllowGradient = true;
            this.simprice.AllowGroup = false;
            this.simprice.BackBackColor = System.Drawing.Color.WhiteSmoke;
            this.simprice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.simprice.Curve = 0;
            this.simprice.ForeColor = System.Drawing.Color.White;
            this.simprice.GradientColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.simprice, "simprice");
            this.simprice.IsHoverEnabled = false;
            this.simprice.Name = "simprice";
            this.simprice.RightBackBackColor = System.Drawing.Color.WhiteSmoke;
            this.simprice.TagDouble = 0;
            this.toolTip1.SetToolTip(this.simprice, resources.GetString("simprice.ToolTip"));
            this.simprice.UseVisualStyleBackColor = false;
            this.simprice.Vertical = true;
            // 
            // realprice
            // 
            this.realprice.AllowGradient = true;
            this.realprice.AllowGroup = false;
            this.realprice.BackBackColor = System.Drawing.Color.WhiteSmoke;
            this.realprice.BackColor = System.Drawing.Color.DimGray;
            this.realprice.Curve = 0;
            this.realprice.ForeColor = System.Drawing.Color.White;
            this.realprice.GradientColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.realprice, "realprice");
            this.realprice.IsHoverEnabled = false;
            this.realprice.Name = "realprice";
            this.realprice.RightBackBackColor = System.Drawing.Color.WhiteSmoke;
            this.realprice.TagDouble = 0;
            this.toolTip1.SetToolTip(this.realprice, resources.GetString("realprice.ToolTip"));
            this.realprice.UseVisualStyleBackColor = false;
            this.realprice.Vertical = true;
            // 
            // dashLabel4
            // 
            this.dashLabel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dashLabel4.DashLength = 0;
            this.dashLabel4.DashWidth = 2;
            this.dashLabel4.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.dashLabel4, "dashLabel4");
            this.dashLabel4.Name = "dashLabel4";
            // 
            // dashLabel3
            // 
            this.dashLabel3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dashLabel3.DashLength = 0;
            this.dashLabel3.DashWidth = 2;
            this.dashLabel3.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.dashLabel3, "dashLabel3");
            this.dashLabel3.Name = "dashLabel3";
            // 
            // dashLabel2
            // 
            this.dashLabel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dashLabel2.DashLength = 0;
            this.dashLabel2.DashWidth = 2;
            this.dashLabel2.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.dashLabel2, "dashLabel2");
            this.dashLabel2.Name = "dashLabel2";
            // 
            // dashLabel1
            // 
            this.dashLabel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dashLabel1.DashLength = 0;
            this.dashLabel1.DashWidth = 2;
            this.dashLabel1.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.dashLabel1, "dashLabel1");
            this.dashLabel1.Name = "dashLabel1";
            // 
            // dash1
            // 
            this.dash1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dash1.DashLength = 0;
            this.dash1.DashWidth = 2;
            this.dash1.ForeColor = System.Drawing.Color.Silver;
            resources.ApplyResources(this.dash1, "dash1");
            this.dash1.Name = "dash1";
            // 
            // plBounds
            // 
            this.plBounds.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.plBounds, "plBounds");
            this.plBounds.Name = "plBounds";
            // 
            // lbRatio
            // 
            this.lbRatio.BackColor = System.Drawing.Color.Transparent;
            this.lbRatio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            resources.ApplyResources(this.lbRatio, "lbRatio");
            this.lbRatio.Name = "lbRatio";
            // 
            // lbCurrency
            // 
            this.lbCurrency.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lbCurrency, "lbCurrency");
            this.lbCurrency.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbCurrency.Name = "lbCurrency";
            // 
            // lb3
            // 
            this.lb3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lb3.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lb3, "lb3");
            this.lb3.Name = "lb3";
            // 
            // lb2
            // 
            this.lb2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lb2.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lb2, "lb2");
            this.lb2.Name = "lb2";
            // 
            // lb1
            // 
            this.lb1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lb1.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lb1, "lb1");
            this.lb1.Name = "lb1";
            // 
            // lb0
            // 
            this.lb0.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lb0.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lb0, "lb0");
            this.lb0.Name = "lb0";
            // 
            // pic
            // 
            resources.ApplyResources(this.pic, "pic");
            this.pic.Name = "pic";
            this.pic.TabStop = false;
            // 
            // lbSim
            // 
            this.lbSim.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lbSim, "lbSim");
            this.lbSim.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.lbSim.Name = "lbSim";
            // 
            // lbSimName
            // 
            this.lbSimName.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lbSimName, "lbSimName");
            this.lbSimName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.lbSimName.Name = "lbSimName";
            // 
            // lbReal
            // 
            this.lbReal.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lbReal, "lbReal");
            this.lbReal.ForeColor = System.Drawing.Color.DimGray;
            this.lbReal.Name = "lbReal";
            // 
            // lbRealName
            // 
            this.lbRealName.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.lbRealName, "lbRealName");
            this.lbRealName.ForeColor = System.Drawing.Color.DimGray;
            this.lbRealName.Name = "lbRealName";
            // 
            // lb4
            // 
            this.lb4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lb4.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.lb4, "lb4");
            this.lb4.Name = "lb4";
            // 
            // lbMonth
            // 
            resources.ApplyResources(this.lbMonth, "lbMonth");
            this.lbMonth.Name = "lbMonth";
            // 
            // rndFrame
            // 
            resources.ApplyResources(this.rndFrame, "rndFrame");
            this.rndFrame.BackColor = System.Drawing.Color.Transparent;
            this.rndFrame.Curve = 15;
            this.rndFrame.FillColor = System.Drawing.Color.WhiteSmoke;
            this.rndFrame.ForeColor = System.Drawing.Color.DimGray;
            this.rndFrame.FrameColor = System.Drawing.Color.Transparent;
            this.rndFrame.Name = "rndFrame";
            // 
            // frmMonthlyCompare
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.DimGray;
            this.KeyPreview = true;
            this.Name = "frmMonthlyCompare";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MonthlyCompare_Load);
            this.Move += new System.EventHandler(this.frmMonthlyCompare_Move);
            this.Resize += new System.EventHandler(this.MonthlyCompare_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMonthlyCompare_KeyDown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private RoundFrame rndFrame;
        private System.Windows.Forms.Label lbMonth;
        private DashLabel dash1;
        private System.Windows.Forms.Label lb4;
        private System.Windows.Forms.Label lbRealName;
        private System.Windows.Forms.Label lbReal;
        private System.Windows.Forms.Label lbSimName;
        private System.Windows.Forms.Label lbSim;
        private System.Windows.Forms.Label lbDifference;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Label lbCurrency;
        private DashLabel dashLabel4;
        private System.Windows.Forms.Label lb3;
        private DashLabel dashLabel3;
        private System.Windows.Forms.Label lb2;
        private DashLabel dashLabel2;
        private System.Windows.Forms.Label lb1;
        private DashLabel dashLabel1;
        private System.Windows.Forms.Label lb0;
        private RoundRectControl realprice;
        private RoundRectControl simprice;
        private System.Windows.Forms.Label lbRatio;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel plBounds;
        private DashLabel dashLabel5;
        private System.Windows.Forms.Label lbConclusion;
    }
}