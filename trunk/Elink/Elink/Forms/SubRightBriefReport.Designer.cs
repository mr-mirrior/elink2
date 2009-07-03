namespace Elink
{
    partial class BriefReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BriefReport));
            this.plReports = new System.Windows.Forms.Panel();
            this.btnCarbon = new Elink.RoundRectControl();
            this.btnCosts = new Elink.RoundRectControl();
            this.btnEnergy = new Elink.RoundRectControl();
            this.label1 = new Elink.RoundRectControl();
            this.SuspendLayout();
            // 
            // plReports
            // 
            this.plReports.AccessibleDescription = null;
            this.plReports.AccessibleName = null;
            resources.ApplyResources(this.plReports, "plReports");
            this.plReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.plReports.BackgroundImage = null;
            this.plReports.Font = null;
            this.plReports.Name = "plReports";
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
            this.btnCarbon.Group = 2;
            this.btnCarbon.Name = "btnCarbon";
            this.btnCarbon.TagDouble = null;
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
            this.btnCosts.Group = 2;
            this.btnCosts.Name = "btnCosts";
            this.btnCosts.TagDouble = null;
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
            this.btnEnergy.Group = 2;
            this.btnEnergy.IsDown = true;
            this.btnEnergy.Name = "btnEnergy";
            this.btnEnergy.TagDouble = null;
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
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.label1.BackgroundImage = null;
            this.label1.Curve = 15;
            this.label1.Font = null;
            this.label1.IsHoverEnabled = false;
            this.label1.Name = "label1";
            this.label1.TabStop = false;
            this.label1.TagDouble = null;
            this.label1.UseVisualStyleBackColor = false;
            // 
            // BriefReport
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = null;
            this.Controls.Add(this.plReports);
            this.Controls.Add(this.btnCarbon);
            this.Controls.Add(this.btnCosts);
            this.Controls.Add(this.btnEnergy);
            this.Controls.Add(this.label1);
            this.Font = null;
            this.Name = "BriefReport";
            this.ResumeLayout(false);

        }

        #endregion

        private RoundRectControl label1;
        private RoundRectControl btnEnergy;
        private RoundRectControl btnCosts;
        private RoundRectControl btnCarbon;
        private System.Windows.Forms.Panel plReports;
    }
}
