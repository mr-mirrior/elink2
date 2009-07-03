namespace Elink
{
    partial class DisplayData
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayData));
            this.plReports = new System.Windows.Forms.Panel();
            this.btnTest = new Elink.RoundRectControl();
            this.btnSmooth = new Elink.RoundRectControl();
            this.btnMonthly = new Elink.RoundRectControl();
            this.btnWeekly = new Elink.RoundRectControl();
            this.btnCarbon = new Elink.RoundRectControl();
            this.btnCosts = new Elink.RoundRectControl();
            this.btnEnergy = new Elink.RoundRectControl();
            this.label1 = new Elink.RoundRectControl();
            this.rndFrame = new Elink.RoundFrame();
            this.plReports.SuspendLayout();
            this.SuspendLayout();
            // 
            // plReports
            // 
            this.plReports.AccessibleDescription = null;
            this.plReports.AccessibleName = null;
            resources.ApplyResources(this.plReports, "plReports");
            this.plReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.plReports.BackgroundImage = null;
            this.plReports.Controls.Add(this.btnTest);
            this.plReports.Font = null;
            this.plReports.Name = "plReports";
            // 
            // btnTest
            // 
            this.btnTest.AccessibleDescription = null;
            this.btnTest.AccessibleName = null;
            this.btnTest.AllowGradient = false;
            this.btnTest.AllowGroup = false;
            resources.ApplyResources(this.btnTest, "btnTest");
            this.btnTest.BackColor = System.Drawing.Color.Sienna;
            this.btnTest.BackgroundImage = null;
            this.btnTest.Curve = 0;
            this.btnTest.Font = null;
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Name = "btnTest";
            this.btnTest.TagDouble = 0;
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSmooth
            // 
            this.btnSmooth.AccessibleDescription = null;
            this.btnSmooth.AccessibleName = null;
            this.btnSmooth.AllowGradient = false;
            this.btnSmooth.AllowGroup = false;
            resources.ApplyResources(this.btnSmooth, "btnSmooth");
            this.btnSmooth.BackColor = System.Drawing.Color.Gray;
            this.btnSmooth.BackgroundImage = null;
            this.btnSmooth.Curve = 0;
            this.btnSmooth.Font = null;
            this.btnSmooth.ForeColor = System.Drawing.Color.White;
            this.btnSmooth.IsDown = true;
            this.btnSmooth.IsDownEnabled = true;
            this.btnSmooth.IsHoverEnabled = false;
            this.btnSmooth.Name = "btnSmooth";
            this.btnSmooth.TagDouble = 0;
            this.btnSmooth.UseVisualStyleBackColor = false;
            this.btnSmooth.Click += new System.EventHandler(this.btnSmooth_Click);
            // 
            // btnMonthly
            // 
            this.btnMonthly.AccessibleDescription = null;
            this.btnMonthly.AccessibleName = null;
            this.btnMonthly.AllowGradient = false;
            this.btnMonthly.AllowGroup = true;
            resources.ApplyResources(this.btnMonthly, "btnMonthly");
            this.btnMonthly.BackColor = System.Drawing.Color.Gray;
            this.btnMonthly.BackgroundImage = null;
            this.btnMonthly.Curve = 0;
            this.btnMonthly.Font = null;
            this.btnMonthly.ForeColor = System.Drawing.Color.White;
            this.btnMonthly.Group = 5;
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.TagDouble = 0;
            this.btnMonthly.UseVisualStyleBackColor = false;
            this.btnMonthly.Click += new System.EventHandler(this.btnMonthly_Click);
            // 
            // btnWeekly
            // 
            this.btnWeekly.AccessibleDescription = null;
            this.btnWeekly.AccessibleName = null;
            this.btnWeekly.AllowGradient = false;
            this.btnWeekly.AllowGroup = true;
            resources.ApplyResources(this.btnWeekly, "btnWeekly");
            this.btnWeekly.BackColor = System.Drawing.Color.Gray;
            this.btnWeekly.BackgroundImage = null;
            this.btnWeekly.Curve = 0;
            this.btnWeekly.Font = null;
            this.btnWeekly.ForeColor = System.Drawing.Color.White;
            this.btnWeekly.Group = 5;
            this.btnWeekly.IsDown = true;
            this.btnWeekly.Name = "btnWeekly";
            this.btnWeekly.TagDouble = 0;
            this.btnWeekly.UseVisualStyleBackColor = false;
            this.btnWeekly.Click += new System.EventHandler(this.btnWeekly_Click);
            // 
            // btnCarbon
            // 
            this.btnCarbon.AccessibleDescription = null;
            this.btnCarbon.AccessibleName = null;
            this.btnCarbon.AllowGradient = false;
            this.btnCarbon.AllowGroup = true;
            resources.ApplyResources(this.btnCarbon, "btnCarbon");
            this.btnCarbon.BackColor = System.Drawing.Color.Gray;
            this.btnCarbon.BackgroundImage = null;
            this.btnCarbon.Curve = 0;
            this.btnCarbon.Font = null;
            this.btnCarbon.ForeColor = System.Drawing.Color.White;
            this.btnCarbon.Group = 4;
            this.btnCarbon.Name = "btnCarbon";
            this.btnCarbon.TagDouble = 0;
            this.btnCarbon.UseVisualStyleBackColor = false;
            this.btnCarbon.Click += new System.EventHandler(this.btnCarbon_Click);
            // 
            // btnCosts
            // 
            this.btnCosts.AccessibleDescription = null;
            this.btnCosts.AccessibleName = null;
            this.btnCosts.AllowGradient = false;
            this.btnCosts.AllowGroup = true;
            resources.ApplyResources(this.btnCosts, "btnCosts");
            this.btnCosts.BackColor = System.Drawing.Color.Gray;
            this.btnCosts.BackgroundImage = null;
            this.btnCosts.Curve = 0;
            this.btnCosts.Font = null;
            this.btnCosts.ForeColor = System.Drawing.Color.White;
            this.btnCosts.Group = 4;
            this.btnCosts.Name = "btnCosts";
            this.btnCosts.TagDouble = 0;
            this.btnCosts.UseVisualStyleBackColor = false;
            this.btnCosts.Click += new System.EventHandler(this.btnCosts_Click);
            // 
            // btnEnergy
            // 
            this.btnEnergy.AccessibleDescription = null;
            this.btnEnergy.AccessibleName = null;
            this.btnEnergy.AllowGradient = false;
            this.btnEnergy.AllowGroup = true;
            resources.ApplyResources(this.btnEnergy, "btnEnergy");
            this.btnEnergy.BackColor = System.Drawing.Color.Gray;
            this.btnEnergy.BackgroundImage = null;
            this.btnEnergy.Curve = 0;
            this.btnEnergy.Font = null;
            this.btnEnergy.ForeColor = System.Drawing.Color.White;
            this.btnEnergy.Group = 4;
            this.btnEnergy.IsDown = true;
            this.btnEnergy.Name = "btnEnergy";
            this.btnEnergy.TagDouble = 0;
            this.btnEnergy.UseVisualStyleBackColor = false;
            this.btnEnergy.Click += new System.EventHandler(this.btnEnergy_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            this.label1.AllowGradient = false;
            this.label1.AllowGroup = false;
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.BackgroundImage = null;
            this.label1.Curve = 15;
            this.label1.Font = null;
            this.label1.IsHoverEnabled = false;
            this.label1.Name = "label1";
            this.label1.TabStop = false;
            this.label1.TagDouble = 0;
            this.label1.UseVisualStyleBackColor = false;
            // 
            // rndFrame
            // 
            this.rndFrame.AccessibleDescription = null;
            this.rndFrame.AccessibleName = null;
            resources.ApplyResources(this.rndFrame, "rndFrame");
            this.rndFrame.BackColor = System.Drawing.Color.Transparent;
            this.rndFrame.Curve = 15;
            this.rndFrame.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rndFrame.Font = null;
            this.rndFrame.ForeColor = System.Drawing.Color.DimGray;
            this.rndFrame.FrameColor = System.Drawing.Color.Transparent;
            this.rndFrame.Name = "rndFrame";
            // 
            // DisplayData
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = null;
            this.Controls.Add(this.btnSmooth);
            this.Controls.Add(this.btnMonthly);
            this.Controls.Add(this.btnWeekly);
            this.Controls.Add(this.plReports);
            this.Controls.Add(this.btnCarbon);
            this.Controls.Add(this.btnCosts);
            this.Controls.Add(this.btnEnergy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rndFrame);
            this.Font = null;
            this.Name = "DisplayData";
            this.plReports.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundRectControl label1;
        private RoundRectControl btnEnergy;
        private RoundRectControl btnCosts;
        private RoundRectControl btnCarbon;
        private System.Windows.Forms.Panel plReports;
        private RoundRectControl btnTest;
        private RoundRectControl btnWeekly;
        private RoundRectControl btnMonthly;
        private RoundFrame rndFrame;
        private RoundRectControl btnSmooth;
    }
}
