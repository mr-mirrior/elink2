namespace Elink
{
    partial class PercentageSmall
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
            IPercentage.I.Inform -= OnReload;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PercentageSmall));
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbPercent = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dotGood = new Elink.RoundRectControl();
            this.dotBad = new Elink.RoundRectControl();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AccessibleDescription = null;
            this.label7.AccessibleName = null;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Font = null;
            this.label7.ForeColor = System.Drawing.Color.DarkGray;
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Font = null;
            this.label6.ForeColor = System.Drawing.Color.DarkGray;
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // lbPercent
            // 
            this.lbPercent.AccessibleDescription = null;
            this.lbPercent.AccessibleName = null;
            resources.ApplyResources(this.lbPercent, "lbPercent");
            this.lbPercent.AutoEllipsis = true;
            this.lbPercent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.lbPercent.Name = "lbPercent";
            this.toolTip1.SetToolTip(this.lbPercent, resources.GetString("lbPercent.ToolTip"));
            // 
            // dotGood
            // 
            this.dotGood.AccessibleDescription = null;
            this.dotGood.AccessibleName = null;
            this.dotGood.AllowGradient = false;
            this.dotGood.AllowGroup = false;
            resources.ApplyResources(this.dotGood, "dotGood");
            this.dotGood.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.dotGood.BackgroundImage = null;
            this.dotGood.Curve = 0;
            this.dotGood.FocusBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(98)))), ((int)(((byte)(170)))));
            this.dotGood.Font = null;
            this.dotGood.ForeColor = System.Drawing.Color.White;
            this.dotGood.IsHoverEnabled = false;
            this.dotGood.Name = "dotGood";
            this.dotGood.TabStop = false;
            this.dotGood.TagDouble = null;
            this.toolTip1.SetToolTip(this.dotGood, resources.GetString("dotGood.ToolTip"));
            this.dotGood.UseVisualStyleBackColor = false;
            // 
            // dotBad
            // 
            this.dotBad.AccessibleDescription = null;
            this.dotBad.AccessibleName = null;
            this.dotBad.AllowGradient = false;
            this.dotBad.AllowGroup = false;
            resources.ApplyResources(this.dotBad, "dotBad");
            this.dotBad.BackColor = System.Drawing.Color.OrangeRed;
            this.dotBad.BackgroundImage = null;
            this.dotBad.Curve = 0;
            this.dotBad.FocusBackColor = System.Drawing.Color.Maroon;
            this.dotBad.Font = null;
            this.dotBad.ForeColor = System.Drawing.Color.White;
            this.dotBad.IsHoverEnabled = false;
            this.dotBad.Name = "dotBad";
            this.dotBad.TabStop = false;
            this.dotBad.TagDouble = null;
            this.toolTip1.SetToolTip(this.dotBad, resources.GetString("dotBad.ToolTip"));
            this.dotBad.UseVisualStyleBackColor = false;
            // 
            // PercentageSmall
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = null;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dotGood);
            this.Controls.Add(this.dotBad);
            this.Controls.Add(this.lbPercent);
            this.Controls.Add(this.label7);
            this.Name = "PercentageSmall";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private RoundRectControl dotGood;
        private RoundRectControl dotBad;
        private System.Windows.Forms.Label lbPercent;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
