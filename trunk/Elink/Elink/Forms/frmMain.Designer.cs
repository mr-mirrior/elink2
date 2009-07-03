using System.Windows.Forms;
namespace Elink
{
    partial class frmMain
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lbToday = new Elink.RoundRectControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.plLefttop = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbBackupDate = new System.Windows.Forms.Label();
            this.lbCollectDate = new System.Windows.Forms.Label();
            this.btnDownload = new Elink.RoundRectControl();
            this.btnTariffGame = new Elink.RoundRectControl();
            this.lbLastCollectDate = new System.Windows.Forms.Label();
            this.lbLastUpdateDate = new System.Windows.Forms.Label();
            this.btnCollectDataNow = new Elink.RoundRectControl();
            this.btnQuit = new Elink.RoundRectControl();
            this.btnHelp = new Elink.RoundRectControl();
            this.btnDisplayData = new Elink.RoundRectControl();
            this.btnSettings = new Elink.RoundRectControl();
            this.lbCarbonRatio = new System.Windows.Forms.Label();
            this.lbAveragePrice = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.lbEfergyE2 = new Elink.RoundRectControl();
            this.lbNumberOfDays = new Elink.RoundRectControl();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbStatus = new System.Windows.Forms.ListBox();
            this.plPage = new System.Windows.Forms.Panel();
            this.plCalendar = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbToday
            // 
            this.lbToday.AllowGradient = false;
            this.lbToday.AllowGroup = false;
            this.lbToday.BackColor = System.Drawing.Color.Gainsboro;
            this.lbToday.Curve = 0;
            resources.ApplyResources(this.lbToday, "lbToday");
            this.lbToday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbToday.IsHoverEnabled = false;
            this.lbToday.Name = "lbToday";
            this.lbToday.TabStop = false;
            this.lbToday.TagDouble = null;
            this.toolTip1.SetToolTip(this.lbToday, resources.GetString("lbToday.ToolTip"));
            this.lbToday.UseVisualStyleBackColor = false;
            this.lbToday.Click += new System.EventHandler(this.lbDate_Click);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Image = global::Elink.Properties.Resources.logo_big;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox1, resources.GetString("pictureBox1.ToolTip"));
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::Elink.Properties.Resources.logo_small;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBox2, resources.GetString("pictureBox2.ToolTip"));
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click_1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.plPage, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.plCalendar, 0, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lbToday);
            this.panel2.Controls.Add(this.plLefttop);
            this.panel2.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 2);
            // 
            // plLefttop
            // 
            resources.ApplyResources(this.plLefttop, "plLefttop");
            this.plLefttop.Name = "plLefttop";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbBackupDate);
            this.panel3.Controls.Add(this.lbCollectDate);
            this.panel3.Controls.Add(this.btnDownload);
            this.panel3.Controls.Add(this.btnTariffGame);
            this.panel3.Controls.Add(this.lbLastCollectDate);
            this.panel3.Controls.Add(this.lbLastUpdateDate);
            this.panel3.Controls.Add(this.btnCollectDataNow);
            this.panel3.Controls.Add(this.btnQuit);
            this.panel3.Controls.Add(this.btnHelp);
            this.panel3.Controls.Add(this.btnDisplayData);
            this.panel3.Controls.Add(this.btnSettings);
            this.panel3.Controls.Add(this.lbCarbonRatio);
            this.panel3.Controls.Add(this.lbAveragePrice);
            this.panel3.Controls.Add(this.lbName);
            this.panel3.Controls.Add(this.lbEfergyE2);
            this.panel3.Controls.Add(this.lbNumberOfDays);
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Controls.Add(this.tbStatus);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // lbBackupDate
            // 
            this.lbBackupDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.lbBackupDate, "lbBackupDate");
            this.lbBackupDate.Name = "lbBackupDate";
            // 
            // lbCollectDate
            // 
            this.lbCollectDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.lbCollectDate, "lbCollectDate");
            this.lbCollectDate.Name = "lbCollectDate";
            // 
            // btnDownload
            // 
            this.btnDownload.AllowGradient = false;
            this.btnDownload.AllowGroup = false;
            this.btnDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnDownload.Curve = 0;
            this.btnDownload.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnDownload, "btnDownload");
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.TagDouble = null;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnTariffGame
            // 
            this.btnTariffGame.AllowGradient = false;
            this.btnTariffGame.AllowGroup = true;
            this.btnTariffGame.BackColor = System.Drawing.Color.Gray;
            this.btnTariffGame.Curve = 0;
            this.btnTariffGame.ForeColor = System.Drawing.Color.White;
            this.btnTariffGame.Group = 1;
            resources.ApplyResources(this.btnTariffGame, "btnTariffGame");
            this.btnTariffGame.Name = "btnTariffGame";
            this.btnTariffGame.TagDouble = null;
            this.btnTariffGame.UseVisualStyleBackColor = false;
            this.btnTariffGame.Click += new System.EventHandler(this.btnTariffGame_Click);
            // 
            // lbLastCollectDate
            // 
            this.lbLastCollectDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.lbLastCollectDate, "lbLastCollectDate");
            this.lbLastCollectDate.Name = "lbLastCollectDate";
            // 
            // lbLastUpdateDate
            // 
            this.lbLastUpdateDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            resources.ApplyResources(this.lbLastUpdateDate, "lbLastUpdateDate");
            this.lbLastUpdateDate.Name = "lbLastUpdateDate";
            // 
            // btnCollectDataNow
            // 
            this.btnCollectDataNow.AllowGradient = false;
            this.btnCollectDataNow.AllowGroup = false;
            this.btnCollectDataNow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCollectDataNow.Curve = 0;
            this.btnCollectDataNow.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnCollectDataNow, "btnCollectDataNow");
            this.btnCollectDataNow.Name = "btnCollectDataNow";
            this.btnCollectDataNow.TagDouble = null;
            this.btnCollectDataNow.UseVisualStyleBackColor = false;
            this.btnCollectDataNow.Click += new System.EventHandler(this.btnCollectDataNow_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.AllowGradient = false;
            this.btnQuit.AllowGroup = false;
            this.btnQuit.BackColor = System.Drawing.Color.Gray;
            this.btnQuit.Curve = 0;
            this.btnQuit.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnQuit.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnQuit, "btnQuit");
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.TagDouble = null;
            this.btnQuit.UseVisualStyleBackColor = false;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.AllowGradient = false;
            this.btnHelp.AllowGroup = false;
            this.btnHelp.BackColor = System.Drawing.Color.Gray;
            this.btnHelp.Curve = 0;
            this.btnHelp.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.btnHelp, "btnHelp");
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.TagDouble = null;
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnDisplayData
            // 
            this.btnDisplayData.AllowGradient = false;
            this.btnDisplayData.AllowGroup = true;
            this.btnDisplayData.BackColor = System.Drawing.Color.Gray;
            this.btnDisplayData.Curve = 0;
            this.btnDisplayData.ForeColor = System.Drawing.Color.White;
            this.btnDisplayData.Group = 1;
            resources.ApplyResources(this.btnDisplayData, "btnDisplayData");
            this.btnDisplayData.Name = "btnDisplayData";
            this.btnDisplayData.TagDouble = null;
            this.btnDisplayData.UseVisualStyleBackColor = false;
            this.btnDisplayData.Click += new System.EventHandler(this.btnDisplayData_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.AllowGradient = false;
            this.btnSettings.AllowGroup = true;
            this.btnSettings.BackColor = System.Drawing.Color.Gray;
            this.btnSettings.Curve = 0;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Group = 1;
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.TagDouble = null;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // lbCarbonRatio
            // 
            this.lbCarbonRatio.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbCarbonRatio, "lbCarbonRatio");
            this.lbCarbonRatio.Name = "lbCarbonRatio";
            // 
            // lbAveragePrice
            // 
            this.lbAveragePrice.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbAveragePrice, "lbAveragePrice");
            this.lbAveragePrice.Name = "lbAveragePrice";
            // 
            // lbName
            // 
            this.lbName.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.lbName, "lbName");
            this.lbName.Name = "lbName";
            // 
            // lbEfergyE2
            // 
            this.lbEfergyE2.AllowGradient = false;
            this.lbEfergyE2.AllowGroup = false;
            this.lbEfergyE2.BackColor = System.Drawing.Color.Gainsboro;
            this.lbEfergyE2.Curve = 0;
            this.lbEfergyE2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbEfergyE2.IsHoverEnabled = false;
            resources.ApplyResources(this.lbEfergyE2, "lbEfergyE2");
            this.lbEfergyE2.Name = "lbEfergyE2";
            this.lbEfergyE2.TabStop = false;
            this.lbEfergyE2.TagDouble = null;
            this.lbEfergyE2.UseVisualStyleBackColor = false;
            // 
            // lbNumberOfDays
            // 
            this.lbNumberOfDays.AllowGradient = false;
            this.lbNumberOfDays.AllowGroup = false;
            this.lbNumberOfDays.BackColor = System.Drawing.Color.Gainsboro;
            this.lbNumberOfDays.Curve = 0;
            this.lbNumberOfDays.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbNumberOfDays.IsHoverEnabled = false;
            resources.ApplyResources(this.lbNumberOfDays, "lbNumberOfDays");
            this.lbNumberOfDays.Name = "lbNumberOfDays";
            this.lbNumberOfDays.TabStop = false;
            this.lbNumberOfDays.TagDouble = null;
            this.lbNumberOfDays.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.YellowGreen;
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.Value = 35;
            // 
            // tbStatus
            // 
            this.tbStatus.FormattingEnabled = true;
            resources.ApplyResources(this.tbStatus, "tbStatus");
            this.tbStatus.Name = "tbStatus";
            // 
            // plPage
            // 
            resources.ApplyResources(this.plPage, "plPage");
            this.plPage.Name = "plPage";
            this.tableLayoutPanel1.SetRowSpan(this.plPage, 3);
            // 
            // plCalendar
            // 
            resources.ApplyResources(this.plCalendar, "plCalendar");
            this.plCalendar.Name = "plCalendar";
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnQuit;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private RoundRectControl lbToday;
        private System.Windows.Forms.Panel plLefttop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private RoundRectControl lbEfergyE2;
        private Label lbLastCollectDate;
        private RoundRectControl lbNumberOfDays;
        private Label lbLastUpdateDate;
        private RoundRectControl btnCollectDataNow;
        private RoundRectControl btnQuit;
        private RoundRectControl btnHelp;
        private RoundRectControl btnDisplayData;
        private RoundRectControl btnSettings;
        private System.Windows.Forms.Label lbCarbonRatio;
        private System.Windows.Forms.Label lbAveragePrice;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Panel plPage;
        private System.Windows.Forms.Panel plCalendar;
        private RoundRectControl btnTariffGame;
        private RoundRectControl btnDownload;
        private ProgressBar progressBar1;
        private Label lbBackupDate;
        private Label lbCollectDate;
        private ListBox tbStatus;
        private PictureBox pictureBox2;
    }
}

