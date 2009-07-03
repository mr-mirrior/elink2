namespace Elink
{
    partial class RoundScroll
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
            this.rndFore = new Elink.RoundRectControl();
            this.rndBack = new Elink.RoundRectControl();
            this.SuspendLayout();
            // 
            // rndFore
            // 
            this.rndFore.AllowGradient = false;
            this.rndFore.AllowGroup = false;
            this.rndFore.BackBackColor = System.Drawing.Color.LightGray;
            this.rndFore.BackColor = System.Drawing.Color.Gray;
            this.rndFore.Curve = 0;
            this.rndFore.ForeColor = System.Drawing.Color.White;
            this.rndFore.Location = new System.Drawing.Point(8, 2);
            this.rndFore.Name = "rndFore";
            this.rndFore.RightBackBackColor = System.Drawing.Color.LightGray;
            this.rndFore.Size = new System.Drawing.Size(88, 17);
            this.rndFore.TabIndex = 40;
            this.rndFore.TagDouble = 0;
            this.rndFore.UseVisualStyleBackColor = false;
            this.rndFore.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rndFore_MouseMove);
            this.rndFore.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rndFore_MouseDown);
            this.rndFore.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rndFore_MouseUp);
            // 
            // rndBack
            // 
            this.rndBack.AllowGradient = false;
            this.rndBack.AllowGroup = false;
            this.rndBack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rndBack.BackBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rndBack.BackColor = System.Drawing.Color.LightGray;
            this.rndBack.Curve = 0;
            this.rndBack.ForeColor = System.Drawing.Color.White;
            this.rndBack.IsHoverEnabled = false;
            this.rndBack.Location = new System.Drawing.Point(0, 1);
            this.rndBack.Name = "rndBack";
            this.rndBack.RightBackBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.rndBack.Size = new System.Drawing.Size(560, 20);
            this.rndBack.TabIndex = 39;
            this.rndBack.TagDouble = 0;
            this.rndBack.UseVisualStyleBackColor = false;
            this.rndBack.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rndBack_MouseClick);
            this.rndBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rndBack_MouseDown);
            // 
            // RoundScroll
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.rndFore);
            this.Controls.Add(this.rndBack);
            this.Name = "RoundScroll";
            this.Size = new System.Drawing.Size(560, 25);
            this.Resize += new System.EventHandler(this.RoundScroll_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private RoundRectControl rndBack;
        private RoundRectControl rndFore;
    }
}
