using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Elink
{
    public partial class SubRightTariffGames : UserControl
    {
        public SubRightTariffGames()
        {
            Global.GoMultiLanguage();
            InitializeComponent();
            path = Path.GetFullPath(EnergyData.REPOSITORY_PATH);

            tariff = advTariff;
            LoadPage();

        }
        public bool Is1Focused { get { return plImport.Visible; } }
        private void UpdateTariffs(string focus)
        {
            //cbTariffs.DataSource = null;
            Tariff[] tariffs = EnergyData.Tariffs();
            if (tariffs == null)
            {
                this.Cursor = Cursors.Default; 
                return;
            }
            if (tariffs != null)
                if (tariffs.Length != 0)
                {
                    cbTariffs.DataSource = tariffs;
                    //cbTariffs.DisplayMember = "tariff_name";
                    if( focus == null )
                        cbTariffs.SelectedIndex = 0;
                    else
                    {
                        foreach(Tariff t in tariffs)
                        {
                            if(t.SameName(focus))
                            {
                                cbTariffs.SelectedItem = t;
                                break;
                            }
                        }
                    }
                }
            
            DateTime dt = DateTime.Now;
            string strdt = string.Format("{0:00}/{1:00}/{2:00}", dt.Day, dt.Month, dt.Year%100);

            lvTariffs.Items.Clear();
            for (int i = 0; i < tariffs.Length; i++)
            {
//                 Tariff t = Tariff.LoadTariffXML(tariffs[i].FullName);
//                 string s = "EVER";
//                 if (t.last_update != DateTime.MinValue)
                Tariff t = tariffs[i];
                string s = string.Format("{0:00}/{1:00}/{2:00}", t.last_update.Day, t.last_update.Month,  t.last_update.Year % 100); /*t.last_update.ToString("D");*/
                string act = "";
                if (t.tariff_name == null)
                    continue;
                if (t.tariff_name.Equals(Settings.I.advTariff.tariff_name, StringComparison.OrdinalIgnoreCase))
                    act = "ACT";
                ListViewItem lvi = new ListViewItem(
                    new string[] { s, t.tariff_name,act });
                lvi.Tag = t;
                lvTariffs.Items.Add(lvi);
            }

            // List<EnergyData> lst = EnergyData.periods;
            List<EnergyData> lst = EnergyData.history;
            lvDatasheets.Items.Clear();
            if( lst != null )
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    EnergyData e = lst[i];
//                     List<int> years = e.valid_years;
//                     List<DateTime> months = e.valid_months;
                    List<EnergyData> yearly = e.YearlyCopies();
                    List<EnergyData> monthly = e.MonthlyCopies();
                    for (int j = 0; j < yearly.Count; j++)
                    {
                        string date = yearly[j].valid_until.E2Time.Year.ToString();
                        string tariff = e.TariffName();
                        string total = yearly[j].total_kwh.ToString("0.00");
                        ListViewItem lvi = new ListViewItem(new string[] { date, tariff, total });
                        lvi.Tag = yearly[j];
                        // 如果数据重新调入，这里必须得到通知
                        lvDatasheets.Items.Add(lvi);
                    }
                    for (int j = 0; j < monthly.Count; j++)
                    {
                        string date = string.Format("{0:00}/{1:00}",  monthly[j].valid_until.E2Time.Month, monthly[j].valid_until.E2Time.Year % 100);/* monthly[j].valid_until.E2Time.ToString("Y");*/
                        string tariff = e.TariffName();
                        string total = monthly[j].total_kwh.ToString("0.00");
                        ListViewItem lvi = new ListViewItem(new string[] { date, tariff, total });
                        lvi.Tag = monthly[j];
                        // 如果数据重新调入，这里必须得到通知
                        lvDatasheets.Items.Add(lvi);
                    }
                }
            }
            EnergyData.OnChange -= OnEnergyDataChange;
            EnergyData.OnChange += OnEnergyDataChange;

            this.Cursor = Cursors.Default;
        }
        private void OnEnergyDataChange(object sender, EventArgs e)
        {
            UpdateTariffs(null);
        }
        private void LoadPage()
        {
            //Focus1();
            Focus2();
            Tariff data = new Tariff();

            if( Global.IsSpanish() )
            {
                //advTariff.SetData(data);
                plAdvTariffs.Controls.Add(advTariff);
                tariff = advTariff;
            }
            else
            {
                plAdvTariffs.Controls.Add(engTariff);
                tariff = engTariff;
            }
            tariff.SetData(data);
        }
        Color focuscolor = Color.FromArgb(0, 98, 170);
        Color nonfocuscolor = Color.Gray;
        private void Focus1()
        {
            plCompare.Visible = false;
            btn21x.Visible = btn23x.Visible = false;
            btn21.Visible = btn23.Visible = true;
            btn21.BackColor = focuscolor;
            btn21.RightBackBackColor = btn23.BackColor = Color.Silver;
            advTariff.AllbackColor = focuscolor;
            plImport.Visible = true;
            plImport.BringToFront();
        }
        private void Focus2()
        {
            plImport.Visible = false;
            btn21x.Visible = btn23x.Visible = true;
            btn21.Visible = btn23.Visible = false;
            btn23x.BackColor = focuscolor;
            btn23x.BackBackColor = btn21x.BackColor = Color.Silver;
            plCompare.Visible = true;
            plCompare.BringToFront();
        }
        private void Unfocus1()
        {
        }
        private void Unfocus2()
        {
        }

        private void btn1_MouseEnter(object sender, EventArgs e)
        {
            if(btn1.BackColor != focuscolor )
                Focus1();
        }

        private void btn1_MouseLeave(object sender, EventArgs e)
        {
            if( btn1.BackColor != nonfocuscolor )
                Unfocus1();
        }

        private void btn21_MouseEnter(object sender, EventArgs e)
        {
            if( plPatch.BackColor != focuscolor )
                Focus2();
        }

        private void btn21_MouseLeave(object sender, EventArgs e)
        {
            if( plPatch.BackColor != nonfocuscolor )
                Unfocus2();
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            //GoTariff(dualtariff);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            //GoTariff(advancedtariff);
            Focus2();
        }

        private void btn21_Click(object sender, EventArgs e)
        {
            //GoTariff(singletariff);
            Focus1();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
        }
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        TariffAdvanced advTariff = new TariffAdvanced();
        TariffScheme engTariff = new TariffScheme();
        ITariffEmb tariff = null;

        string path;
        private void roundRectControl1_Click(object sender, EventArgs e)
        {
            if (advTariff == null && engTariff == null)
                return;

            if (cbTariffs.Text.Length == 0 || cbTariffs.Text[0]=='.')
            {
                MB.Warning(
                    GetString("s7008", "Please input Tariff ID"));
                return;
            }
            
            // 要覆盖原有的tariff吗？
            if (cbTariffs.SelectedItem != null)
            {
                if(! MB.YesNoQ(
                   GetString("s7000", "Are you sure to update the existing tariff? " + cbTariffs.Text)
                   /*GetString("s7001", "Update existing tariff")*/) )
                    return;
            }

            string editing = cbTariffs.Text;
            //advTariff.TariffName = editing;
            //advTariff.UpdateTime = DateTime.Now;
            tariff.TariffName = editing;
            tariff.UpdateTime = DateTime.Now;

            string file = Path.Combine(path, editing);//path + "\\" + cbTariffs.Text;
            file = Path.ChangeExtension(file, "xml");
//             if (!file.Contains(".xml"))
//                 file += ".xml";
            //advTariff.SaveToXML(file);
            tariff.SaveToXML(file);

            FileInfo fi = new FileInfo(file);
            if (fi.Exists)
            {
                JumpWindows.TriggerReloadData();
                UpdateTariffs(editing);
                MB.OKI(GetString("s7009", "Tariff is saved."));
            }
        }
        Timer t = new Timer();
        private void OnTimer(object sender, EventArgs e)
        {
            t.Stop();
            this.BeginInvoke(new InvokeDelegate(delegate() { UpdateTariffs(null); }));
        }
        private void SubRightTariffGames_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            t.Interval = 500;
            t.Tick += OnTimer;
            t.Start();
        }

        private void cbTariffs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTariffs.SelectedItem == null)
                return;
            Tariff tf = (Tariff)cbTariffs.SelectedItem;
            //advTariff.SetData(tf);
            //advTariff.LoadPage();
            tariff.SetData(tf);
            tariff.LoadPage();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckFileExists = true;
            fd.CheckPathExists = true;
            fd.DefaultExt = ".xml";
            fd.Filter = GetString("s7007","Tariff ID. (*.xml)|*.xml|All Files (*.*)|*.*");
            fd.Multiselect = false;

            if (fd.ShowDialog() != DialogResult.OK)
                return;

            FileInfo fi = new FileInfo(fd.FileName);
            if( fi.Exists )
            {
                FileInfo fi1 = fi.CopyTo(path + "\\" + fi.Name, true);
                if(fi1.Exists)
                {
                    MB.OKI(
                        GetString("s7006","Tariff is imported."));
                }
            }
            UpdateTariffs(cbTariffs.Text);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            UpdateTariffs(null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string file;
            string cannotbedeleted = "Tariff {0} is belonging to an energy cost, which cannot be deleted.";
//             if( plImport.Visible )
//             {
//                 if (lvTariffs.SelectedItems.Count == 0)
//                     return;
//                 Tariff t = (Tariff)lvTariffs.SelectedItems[0].Tag;
//                 //file = t.tariff_name
//             }    
            if (!MB.YesNoQ(
                GetString("s7002","Are you sure to delete the selected tariff?")
                /*GetString("s7003", "Confirm")*/))
                return;
            bool success = true;

            if (plImport.Visible) // IMPORT DIALOG
            {
                FileInfo fi = (FileInfo)cbTariffs.SelectedItem;
                file = fi.Name;
                if (fi == null)
                    return;
                if (!fi.Exists)
                    return;
                Tariff t = new Tariff();
                t.tariff_name = fi.Name;
                if (!t.Delete())
                {
                    success = false;
                    MB.Warning(string.Format(
                        GetString("s8010", cannotbedeleted),
                        t.tariff_name));
                    return;
                }

            }
            else // COMPARE DIALOG
            {
                if( lvTariffs.CheckedItems.Count != 0 )
                {
                    ListView.CheckedListViewItemCollection cks = lvTariffs.CheckedItems;
                    for (int i = 0; i < cks.Count; i++ )
                    {
                        object tag = cks[i].Tag;
                        if (tag == null)
                            continue;
                        Tariff t = (Tariff)tag;
                        if( !t.Delete() )
                        {
                            success = false;
                            MB.Warning(string.Format(
                                GetString("s8010", cannotbedeleted), 
                                t.tariff_name));
                        }
                    }
                }
                else
                if( lvTariffs.SelectedItems.Count != 0 )
                {
                    object tag = lvTariffs.SelectedItems[0].Tag;
                    if (tag == null)
                        return;
                    Tariff t = (Tariff)tag;
                    if( !t.Delete() )
                    {
                        success = false;
                        MB.Warning(string.Format(
                            GetString("s8010", cannotbedeleted),
                            t.tariff_name));
                    }
                }
            }
            UpdateTariffs(null);
            if( success )
                MB.OKI(
                GetString("s7004", "Tariff(s) deleted") );
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            JumpWindows.TriggerGoBrief();
        }

        private void ShowMonthly(EnergyData real, EnergyData simulation)
        {
            frmMonthlyCompare mc = new frmMonthlyCompare(real, simulation);
            //mc.Owner = frmMain.MainWindow;
            mc.Show(this);
            months.Add(mc);
            //mc.MdiParent = frmMain.MainWindow;
            
        }
        private void ShowYearly(EnergyData real, EnergyData simulation)
        {
            frmYearlyCompare yc = new frmYearlyCompare(real, simulation);
            //yc.Owner = frmMain.MainWindow;
            yc.Show(this);
            years.Add(yc);

            //yc.MdiParent = frmMain.MainWindow;
        }
        List<Form> frms = new List<Form>();
        List<Form> months = new List<Form>();
        List<Form> years = new List<Form>();
        private void btnCompare_Click(object sender, EventArgs e)
        {
            months.Clear();
            years.Clear();
            int sel1 = lvTariffs.CheckedItems.Count;
            int sel2 = lvDatasheets.CheckedItems.Count;
            if( sel1 == 0 || sel2 == 0 )
            {
                MB.Warning(
                    GetString("s7005", "Please tick both Tariff and Data sheet you want to compare, and try again."));
                return;
            }
            for (int i = 0; i < sel1; i++)
            {
                object o = lvTariffs.CheckedItems[i].Tag;
                if (o == null)
                    continue;
                Tariff t = (Tariff)o;

                for (int j = 0; j < sel2; j++)
                {
                    o = lvDatasheets.CheckedItems[j].Tag;
                    if (o == null)
                        continue;
                    EnergyData real = (EnergyData)o;

                    EnergyData simulation = (EnergyData)real.Clone();
                    simulation.SetTariff(t);
                    if (real.IsMonthly())
                        ShowMonthly(real, simulation);
                    else
                        ShowYearly(real, simulation);
                }
            }
            Cascade();
        }
        void Cascade()
        {
            frms.Clear();
            frms.AddRange(years);
            frms.AddRange(months);
            if (frms.Count == 0)
                return;
            int x = frms[0].Location.X;
            int y = frms[0].Location.Y;
            for (int i = 0; i < frms.Count; i++ )
            {
                Point p = frms[i].Location;
                //System.Diagnostics.Debug.Print(p.ToString());
                x += 20;
                y += 20;
                if (x >= System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width-50 ||
                    y >= System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height-50)
                {
                    x = 0;
                    y = 0;
                }
                frms[i].Location = new Point(x,y);
                //frms[i].Visible = true;
                //frms[i].Show(this);
                //System.Diagnostics.Debug.Print(frms[i].Location.ToString());
            }
        }

        private void lvTariffs_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvTariffs.ListViewItemSorter = new ListViewItemComparer(e.Column);
            lvTariffs.Columns[e.Column].Tag = lvTariffs.Sorting;
        }

        private void lvDatasheets_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lvDatasheets.ListViewItemSorter = new ListViewItemComparer(e.Column);
            lvDatasheets.Columns[e.Column].Tag = e.Column;
        }


        class ListViewItemComparer : IComparer
        {
            private int col;
            public ListViewItemComparer()
            {
                col = 0;
            }
            public ListViewItemComparer(int column)
            {
                col = column;
            }
            public int Column
            {
                get { return col; }
                set { col = value; }
            }
            public int Compare(object x, object y)
            {
                return String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        private void lvTariffs_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if(sender.Equals(lvTariffs)||sender.Equals(lvDatasheets))
            {
                //ListView lv = (ListView)sender;
                e.Item.Focused = true;
                e.Item.Selected = true;
            }
        }

    }

}
