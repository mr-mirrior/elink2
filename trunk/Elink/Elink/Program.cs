using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.IO;

namespace Elink
{
    static class Program
    {
//         static DateTime justnow;
//         static void OnOver(object sender, EventArgs e)
//         {
//             TimeSpan ts = DateTime.Now - justnow;
//             System.Diagnostics.Debug.Print("It takes {0} to finish the operations.", ts.ToString());
//         }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //frmFirst f = new frmFirst();
            //f.ShowDialog();
//             string s = "2008-09-08T16:04:00.0000000Z";
//             DateTime t;
//             DateTime.TryParse(s, out t);
// 
//             s = t.ToLocalTime().ToString();
    
            //EnergyDataNew.Load();
            //EnergyDataNew.SimulateMerge();
            //EnergyDataNew.Save();
            //EnergyDataNew.Save();
            //CultureInfo
//             Serial.YetSerialPort pt = new Elink.Serial.YetSerialPort();
//             byte[] x = new byte[512];
//             pt.OpenPort("COM4", 38400, 1, 8, 1);
//             pt.ReadPort(1, ref x);

//             byte[] y = new byte [] {0x45,0x4c};
//             string s = string.Format("0x{0:X2}{1:X2}, \"{2}{3}\"",
//                 y[0], y[1], (char)y[0], (char)y[1]);
// 
//             System.Diagnostics.Debug.Print(s);

//   Dim asm As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()   
//   For   Each   strName   As   String   In   asm.GetManifestResourceNames()   
//   Console.WriteLine(strName)   
//   Next   
//   如果NDocGui.ToolBarImages.resources不在清单之中,则表明你的资源根本就不存在.   

//             System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
//             string[] lst = asm.GetManifestResourceNames();
//             string all = "";
//             foreach (string str in lst)
//             {
//                 //Console.WriteLine(str);
//                 all += str;
//                 all += "\r\n";
//             }
            
//            MB.OKI(all);
//             DateTime today = new DateTime(2008, 7, 31);
//             DateTime tomorrow = DateTime.Today;
//             int weeks = Calendar.WeekDistance(today, tomorrow);
//             weeks = Calendar.WeekDistance(tomorrow, today);

            System.IO.Directory.SetCurrentDirectory(
                Path.GetDirectoryName(Application.ExecutablePath));

            Settings.Load();
            Global.Language = Settings.I.lang;
            Global.GoMultiLanguage();
            if (Settings.I.lang == null)
                Global.Language = "en-US";
            else
                if (Settings.I.lang.Length == 0)
                    Global.Language = "en-US";


            //Float24bit.Convert(0x5F, 0x39, 0xFF);
            //bool istrue = Settings.I.advTariff.IsSummer(DateTime.Today);
            //EnergyData.LoadDeviceDataCSV();
//             EnergyData.Simulate("PreservedData.2008.08.25 19.38.13.xml");
//             EnergyData.Simulate("PreservedData.2008.8.26 14.18.58.xml");
//             EnergyData.Simulate("PreservedData.2008.8.26 16.14.57.xml");
//             EnergyData.SaveAllCSVAndXML();
//             byte h = 0x53;
//             byte m = 0xe9;
//             byte l = 0x02;
//             float x = Float24bit.Convert(0, 0, 0);
//             float x = 12.85f;
//             double y = 12.85;
//             byte[] z = new byte[] { 0x66, 0xec, 0x04, 0x00};
//             //{ /*0x414d999a*/ 0x9a, 0x99, 0x4d, 0x41};//{ 0x00, 0x04, 0xec, 0x66};
//             unsafe
//             {
//                 fixed(byte *pb = z)
//                 {
//                     float zz = (float)System.Runtime.InteropServices.Marshal.PtrToStructure
//                         (new IntPtr(pb), typeof(float));
//                 }
//             }

            //Serial.SerialCOM.Probe();
//             if( !sc.Open() )
//             {
//                 MessageBox.Show("COM4 cannot be opened, maybe in use or no device.");
//                 return;
//             }
//             sc.CollectData();

//             sc.SendProbe();
//             Thread.Sleep(1000);
//             sc.RequestMaxPowerInMonth(0);
//             Thread.Sleep(1000);
//             sc.RequestEnergyDaily(0);
            //sc.SendProbe();
            //Thread.Sleep(1000);
            //sc.RequestEnergyWeekly(0);
            //Thread.Sleep(1000);
            //sc.RequestEnergyMonthly(0);
            //justnow = DateTime.Now;
            //sc.FinishTransmission += OnOver;
            //sc.RequestEnergyHourlyDaily(0);
            //Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            //System.Threading.Thread.Sleep(1000);
            //string[] s = System.Environment.GetLogicalDrives();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            if( Global.IsSpanish() )
            {
                Splash splash = new Splash();
                splash.Switch();
            }

            Application.Run(new frmMain());
            
            //sc.Close();
        }
    }
}
