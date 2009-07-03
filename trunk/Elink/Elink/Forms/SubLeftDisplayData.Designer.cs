namespace Elink
{
    partial class SubLeftDisplayData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SubLeftDisplayData));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbWeek = new Elink.RoundRectControl();
            this.lbCurve = new Elink.RoundRectControl();
            this.lbYear = new Elink.RoundRectControl();
            this.lbMonth = new Elink.RoundRectControl();
            this.label1 = new System.Windows.Forms.Label();
            this.lbDay = new Elink.RoundRectControl();
            this.plPercentSmall = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AccessibleDescription = null;
            this.tableLayoutPanel1.AccessibleName = null;
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.BackgroundImage = null;
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.plPercentSmall, 0, 0);
            this.tableLayoutPanel1.Font = null;
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = null;
            this.panel1.AccessibleName = null;
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackgroundImage = null;
            this.panel1.Controls.Add(this.lbWeek);
            this.panel1.Controls.Add(this.lbCurve);
            this.panel1.Controls.Add(this.lbYear);
            this.panel1.Controls.Add(this.lbMonth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbDay);
            this.panel1.Font = null;
            this.panel1.Name = "panel1";
            // 
            // lbWeek
            // 
            this.lbWeek.AccessibleDescription = null;
            this.lbWeek.AccessibleName = null;
            this.lbWeek.AllowGradient = false;
            this.lbWeek.AllowGroup = true;
            resources.ApplyResources(this.lbWeek, "lbWeek");
            this.lbWeek.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbWeek.BackgroundImage = null;
            this.lbWeek.Curve = 0;
            this.lbWeek.Font = null;
            this.lbWeek.ForeColor = System.Drawing.Color.White;
            this.lbWeek.Group = 3;
            this.lbWeek.IsDown = true;
            this.lbWeek.Name = "lbWeek";
            this.lbWeek.TagDouble = null;
            this.lbWeek.UseVisualStyleBackColor = false;
            this.lbWeek.Click += new System.EventHandler(this.lbWeek_Click);
            // 
            // lbCurve
            // 
            this.lbCurve.AccessibleDescription = null;
            this.lbCurve.AccessibleName = null;
            this.lbCurve.AllowGradient = false;
            this.lbCurve.AllowGroup = true;
            resources.ApplyResources(this.lbCurve, "lbCurve");
            this.lbCurve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbCurve.BackgroundImage = null;
            this.lbCurve.Curve = 0;
            this.lbCurve.Font = null;
            this.lbCurve.ForeColor = System.Drawing.Color.White;
            this.lbCurve.Group = 3;
            this.lbCurve.Name = "lbCurve";
            this.lbCurve.TagDouble = null;
            this.lbCurve.UseVisualStyleBackColor = false;
            this.lbCurve.Click += new System.EventHandler(this.btnCurve_Click);
            // 
            // lbYear
            // 
            this.lbYear.AccessibleDescription = null;
            this.lbYear.AccessibleName = null;
            this.lbYear.AllowGradient = false;
            this.lbYear.AllowGroup = true;
            resources.ApplyResources(this.lbYear, "lbYear");
            this.lbYear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbYear.BackgroundImage = null;
            this.lbYear.Curve = 0;
            this.lbYear.Font = null;
            this.lbYear.ForeColor = System.Drawing.Color.White;
            this.lbYear.Group = 3;
            this.lbYear.Name = "lbYear";
            this.lbYear.TagDouble = null;
            this.lbYear.UseVisualStyleBackColor = false;
            this.lbYear.Click += new System.EventHandler(this.lbYear_Click);
            // 
            // lbMonth
            // 
            this.lbMonth.AccessibleDescription = null;
            this.lbMonth.AccessibleName = null;
            this.lbMonth.AllowGradient = false;
            this.lbMonth.AllowGroup = true;
            resources.ApplyResources(this.lbMonth, "lbMonth");
            this.lbMonth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbMonth.BackgroundImage = null;
            this.lbMonth.Curve = 0;
            this.lbMonth.Font = null;
            this.lbMonth.ForeColor = System.Drawing.Color.White;
            this.lbMonth.Group = 3;
            this.lbMonth.Name = "lbMonth";
            this.lbMonth.TagDouble = null;
            this.lbMonth.UseVisualStyleBackColor = false;
            this.lbMonth.Click += new System.EventHandler(this.lbMonth_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // lbDay
            // 
            this.lbDay.AccessibleDescription = null;
            this.lbDay.AccessibleName = null;
            this.lbDay.AllowGradient = false;
            this.lbDay.AllowGroup = true;
            resources.ApplyResources(this.lbDay, "lbDay");
            this.lbDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbDay.BackgroundImage = null;
            this.lbDay.Curve = 0;
            this.lbDay.Font = null;
            this.lbDay.ForeColor = System.Drawing.Color.White;
            this.lbDay.Group = 3;
            this.lbDay.Name = "lbDay";
            this.lbDay.TagDouble = null;
            this.lbDay.UseVisualStyleBackColor = false;
            this.lbDay.Click += new System.EventHandler(this.lbDay_Click);
            // 
            // plPercentSmall
            // 
            this.plPercentSmall.AccessibleDescription = null;
            this.plPercentSmall.AccessibleName = null;
            resources.ApplyResources(this.plPercentSmall, "plPercentSmall");
            this.plPercentSmall.BackgroundImage = null;
            this.plPercentSmall.Font = null;
            this.plPercentSmall.Name = "plPercentSmall";
            // 
            // SubLeftDisplayData
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = null;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = null;
            this.Name = "SubLeftDisplayData";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private RoundRectControl lbDay;
        private RoundRectControl lbYear;
        private RoundRectControl lbMonth;
        private RoundRectControl lbWeek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel plPercentSmall;
        private RoundRectControl lbCurve;
    }
}
