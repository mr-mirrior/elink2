using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Threading;

namespace Elink
{
    public partial class frmMain : Form
    {
        void Test()
        {
            
            //EnergyData.C.Simulate("PreservedData.2008.9.3 20.45.34.xml");
        }

        public frmMain()
        {
            thiswindow = this;
            JumpWindows.GoBrief += pictureBox1_Click;
            JumpWindows.DoCollectData += btnCollectDataNow_Click;
            JumpWindows.ReloadData += OnReloadData;
//             SetStyle(ControlStyles.OptimizedDoubleBuffer|
//                 ControlStyles.Opaque|
//                 ControlStyles.ResizeRedraw|
//                 ControlStyles.UserPaint|
//                 ControlStyles.AllPaintingInWmPaint, 
//                 true);
            Global.GoMultiLanguage();
            InitializeComponent();
            Initialize();
            settings.OnCarbonChange += OnCarbonChange;
            settings.OnPriceChange += OnPriceChange;
            settings.OnUserNameChange += OnUserNameChange;
            settings.ReadPersonalProfiles();
            settings.OnPriceChange += briefReport.OnCurrencyChange;

            UpdateTitle();

            progressBar1.ForeColor = Color.YellowGreen;
            //EnergyData.C.Simulate("PreservedData.2008.9.1 18.25.1.xml");
            //EnergyData.SaveAllCSVAndXML();

//             this.IsMdiContainer = true;
        }
        private static frmMain thiswindow = null;
        public static frmMain MainWindow { get { return thiswindow; } }
//         public void Cascade()
//         {
//             Form[] frms = this.MdiChildren;
//             foreach (Form f in frms)
//             {
//                 f.TopMost = true;
//                 f.Show();
//             }
//             this.LayoutMdi(MdiLayout.Cascade);
//         }
        private void UpdateTitle()
        {
            this.Text = "eLink " +
    System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() +
    " " +
    System.Threading.Thread.CurrentThread.CurrentUICulture.ToString() +
    " Firmware " +
    Settings.I.firmware_version.ToString("0.00") +
    " - Efergy Technologies Ltd.";

        }
        private void GoSpanish()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-ES");
            ApplyResource();
        }
        private void ApplyResource()
        {
            System.ComponentModel.ComponentResourceManager res =
                new System.ComponentModel.ComponentResourceManager(this.GetType());
            ApplyAll(this, res);
        }
        private void ApplyAll(Control p, System.ComponentModel.ComponentResourceManager res)
        {
            foreach (Control c in p.Controls)
            {
                if( c is TableLayoutPanel )
                {
                    TableLayoutPanel tlp = (TableLayoutPanel)c;
                    
                }
                if (c is Panel ||
                    c is UserControl ||
                    c is TableLayoutPanel ||
                    c is FlowLayoutPanel)
                {
                    System.Diagnostics.Debug.Print(c.Name);
                    ApplyAll(c, res);
                }
                res.ApplyResources(c, c.Name);
            }
        }

        Rectangle rcFrame = new Rectangle(0,0,0,0);
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

//             if (this.ClientRectangle == panel1.ClientRectangle)
//                 return;
//             if (rcFrame.Height == 0)
//                 return;
// 
//             using (Pen p = new Pen(Color.Black))
//                 e.Graphics.DrawRectangle(p, rcFrame);
        }
        SubRightSettings settings = new SubRightSettings();
        UserControl lefttop, right;
        Calendar cal = new Calendar();
        BriefReport briefReport = new BriefReport();
        SubLeftDisplayData leftDispdata = new SubLeftDisplayData();
        DisplayData rightDispdata = new DisplayData();
        PercentageLarge perclarge = new PercentageLarge();
        PercentageSmall percsmall = new PercentageSmall();
        SubRightTariffGames tariffgames = new SubRightTariffGames();
        private void Initialize()
        {
            cal.Parent = this.plCalendar;
            plCalendar.Controls.Add(cal);
            RefreshToday();
            RefreshPercentage();
            GoBrief();
            //GoSettings();
        }
        private void RefreshBrief()
        {
            DateTime now = DateTime.Now;
            float carbon = Settings.I.carbon;
            float daily = EnergyData.QueryGlobalDailyAverage();//EnergyDataNew.avr_daily_e;
            float dailycost = EnergyData.QueryGlobalDailyAverageCost();//EnergyDataNew.avr_daily_c;
            float dailycarbon = EnergyData.QueryGlobalDailyAverageCarbon();//EnergyDataNew.avr_daily_e*Settings.I.carbon;
            briefReport.ENERGY.DailyConsumption = daily;
            briefReport.COSTS.DailyConsumption = dailycost;
            briefReport.CARBON.DailyConsumption = dailycarbon;

            briefReport.ENERGY.EstForTheYear = 365 * daily;
            briefReport.COSTS.EstForTheYear = 365 * dailycost;
            briefReport.CARBON.EstForTheYear = 365 * dailycarbon;

            briefReport.ENERGY.TotalForTheYear = EnergyData.QueryGlobalEnergyYearly(now);//EnergyDataNew.total_thisyear_e;
            briefReport.COSTS.TotalForTheYear = EnergyData.QueryGlobalCostYearly(now);//EnergyDataNew.total_thisyear_c;
            briefReport.CARBON.TotalForTheYear = EnergyData.QueryGlobalEnergyYearly(now) * Settings.I.carbon;//EnergyDataNew.total_thisyear_e * Settings.I.carbon;

            UpdatePrice();
            UpdateTarget();
            //float kwhtotalyear = EnergyData.P.GetBriefThisYearEnergy();
            //float costtotalyear = EnergyData.P.GetBriefYearConsCost(Settings.I.advTariff);
            ////DateTime lastday = new DateTime(DateTime.Today.Year, 12, 31);
            //briefReport.ENERGY.TotalForTheYear = kwhtotalyear;
            //briefReport.COSTS.TotalForTheYear = costtotalyear;
            //briefReport.CARBON.TotalForTheYear = Settings.I.carbon_ratio * kwhtotalyear;

            //float actual_aver = kwh * 365;
            //briefReport.ENERGY.EstForTheYear = actual_aver;
            //briefReport.COSTS.EstForTheYear = cost*365;
            //briefReport.CARBON.EstForTheYear = actual_aver*Settings.I.carbon_ratio;
        }
        private void RefreshDates()
        {
            lbCollectDate.Text = Global.GetString("s1000", "NA");
            lbBackupDate.Text = Global.GetString("s1000", "NA");
            DateTime collect = Settings.I.collect_time;//EnergyDataNew.latest_time_e2.E2Time;
            DateTime backup = Settings.I.backup_time;

            if ( DT.IsStrictlyValid(collect))
            {
                lbCollectDate.Text = string.Format("{0:00}/{1:00}/{2:00}", collect.Day, collect.Month, collect.Year % 100);
            }
            if (DT.IsStrictlyValid(backup)  )
            {
                lbBackupDate.Text = string.Format("{0:00}/{1:00}/{2:00}", backup.Day, backup.Month, backup.Year % 100);
            }

        }
        private void GoSettings()
        {
            if (right == settings)
                return;
            HideAll();
            Show(perclarge, settings);
        }
        private void HideAll()
        {
            if (lefttop != null)
                lefttop.Hide();
            if (right != null)
                right.Hide();
        }
        private void Show(UserControl l, UserControl r)
        {
            //if (l.Visible && r.Visible)
            //    return;

//             if (r == briefReport)
//             {
//                 pictureBox1.Image = Properties.Resources.logo_big_blue;
//             }
//             else
//                 pictureBox1.Image = Properties.Resources.logo_big;

            lefttop = l;
            right = r;

            plLefttop.Controls.Add(l);
            l.Dock = DockStyle.Fill;
            plPage.Controls.Add(r);
            r.Dock = DockStyle.Fill;
            l.Visible = true;
            r.Visible = true;

            if (r != this.rightDispdata)
                cal.ShowAll();
        }
        private void RefreshPercentage()
        {
            //IPercentage.I.Percent = 15;
        }
        private void OnGoBrief(object sender, EventArgs e)
        {
            RefreshBrief();
            GoBrief();
        }
        private void GoBrief()
        {
            if (right == briefReport)
                return;

            HideAll();
            Show(perclarge, briefReport);

            btnSettings.IsDown = btnDisplayData.IsDown = btnTariffGame.IsDown = false;
        }
        private void RefreshToday()
        {
            DateTime t = DateTime.Today;
            lbToday.Text = FormatDate(t);
        }
        static string FormatDate(DateTime t)
        {
//             string s = t.Day.ToString();
//             s += Calendar.DaySuffix(t.Day);
//             s += t.ToString(" MMMM yyyy",  CultureInfo.CreateSpecificCulture("en-US"));
            return t.ToString("d", CultureInfo.CurrentUICulture);
        }

        private void lbDate_Click(object sender, EventArgs e)
        {
            cal.Today();
        }
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        private bool ConfirmQuit()
        {

            if (MB.OKCancelQ(
                GetString("s1001", "Are you sure to quit?")))
                return true;
            else
                return false;
        }
        private void Quit()
        {
            if (ConfirmQuit())
                this.Close();
        }
        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if( e.KeyCode == Keys.Escape )
            {
                Quit();
            }
        }
        //private bool is_dragging = false;

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Quit();
        }
        private void UpdateButtonState()
        {
            btnSettings.IsDown = (right == settings);
            btnDisplayData.IsDown = (right == rightDispdata);
            if( !btnDisplayData.IsDown )
            {
                Calendar.I.ShowAll();
            }
        }
        private void btnSettings_Click(object sender, EventArgs e)
        {
            //if (right != settings)
            //    GoSettings();
            //else
            {
                GoSettings();
                UpdateButtonState();
                //GoBrief();
            }
        }
        private void GoDisplayData()
        {
            if (right == rightDispdata)
                return;

            HideAll();
            Show(leftDispdata, rightDispdata);
            leftDispdata.OnLoad();
        }
        private void btnDisplayData_Click(object sender, EventArgs e)
        {
            GoDisplayData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //if (right != settings)
            GoBrief();
            UpdateButtonState();
        }
        private void UpdateTarget()
        {
            float reality = 0;
            float target = Settings.I.target;
            if( target <= 0 )
            {
                IPercentage.I.Percent = Is.UntouchedF;
                IPercentage.I.InformChange();
                return;
            }
            ReturnFloat[,] thetable = new ReturnFloat[3, 3] {
//                 {EnergyDataNew.AvrDailyEnergy,EnergyDataNew.AvrDailyCost,EnergyDataNew.AvrDailyCarbon},
//                 {EnergyDataNew.AvrWeeklyEnergy,EnergyDataNew.AvrWeeklyCost,EnergyDataNew.AvrWeeklyCarbon},
//                 {EnergyDataNew.AvrMonthlyEnergy,EnergyDataNew.AvrMonthlyCost,EnergyDataNew.AvrMonthlyCarbon}

                {EnergyData.QueryGlobalDailyAverage,EnergyData.QueryGlobalDailyAverageCost,EnergyData.QueryGlobalDailyAverageCarbon},
                {EnergyData.QueryGlobalWeeklyAverage,EnergyData.QueryGlobalWeeklyAverageCost,EnergyData.QueryGlobalWeeklyAverageCarbon},
                {EnergyData.QueryGlobalMonthlyAverage,EnergyData.QueryGlobalMonthlyAverageCost,EnergyData.QueryGlobalMonthlyAverageCarbon}
            };
            string[] w1 = new string[] { "kWh", Settings.I.currency, "kg.CO2"};
            string[] w2 = new string[] { 
                GetString("s1003", "day"), 
                GetString("s1004", "week"), 
                GetString("s1005", "month")};
            int idx1 = Settings.I.target_type;
            int idx2 = Settings.I.target_period;
            ReturnFloat theone = thetable[idx2, idx1];
            reality = theone();
            if (reality == -1)
                reality = float.NaN;
            float percent;
            float diff = reality - target;
            if (diff < 0)
                percent = diff * 100 / reality;
            else
                percent = diff * 100 / target;
            IPercentage.I.real = reality;
            IPercentage.I.target = target;
            IPercentage.I.period = w1[idx1] + 
                GetString("s1006", " per ") 
                + w2[idx2];
            IPercentage.I.Percent = percent;
            IPercentage.I.InformChange();
        }
        private void OnUserNameChange(object sender, EventArgs e)
        {
            UserNameEvent f = e as UserNameEvent;
            lbName.Text = f.Name;

            UpdateTarget();
        }
        string currency = "$";
        private void OnPriceChange(object sender, EventArgs e)
        {
            PriceEvent f = e as PriceEvent;
            if( f.Price == -1 )
            {
                lbAveragePrice.Text = null;
                return;
            }
            currency = f.Currency;
            UpdatePrice();
        }
        private void UpdatePrice()
        {
            float at = EnergyData.QueryAverageTotal();//EnergyDataNew.avr_yuan_per_kwh;
            if (!Is.ValidF(at) || at<0 )
                lbAveragePrice.Text = "N/A";
            else
                lbAveragePrice.Text = string.Format(
                    GetString("s1007", "Electricity average cost: {0}{1}/kWh"), 
                    at.ToString("0.00"), currency);
        }
        private void OnCarbonChange(object sender, EventArgs e)
        {
            CarbonEvent f = e as CarbonEvent;
            if( f.Carbon == -1 )
            {
                lbCarbonRatio.Text = null;
                return;
            }
            lbCarbonRatio.Text = string.Format(
                GetString("s1008", "Carbon ratio: {0} kg.CO2/kWh"), 
                f.Carbon.ToString("0.00"));
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            FileInfo fi = new FileInfo("elinkmanual.pdf");
            if(!fi.Exists )
            {
                MB.Warning("User Manual file not found.");
                return;
            }
            if (!MB.OKCancelQ(
                GetString("s1009", "You will be directed to read a USER MANUAL, are you sure?")))
                return;
            {
                string curdir = System.IO.Directory.GetCurrentDirectory();
                Help.ShowHelp(this, @"file:///" + curdir + "/elinkmanual.pdf");
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.logo_big_blue;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.logo_big;
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            Rectangle rc = ClientRectangle;
            Rectangle rcPanel = panel1.ClientRectangle;

            int x, y;
            x = rc.Width - rcPanel.Width;
            y = rc.Height - rcPanel.Height;
            x /= 2;
            y /= 2;

            panel1.Location = new Point(x, y);

            rcFrame = panel1.ClientRectangle;
            rcFrame.Offset(panel1.Location);
            rcFrame.Inflate(1, 1);
            Refresh();
        }
        private void MsgWarningPortError()
        {
            MB.Error(
                //GetString("s1010", "Port cannot be opened, maybe in use or no device connected.\n") +
                GetString("s1011", "Port cannot be opened, maybe in use or no device connected.\nPlease check the device USB connection and try again."));
        }
        //frmProgress progDlg = null;
        Serial.SerialCOM sc = null;
        private bool StartCollectData()
        {
            tbStatus.Items.Clear();
            tbStatus.BringToFront();
            tbStatus.Visible = true;
            sc = Serial.SerialCOM.Probe(OnStatus);
            tbStatus.Visible = false;
            if (sc == null)
            {
                MsgWarningPortError();
                return false;
            }

            //sc = new Elink.Serial.SerialCOM("COM5");
            sc.InProgress -= OnProgress;
            sc.InStep -= OnStep;
            sc.FinishTransmission -= OnFinish;
            sc.InProgress += OnProgress;
            sc.InStep += OnStep;
            sc.FinishTransmission += OnFinish;
            if (!sc.Open())
            {
                MsgWarningPortError();
                return false;
            }
            progressBar1.Visible = true;
            lbName.Visible = lbAveragePrice.Visible = lbCarbonRatio.Visible = false;
            btnCollectDataNow.Enabled = false;
            //btnDisplayData.Enabled = false;
            sc.CollectData();
            return true;
        }
        private void OnStatus(object sender, EventArgs e)
        {
            Serial.StatusEvent f = (Serial.StatusEvent)e;
            //tbStatus.Text += f.status + "\r\n";
            //tbStatus.Select(tbStatus.Text.Length-2, 1);
            int idx = tbStatus.Items.Add(f.status);
            tbStatus.SelectedIndex = idx;
        }
        private void btnCollectDataNow_Click(object sender, EventArgs e)
        {
            //pictureBox1_Click(sender, e);
            StartCollectData();
//             if (DialogResult.Yes != MessageBox.Show(
//                 "This operation may take up to 30 seconds to complete.\nAre you sure to do it now?", "Confirm",
//                 MessageBoxButtons.YesNo, MessageBoxIcon.Question))
//             {
//                 return;
//             }
//             progDlg = new frmProgress();
//             BoolInvokeDelegate d = new BoolInvokeDelegate(delegate() { return StartCollectData(); });
//             progDlg.AutoRun(d);
//             //progDlg.Parent = this;
//             DialogResult result = progDlg.ShowDialog();
//             progDlg.Dispose();
//             progDlg = null;
//             if (result == DialogResult.Cancel)
//             {
//                 sc.Cancel();
//                 System.Threading.Thread.Sleep(1000);
//             }
//             sc.Dispose();
//             sc = null;
//             if (result != DialogResult.Cancel &&
//                 result != DialogResult.Abort)
//             {
//                 MessageBox.Show("Data collection done successfully!",
//                     "Successful",
//                     MessageBoxButtons.OK,
//                     MessageBoxIcon.Information);
//             }
        }
        void SetProgress(int value)
        {
            progressBar1.Value = value;
        }
        private void OnProgress(object sender, EventArgs e)
        {
            Serial.ProgressEvent f = (Serial.ProgressEvent)e;
            //progressBar1.Value = f.pos * 100 / f.count;
            this.BeginInvoke(new InvokeDelegateInt(SetProgress), f.pos * 100 / f.count);           
            
            //if (progDlg == null)
            //    return;
            //progDlg.Position = f.pos * 100 / f.count;

        }
        private void OnStep(object sender, EventArgs e)
        {
            //Serial.StepEvent f = (Serial.StepEvent)e;
            //if (progDlg == null)
            //    return;
            //progDlg.Caption = string.Format("Step {0} of {1} in progress...", f.step, f.count);
        }
        private void OnReloadData(object sender, EventArgs e)
        {
            ReloadAllData();
        }
        private void OnFinish(object sender, EventArgs e)
        {
            InvokeDelegate d = delegate()
            {
                progressBar1.Visible = false;
                lbName.Visible = lbAveragePrice.Visible = lbCarbonRatio.Visible = true;
                System.Diagnostics.Debug.Print("Finished, disconnecting...");
                sc.Disconnect();
                sc.Dispose();
                sc = null;
                btnCollectDataNow.Enabled = true;
                // Errors in collect data
                if( sender == null )
                {
                    return;
                }

                //btnDisplayData.Enabled = true;
                Settings.I.firmware_version = EnergyData.devdata.firmware_version;
                Settings.I.collect_time = EnergyData.devdata.now_in_PC;
                Settings.I.Save();
//                 EnergyDataNew.Merge();
//                 EnergyDataNew.Save();
                EnergyData.C.Merge();
                EnergyData.SaveAllCSVAndXML();
                EnergyData.C.SavePreservedData();
                ReloadAllData();

                MB.OKI(
                    GetString("s1012", "Data has been successfully collected!")); 
                //    +
                //GetString("s1010", "*** Please now pull out the USB cable ***\n")+
                //GetString("s1010", "to make sure the device continues receiving adv pointer the wireless transmitter again."));

                //Application.Restart();
            };
            this.BeginInvoke(d);
            //if (progDlg != null)
            //{
            //    progDlg.Finish();
            //}
            //progDlg.Caption = "Finishing...";

        }
        private void ReloadAllData()
        {
            Settings.Load();
            EnergyData.Load();
            RefreshDates();
            RefreshBrief();
            UpdateTitle();
        }
        private void btnTariffGame_Click(object sender, EventArgs e)
        {
            HideAll();
            Show(perclarge, tariffgames);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            // countryprofiles.xml
            // config.xml
            // repository folder

            BackupRestore br = new BackupRestore();
            DialogResult r = br.ShowDialog();
            if (r == DialogResult.Cancel)
                return;
            if (r == DialogResult.OK)
            {
                DirectoryInfo di = new DirectoryInfo(EnergyData.REPOSITORY_PATH);
                if (!di.Exists)
                {
                    MB.Warning(
                        GetString("s1013", "There is no data to backup."));
                    return;
                }

                FolderBrowserDialog fd = new FolderBrowserDialog();
                fd.ShowNewFolderButton = true;
                fd.Description = 
                    GetString("s1014", "Please select backup destination folder:");
                if (fd.ShowDialog() != DialogResult.OK)
                    return;

                int count = 0;
                try
                {
                    FileInfo fi = new FileInfo("CountryProfiles.xml");
                    if (fi.Exists)
                    {
                        fi.CopyTo(Path.Combine(fd.SelectedPath, fi.Name), true);
                        count++;
                    }
                    fi = new FileInfo("Config.xml");
                    if (fi.Exists)
                    {
                        fi.CopyTo(Path.Combine(fd.SelectedPath, fi.Name), true);
                        count++;
                    }
                    string path = Path.Combine(fd.SelectedPath, EnergyData.REPOSITORY_PATH);
                    DirectoryInfo diTarget = new DirectoryInfo(path);
                    if (diTarget.Exists)
                    {
                        diTarget.Delete(true);
                        //diTarget.Create();
                    }
                    count += FileIO.DirectoryCopy(di.FullName, diTarget.FullName, false, true);
                }
                catch(Exception)
                {
                    
                    count = 0;
                }

                if (count != 0)
                {
                    Settings.I.backup_time = DateTime.Now;
                    Settings.I.Save();
                    RefreshDates();

                    MB.OKI(string.Format(
                        GetString("s1015", "Backup finished. {0} files copied."),
                        count));
                }
                else
                    MB.Warning(
                        GetString("s1016", "No files copied, is destination folder writable?"));
            }
            else // restore
            {
                FolderBrowserDialog fd = new FolderBrowserDialog();
                fd.ShowNewFolderButton = true;
                fd.Description = 
                    GetString("s1017", "Please select backup source folder:");
                if (fd.ShowDialog() != DialogResult.OK)
                    return;
                DirectoryInfo diSource = new DirectoryInfo(fd.SelectedPath);

                if (!diSource.Exists)
                {
                    MB.Warning(GetString("s1018", "Source folder does not exist."));
                    return;
                }

                int count = 0;
                try
                {
                    string curdir = Directory.GetCurrentDirectory();
                    FileInfo fi = new FileInfo(Path.Combine(fd.SelectedPath,"CountryProfiles.xml"));
                    fi.CopyTo(Path.Combine(curdir, fi.Name) ,true);
                    count++;

                    fi = new FileInfo(Path.Combine(fd.SelectedPath, "Config.xml"));
                    fi.CopyTo(Path.Combine(curdir, fi.Name), true);
                    count++;

                    diSource = new DirectoryInfo(Path.Combine(diSource.FullName, ("Repository")));
                    if (diSource.Exists)
                    {
                        DirectoryInfo diTarget = new DirectoryInfo("Repository");
                        if (diTarget.Exists)
                            diTarget.Delete(true);
                        count += FileIO.DirectoryCopy(diSource.FullName,
                            diTarget.FullName, true, true);
                    }
                }
                catch
                {
                    count = 0;
                }
                if( count != 0 )
                {
                    MB.OKI(string.Format(
                        GetString("s1019", "Restore finished. {0} files copied"), count));
                    ReloadAllData();
                }
                else
                {
                    MB.OKI(GetString("s1020", "No file restored, source folder empty?"));
                }

            }
//             SaveFileDialog fd = new SaveFileDialog();
//             fd.OverwritePrompt = true;
//             fd.Filter = "CSV File|*.csv";
//             fd.DefaultExt = "csv";
//             if (fd.ShowDialog() != DialogResult.OK)
//                 return;
//             Settings.I.backup_time = DateTime.Now;
//             EnergyData.Backup(fd.FileName);
//             settings.SavePersonalInfo();
//             RefreshDates();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "http://www.efergy.com");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            EnergyData.Load();
//             if (!EnergyData.Load())
//                 MB.Warning(
//                     GetString("s1021", "Energy data not found, you may not see the graphs."+"\n" +
//                     GetString("s1022", "Please connect E2 to PC and then click CollectData button")));
            Test();
            RefreshDates();
            RefreshBrief();
            logo_read();

            //GoSpanish();
        }
        public void logo_read()
        {
            string imagestr = "mylogo.png";
            string tipsadress = "logotips.txt";
            string tipslink = "logolink.txt";
            if (File.Exists(imagestr) && File.Exists(tipsadress)&&File.Exists(tipslink))
            {
                Image logo = Image.FromFile(imagestr);
                if (logo == null)
                {
                    MB.Warning("Fail in loading the logo.");
                    pictureBox2.Image = null;
                }
                else
                {
                    pictureBox2.Image = logo;
                    StreamReader tips = new StreamReader(tipsadress);
                    string s = tips.ReadLine();
                    if (s == null) 
                    { 
                        MB.Warning("Failed in loading.");
                    }
                    else if (s.Length < 100)
                    {
                        toolTip1.SetToolTip(pictureBox2, s);
                    }
                    else
                    {
                        MB.Warning("There are too many words in tips.");
                        this.Close();
                    }
                }
            }
            else { return; }
            int intWidth = pictureBox2.Image.Width;
            int intHeight = pictureBox2.Image.Height;
            if ((intWidth != 48) || (intHeight != 48))
            {
                MB.Warning("Please change the size of the logo into 48x48.");
                pictureBox2.Image = null;
            }
        }
            
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            string imagestr = "mylogo.png";
            string tipsadress = "logotips.txt";
            string tipslink = "logolink.txt";
            if (File.Exists(imagestr) && File.Exists(tipsadress) && File.Exists(tipslink))
            {
                StreamReader link = new StreamReader(tipslink);
                string s = link.ReadLine();
                if (s == null)
                {
                    MB.Warning("Failed in loading.");
                }
                else
                {
                    Help.ShowHelp(this, s);
                }
            }
            else
            {
                Help.ShowHelp(this, "http://www.efergy.com");
            }
            
        }
        
        void ShowWizard()
        {
            GoSettings();
            settings.RunWizard();
        }
        private void frmMain_Shown(object sender, EventArgs e)
        {
            if(EnergyData.Empty())
            {
                MB.Info( 
                    GetString("s1021", "It is the first time to run this software")+"\n"+
                    GetString("s1022", "Please take a few minutes to setup the initial tariff settings")+"\n"+"\n"+
                    GetString("s1023", "Press OK to proceed"),
                    GetString("s1024", "Welcome!"),
                    MessageBoxIcon.Information);
                
                ShowWizard();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
//             if (!this.ConfirmQuit())
//                 e.Cancel = true;
        }
    }
    public class eColor
    {
        public static Color MyBlue = Color.FromArgb(0, 98, 170);
        public static Color NA = Color.Gray;
    }

    public static class JumpWindows
    {
        public static event EventHandler GoBrief;
        public static event EventHandler GoTime;
        public static event EventHandler DoCollectData;
        public static event EventHandler ReloadData;
        public static void TriggerReloadData()
        {
            if (ReloadData != null)
                ReloadData.Invoke(null, null);
        }
        public static void TriggerGoBrief()
        {
            if (GoBrief != null)
                GoBrief.Invoke(null, null);
        }
        public static void TriggerGoTime(DateTime t)
        {
            if (GoTime != null)
                GoTime.Invoke(null, new TimeEvent(t));
        }
        public static void TriggerDoCollectData()
        {
            if (DoCollectData != null)
                DoCollectData.Invoke(null, null);
        }
        public class TimeEvent: EventArgs
        {
            public DateTime time;
            public TimeEvent(DateTime t) { time = t; }
        }
    }
    public delegate float ReturnFloat();

}
