namespace Elink
{
    partial class Sub2RightSingleTariff
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sub2RightSingleTariff));
            this.lb213 = new System.Windows.Forms.Label();
            this.lb212 = new System.Windows.Forms.Label();
            this.tbPayForElec = new System.Windows.Forms.TextBox();
            this.lb211 = new System.Windows.Forms.Label();
            this.rnd211 = new Elink.RoundRectControl();
            this.ep = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ep)).BeginInit();
            this.SuspendLayout();
            // 
            // lb213
            // 
            this.lb213.BackColor = System.Drawing.Color.Gray;
            this.lb213.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lb213, "lb213");
            this.lb213.Name = "lb213";
            // 
            // lb212
            // 
            this.lb212.BackColor = System.Drawing.Color.Gray;
            this.lb212.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lb212, "lb212");
            this.lb212.Name = "lb212";
            // 
            // tbPayForElec
            // 
            this.tbPayForElec.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.tbPayForElec, "tbPayForElec");
            this.tbPayForElec.Name = "tbPayForElec";
            // 
            // lb211
            // 
            this.lb211.BackColor = System.Drawing.Color.Gray;
            this.lb211.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.lb211, "lb211");
            this.lb211.Name = "lb211";
            // 
            // rnd211
            // 
            this.rnd211.AllowGradient = false;
            this.rnd211.AllowGroup = false;
            this.rnd211.BackBackColor = System.Drawing.Color.Gray;
            this.rnd211.BackColor = System.Drawing.Color.White;
            this.rnd211.Curve = 0;
            this.rnd211.ForeColor = System.Drawing.Color.White;
            this.rnd211.IsHoverEnabled = false;
            resources.ApplyResources(this.rnd211, "rnd211");
            this.rnd211.Name = "rnd211";
            this.rnd211.RightBackBackColor = System.Drawing.Color.Gray;
            this.rnd211.TabStop = false;
            this.rnd211.TagDouble = null;
            this.rnd211.UseVisualStyleBackColor = false;
            // 
            // ep
            // 
            this.ep.ContainerControl = this;
            // 
            // Sub2RightSingleTariff
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lb213);
            this.Controls.Add(this.lb212);
            this.Controls.Add(this.tbPayForElec);
            this.Controls.Add(this.rnd211);
            this.Controls.Add(this.lb211);
            resources.ApplyResources(this, "$this");
            this.Name = "Sub2RightSingleTariff";
            this.Load += new System.EventHandler(this.Sub2RightSingleTariff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb213;
        private System.Windows.Forms.Label lb212;
        private System.Windows.Forms.TextBox tbPayForElec;
        private RoundRectControl rnd211;
        private System.Windows.Forms.Label lb211;
        private System.Windows.Forms.ErrorProvider ep;
    }
}
