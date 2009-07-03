using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Elink
{
    public partial class SubRightSettings : UserControl
    {
        static SubRightSettings i = null;
        public static SubRightSettings Instance { get { return i; } }
        public SubRightSettings()
        {
            i = this;
            Global.GoMultiLanguage();
            InitializeComponent();
            GoTariff(singletariff);
            singletariff.SetCapture(btn21_MouseEnter, btn21_MouseLeave);
            //dualtariff.SetCapture(btn21_MouseEnter, btn21_MouseLeave);
            advancedtariff.SetCapture(btn21_MouseEnter, btn21_MouseLeave);
            //advancedtariff.SetData(Settings.I.advTariff);
            
            LoadPage();

            Settings.AddListener(OnSettingsChange);
        }
        private void OnSettingsChange(object sender, EventArgs e)
        {
            UpdatePersonalInfo();
        }
        private void GoTariff(UserControl tariff)
        {
            UserControl c = singletariff;
//             if (current != null)
//             {
//                 c = current as UserControl;
//                 c.Hide();
//             }
            if (tariff == singletariff && !Global.IsSpanish())
            {
                btn21.Visible = true;
                btn23.Visible = false;
//                 btn23.Visible = true;
//                 btn21x.Visible = false;
//                 btn23x.Visible = false;
            }
            else
                if( tariff != singletariff && Global.IsSpanish())
                {
                    btn21.Visible = false;
                    btn23.Visible = true;
//                     btn21x.Visible = true;
//                     btn23x.Visible = true;
                }
                else
                    return;

            c = tariff;
            pl21.Controls.Add(c);
            current = tariff as ITariff;
            c.Visible = true;

            Focus2();
        }
        private void LoadPage()
        {
            ReadCountryProfiles();
            ReadPersonalProfiles();
            cb14.SelectedIndex = 0;
            cb15.SelectedIndex = 0;
            if (Global.IsSpanish())
            {
                //Focus2();
                btn23.Location = btn21.Location;
                //btn23x.Location = btn21.Location;
                btn21.Visible = false;
//                 btn21x.Visible = false;
            }
            else
            {
                //Focus1();
                btn23x.Visible = false;
//                 btn23.Visible = false;
            }

        }
        Color focuscolor = Color.FromArgb(0, 98, 170);
        Color nonfocuscolor = Color.Gray;
        //Sub2RightSingleTariff singletariff = new Sub2RightSingleTariff();
        TariffScheme singletariff = new TariffScheme();
        //TariffDual dualtariff = new TariffDual();
        TariffAdvanced advancedtariff = new TariffAdvanced();
        ITariff current;
        private void Focus1()
        {
            Unfocus2();

            plTop.BackColor = focuscolor;
            btn1.BackColor = pl1.BackColor = focuscolor;
        }
        private void Focus2()
        {
            Unfocus2();
            Unfocus1();
            if (current != null)
                current.AllbackColor = focuscolor;
            Color back1 = Color.Gray;
            Color back2 = Color.DarkGray;
            Color back3 = Color.Silver;
            
            if( current == singletariff )
                back1 = focuscolor;
            //else if( current == dualtariff )
            //    back2 = focuscolor;
            else if (current == advancedtariff)
            {
                back3 = focuscolor;
                back1 = Color.Silver;
            }
            btn21.BackColor = focuscolor;
            btn23.BackColor = focuscolor;
//             btn21.BackColor = back1;
//             btn23.BackColor = back3;
//             //btn21x.RightBackBackColor = back1;
//             btn23x.BackColor = back3;
//             btn21x.BackColor = back1;
//             btn23x.BackBackColor = back1;
//             btn21.RightBackBackColor = back3;
            //btn22.BackColor = btn21.RightBackBackColor = back2;
            //btn23.BackColor = btn22.RightBackBackColor = back3;
            pl2.BackColor = pl21.BackColor = focuscolor;
            plPatch.BackColor = focuscolor;
        }
        private void Focus3()
        {
//             Unfocus1();
//             Unfocus2();
//             btn3.BackColor = pl3.BackColor = lb31.BackColor = focuscolor;
//             lb32.BackColor = lb33.BackColor = rnd3.BackBackColor = rnd3.RightBackBackColor = focuscolor;
        }
        private void Unfocus1()
        {
            if (tb11.Focused || cb12.Focused || cb13.Focused || 
                tb14.Focused || tb3.Focused ||
                cb14.Focused || cb15.Focused)
                return;

            plTop.BackColor = nonfocuscolor;
            btn1.BackColor = pl1.BackColor = nonfocuscolor;
        }
        private void Unfocus2()
        {
            if (current == null)
                return;
            if (current.IsFocused)
                return;
            Color back1 = Color.Gray;
            //Color back2 = Color.DarkGray;
            Color back3 = Color.Silver;

            if (current == singletariff)
            {
                //back1 = back1;
            }
            //else if (current == dualtariff)
            //    back2 = focuscolor;
            else if (current == advancedtariff)
            {
                back3 = nonfocuscolor;
                back1 = Color.Silver;
            }
            this.SuspendLayout();
            current.AllbackColor = nonfocuscolor;
            btn21.BackColor = nonfocuscolor;
            btn23.BackColor = nonfocuscolor;
//             btn21.BackColor = back1;
//             btn23.BackColor = back3;
//             btn21.RightBackBackColor = back3;
//             btn23x.BackColor = back3;
//             btn23x.BackBackColor = Color.White;
            //btn22.BackColor = btn21.RightBackBackColor = back2;
            //btn23.BackColor = btn22.RightBackBackColor = back3;
            pl2.BackColor = pl21.BackColor = nonfocuscolor;
            plPatch.BackColor = nonfocuscolor;
            this.ResumeLayout();
        }
        private void Unfocus3()
        {
//             if (tb3.Focused)
//                 return;
// 
//             btn3.BackColor = pl3.BackColor = lb31.BackColor = nonfocuscolor;
//             lb32.BackColor = lb33.BackColor = nonfocuscolor;
//             rnd3.BackBackColor = rnd3.RightBackBackColor = nonfocuscolor;
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

        private void btn3_MouseEnter(object sender, EventArgs e)
        {
            Focus3();
        }

        private void btn3_MouseLeave(object sender, EventArgs e)
        {
            Unfocus3();
        }

        private void btn22_Click(object sender, EventArgs e)
        {
            //GoTariff(dualtariff);
        }

        private void btn23_Click(object sender, EventArgs e)
        {
            Settings.I.single_tariff = false;
            //Settings.I.advTariff.tariffs = 1;

            GoTariff(advancedtariff);
        }

        private void btn21_Click(object sender, EventArgs e)
        {
//             Settings.I.single_tariff = true;
//             Settings.I.advTariff.tariffs = 1;
//             Settings.I.advTariff.max_power = 0;
//             Settings.I.advTariff.power_fee = 0;
//             Settings.I.advTariff.meter_rent = 0;
//             Settings.I.advTariff.elec_tax = 0;
//             Settings.I.advTariff.VAT = 0;
//             Settings.I.advTariff.over_limit = 0;
//             Settings.I.advTariff.over_charge = 0;

            GoTariff(singletariff);

            UpdatePersonalInfo();
        }

        // Country info
        const string XML_COUNTRY = "CountryProfiles.xml";
        // Personal info
        const string XML_PERSONAL = "PersonalProfiles.xml";


        List<CountryProfile> cps;
        private static int CompareCountry(CountryProfile cp1, CountryProfile cp2)
        {
            return cp1.CountryName.CompareTo(cp2.CountryName);
        }
        private void ReadCountryProfiles()
        {
            cps = XMLUtil<List<CountryProfile>>.LoadXml(XML_COUNTRY);
            if (cps == null)
            {
                MB.Warning(
                    GetString("s5000", "Country profiles read failure.")+
                    GetString("s5001", "You will not able to see country information."));
                return;
            }
            cps.Sort(CompareCountry);

            UpdateCountryInfo();
        }
        int FindCountryInfo(string str)
        {
            string country = str;
            if (cps == null)
                return -1;
            //DataRow r = dsCountries.Tables[TBL_COUNTRY].Rows.Find(country);
            for (int i = 0; i < cps.Count; i++ )
            {
                if (cps[i].CountryName.Equals(country))
                    return i;
            }
            return -1;
        }
        private void UpdateCountryInfo()
        {
            if (cps == null)
                return;
            if (cps.Count == 0)
                return;
            //DataRowCollection rows = dsCountries.Tables[TBL_COUNTRY].Rows;
            int count = cps.Count;
            List<string> countries = new List<string>();
            List<string> symbols = new List<string>();
            for(int i=0; i<cps.Count; i++)
            {
                countries.Add(cps[i].CountryName);//(string)r[COL_CountryName];
                symbols.Add(cps[i].CurrencySymbol);//(string)r[COL_CurrencySymbol];
            }

            cb12.DataSource = countries;
            cb13.DataSource = symbols;
            //cb12.SelectedIndex = 0; // default

        }

        private void cb12_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        public bool ReadPersonalProfiles()
        {
            Settings.Load();
            UpdatePersonalInfo();
            return true;
        }
        public void UpdatePersonalInfo()
        {
            tb11.Text = Settings.I.user_name;
            cb14.SelectedIndex = Settings.I.target_type;
            cb15.SelectedIndex = Settings.I.target_period;
            tb14.Text = Settings.I.carbon.ToString();
            tb3.Text = Settings.I.target.ToString();
            tbProfile.Text = Settings.I.advTariff.tariff_name;
            singletariff.Price = Settings.I.advTariff.energy_rates_p1;
            advancedtariff.SetData(Settings.I.advTariff);
            advancedtariff.LoadPage();
            if (!Global.IsSpanish() || Settings.I.single_tariff)
            {
                singletariff.SetData(Settings.I.advTariff);
                singletariff.LoadPage();
                GoTariff(singletariff);
            }
            else
                GoTariff(advancedtariff);

            cb14.Items[1] = Settings.I.currency;
            Inform(OnUserNameChange, this, new UserNameEvent(Settings.I.user_name));
            Inform(OnPriceChange, this, new PriceEvent(Settings.I.advTariff.energy_rates_p1, Settings.I.currency));
            Inform(OnCarbonChange, this, new CarbonEvent(Settings.I.carbon));
        }
        public bool IsEnglish { get { return !Global.IsSpanish(); } }
        public void PreservePersonalInfo()
        {
            if (IsEnglish)
                Settings.I.advTariff = singletariff.SaveData();
            else
                Settings.I.advTariff = advancedtariff.SaveData();

            Settings.I.user_name = tb11.Text;
            Settings.I.country = cb12.SelectedItem.ToString();
            Settings.I.carbon = float.Parse(tb14.Text);
            Settings.I.target = float.Parse(tb3.Text);
            //Settings.I.advTariff.energy_rates_p1 = (float)singletariff.Price;
            //Settings.I.simple = (singletariff.Visible);
            Settings.I.advTariff.tariff_name = tbProfile.Text;
            Settings.I.target_type = cb14.SelectedIndex;
            Settings.I.target_period = cb15.SelectedIndex;
        }
        public bool SavePersonalInfo()
        {
//             if( EnergyDataNew.Empty() )
//             {
//                 EnergyDataNew.NewPeriod(Settings.I.advTariff);
//             }
//             else
            string newname = tbProfile.Text;
            string oldname = oldTariff.tariff_name;

            if( oldname != null )
                if (oldname.Length != 0)
            {
                if (!newname.Equals(
                    oldname,
                    StringComparison.OrdinalIgnoreCase))
                {
                    DialogResult dr = MB.YesNoCancelQ(
                        GetString("s5008", "Would you like to rename the current tariff or switch to a new one?") + "\n" +
                        GetString("s5009", "Press Yes to rename and No to switch to a new tariff") + "\n" +
                        GetString("s5010", "Press Cancel to return without changes"));
                    if (dr == DialogResult.Cancel)
                        return false;
                    if (dr == DialogResult.No)
                    {
                        PreservePersonalInfo();
                        EnergyData.NewPair(oldTariff);
                    }
                    if (dr == DialogResult.Yes)
                    {
                        //                     EnergyDataNew.Rename(oldTariff.tariff_name, tbProfile.Text);
                        try
                        {
                            EnergyData ed = EnergyData.Find(oldname);
                            if (ed != null)
                                ed.Rename(newname);
                            else
                                return false;
                            Settings.I.advTariff.tariff_name = newname;
                            Settings.I.Save();
                            UpdatePersonalInfo();
                            EnergyData.Load();
                            oldTariff = Settings.I.advTariff;
                            JumpWindows.TriggerReloadData();
                            return true;
                        }
                        catch(System.IO.IOException)
                        {
                            MB.Warning("Cannot rename to an existing tariff.");
                            return false;
                        }
                    }
                }
            }

            PreservePersonalInfo();

            Settings.I.Save();
            if( EnergyData.IsCurrentPresent() )
            {
                EnergyData.T = Settings.I.advTariff;
                EnergyData.SaveAllCSVAndXML();
                //EnergyData.Load();
            }
            oldTariff = Settings.I.advTariff;

            JumpWindows.TriggerReloadData();

            UpdatePersonalInfo();
            return true;
        }
        static void Inform(EventHandler eh, object sender, EventArgs ea)
        {
            if (eh != null)
                eh.Invoke(sender, ea);
        }
        public EventHandler OnCarbonChange;
        public EventHandler OnPriceChange;
        public EventHandler OnUserNameChange;
        public EventHandler OnTargetChange;
        private string GetString(string key, string def)
        {
            return Global.GetString(key, def);
        }
        private void btnDiscard_Click(object sender, EventArgs e)
        {
            string title = GetString("s5003", "Confirm");
            string text = GetString("s5004", "Are you sure to discard all changes and restore the last saved settings?");
            if (!MB.YesNoQ(text))
                return;
            if( ReadPersonalProfiles() )
            {
                MB.OKI(GetString("s5005", "Settings has been restored."));
            }
        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (tbProfile.Text.Length == 0)
            {
                MB.Warning(GetString("s5002", "Please input Tariff ID. and try again"));
                return;
            }

            string title = GetString("s5003", "Confirm");
            string text = GetString("s5006", "Are you sure to save changes?");
            if (!MB.YesNoQ(text))
                return;
            if( !tbProfile.Text.Equals(Settings.I.advTariff.tariff_name, 
                StringComparison.CurrentCultureIgnoreCase))
            {
//                 if (DialogResult.Yes !=
//                     MessageBox.Show(
//                     "You are about to create a different Tariff ID,"+
//                     "\nMake sure you have collected the recent data before switching." +
//                     "\nIf you want to proceed switching Tariff ID, click YES"+
//                     "\nIf you want to cancel and collect data, click NO", 
//                     "Change Tariff ID", 
//                     MessageBoxButtons.YesNo, MessageBoxIcon.Question))
//                     return;
            }
            if (!SavePersonalInfo())
                return;
            MB.OKI(GetString("s5007", "Setting changes saved."));
//             if (!firsttime)
//                 return;
// 
//             if (MB.YesNoQ("Would you like to collect data from E2 now?"))
//             {
//                 JumpWindows.TriggerDoCollectData();
//                 firsttime = false;
//             }
        }
        //bool firsttime = false;
        private void SubRightSettings_MouseMove(object sender, MouseEventArgs e)
        {
            System.Diagnostics.Debug.Print(e.Location.ToString());
        }

        private void cb13_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.I.currency = cb13.Text;
            Settings.I.advTariff.price_currency = Settings.I.currency;
            UpdatePersonalInfo();
        }

        private void cb12_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string country = cb12.SelectedItem as string;
            if (country == null || country.Length == 0)
                return;
            int idx = FindCountryInfo(country);
            if (idx != -1)
            {
                cb13.SelectedItem = cps[idx].CurrencySymbol;
                tb14.Text = cps[idx].CarbonRatio.ToString();
                Settings.I.country = country;
                Settings.I.currency = cps[idx].CurrencySymbol;
                Settings.I.advTariff.price_currency = Settings.I.currency;
                Settings.I.carbon = cps[idx].CarbonRatio;
                UpdatePersonalInfo();
            }
        }
        Tariff oldTariff;
        private void SubRightSettings_Load(object sender, EventArgs e)
        {
            oldTariff = (Tariff)Settings.I.advTariff.Clone();
            Focus1();

            if( singletariff.Visible )
            {
                btn21_Click(null, null);
            }

            GoTariff(singletariff);
            GoTariff(advancedtariff);

            cb12.Text = Settings.I.country;
            cb13.Text = Settings.I.currency;

        }

        public void RunWizard()
        {
            tbProfile.Text = "";
            ttGuide.Active = true;
            ttGuide.ShowAlways = true;

            //cb12.Text = "YOUR COUNTRY";

            Point pt = tbProfile.Location;
            pt = this.PointToScreen(pt);
            pt.Offset(2, 2);
            Cursor.Position = pt;

            tbProfile.Text = Settings.I.advTariff.tariff_name;
            //Settings.I.advTariff.tariff_name = "Tarifa 2.0.3";
            this.UpdatePersonalInfo();

            Color og = tbProfile.BackColor;
            Color cg = Color.OrangeRed;
            for (int i = 0; i < 5; i++ )
            {
                BlinkProfile(cg);
                BlinkProfile(og);
            }
            //firsttime = true;
        }
        void BlinkProfile(Color cg)
        {
            tbProfile.BackColor = cg;
            rfTariff.FillColor = cg;
            Application.DoEvents();
            System.Threading.Thread.Sleep(500);
        }

        private void tb11_Validated(object sender, EventArgs e)
        {
            Settings.I.user_name = tb11.Text;
        }
        private void tbProfile_Validated(object sender, EventArgs e)
        {
            Settings.I.advTariff.tariff_name = tbProfile.Text;
        }
        public void DetailCancel()
        {

        }
        public void DetailOK()
        {
            Settings.I.advTariff = advancedtariff.SaveData();
            UpdatePersonalInfo();
        }

        private void tbProfile_KeyPress(object sender, KeyPressEventArgs e)
        {
            //this.advancedtariff.TariffName = tbProfile.Text;
        }

        private void tbProfile_KeyUp(object sender, KeyEventArgs e)
        {
            //this.advancedtariff.TariffName = tbProfile.Text;
        }

        private void tbProfile_TextChanged(object sender, EventArgs e)
        {
            this.advancedtariff.TariffName = tbProfile.Text;
        }
    }
    public class UserNameEvent : EventArgs
    {
        string name;
        public string Name
        {
            get { return name; }
        }
        public UserNameEvent(string n) { name = n; }
    }
    public class PriceEvent : EventArgs
    {
        double price;
        string currency;
        public double Price
        {
            get { return price; }
        }
        public string Currency { get { return currency; } }
        public PriceEvent(double p, string c) { price = p; currency = c; }
    }
    public class CarbonEvent : EventArgs
    {
        double carbon;
        public double Carbon
        {
            get { return carbon; }
        }
        public CarbonEvent(double c) { carbon = c; }
    }

    public class CountryProfile
    {
        public string CountryName;
        public string CurrencyAb;
        public string CurrencySymbol;
        public float CarbonRatio;

        public override string ToString()
        {
            return CountryName + ", " + CurrencySymbol;
        }
    }
}
