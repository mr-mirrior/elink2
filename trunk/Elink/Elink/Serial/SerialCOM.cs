#define MONO_COMPATIBLE1

using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
//using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;

namespace Elink.Serial
{
    public class SerialCOM: IDisposable
    {
        enum STATUS
        {
            IDLE = 0,
            SENT_PROBE_WATING_ACK,
            GOT_PROBE_ACK,
            
            REQUEST_CANCEL,

            REQUEST_MAX_P_MONTHLY,
            GOT_MAX_P_MONTHLY,

            REQUEST_ENERGY_HOURLY,
            GOT_ENERGY_HOURLY,

            REQUEST_ENERGY_DAILY,
            GOT_ENERGY_DAILY,

            REQUEST_ENERGY_WEEKLY,
            GOT_ENERGY_WEEKLY,

            REQUEST_ENERGY_MONTHLY,
            GOT_ENERGY_MONTHLY,

            REQUEST_FIRMWARE_VERSION,
            GOT_FIRMWARE_VERSION,

            REQUEST_DATETIME,
            GOT_DATETIME
        }
        enum MSG_TYPE
        {
            FIRMWARE_VER = 0,
            PROBE = 1,
            MAX_POWER_MONTHLY = 2,
            ENERGY_HOURLY = 3,
            CANCEL = 4,
            ENERGY_DAILY = 5,
            ENERGY_WEEKLY = 6,
            ENERGY_MONTHLY = 7,
            DATETIME = 8
        }
        STATUS status = STATUS.IDLE;
        SerialPort port = null;
        const int baudrate = 38400;//19200;
        public SerialCOM(string pname)
        {
            EventHandler OnStatusFake = delegate(object sender, EventArgs e)
            {

            };
            EventHandler OnStep = delegate(object sender, EventArgs e)
            {

            };
            port = new SerialPort(pname, baudrate);
            if( port == null )
            {
                throw new Exception("Port open fatal "+pname);
            }

            //port.ReadTimeout = 500;
            //MB.OKI(port.ReadTimeout.ToString());
            //port.ReadTimeout
            port.ErrorReceived += port_ErrorReceived;
            FinishTransmission = OnFinishFake;
            InProgress += OnProgress;
            InStep += OnStep;
            OnStatus += OnStatusFake;

            retry.Tick += OnRetry;

#if MONO_COMPATIBLE
            timerCheck.Interval = 100;
            timerCheck.Enabled = true;
            timerCheck.Tick += OnData1;
#else
            port.DataReceived += OnData;
#endif
            //timerCheck.Start();
        }
        public event EventHandler FinishTransmission;
        public event EventHandler InProgress;
        public event EventHandler InStep;
        public event EventHandler OnStatus;

        System.Windows.Forms.Timer retry = new System.Windows.Forms.Timer();
        InvokeDelegate retryAction = null;
        int retries = 0;
        const int MAX_RETRIES = 3;
        void OnRetry(object sender, EventArgs e)
        {
            if (retryAction == null)
                return;
            if( ++retries == MAX_RETRIES )
            {
                StopRetry();
                return;
            }
            TRACE("SerialCOM: time out, retrying {0}...", retries);
            Cancel();
            Thread.Sleep(500);
            retryAction();
        }
        const int RETRY_INTERVAL = 500;
        void RetryIfTimeout(InvokeDelegate act)
        {
            retry.Interval = RETRY_INTERVAL;
            retryAction = act;
            retry.Start();
        }
        void StopRetry()
        {
            retry.Stop();
            retries = 0;
        }
        void port_ErrorReceived(object sender, System.IO.Ports.SerialErrorReceivedEventArgs e)
        {
            MB.Warning("Port connection error: "+e.ToString());
            this.Dispose();
        }
        public string PortName { get { return port.PortName; } }
        public void Dispose()
        {
//             port.ErrorReceived = null;
//             port.DataReceived = null;
            FinishTransmission = null;
            InProgress = null;
            InStep = null;

            Close();
            port.Dispose();
        }
        public bool Open()
        {
            if (port.IsOpen)
                return true;
            try
            {
                port.Open();
            }
            catch (System.Exception)
            {
                //MB.Warning(e.ToString());
                //System.Diagnostics.Debug.Print(e.ToString());
                return false;
            }
            if(!port.IsOpen)
            {
                MB.Warning("Port is not opened " + port.PortName);
            }
            //MB.OKI(port.IsOpen.ToString()); 
            //MB.OKI("port.BytesToRead.ToString() returns " + port.BytesToRead.ToString());
            return true;
        }
        public void Close()
        {
            port.Close();
            status = STATUS.IDLE;
        }
        public void Disconnect()
        {
            ELMsgDisconnect req = ELMsgDisconnect.Instance();
            Send(req);

            Thread.Sleep(500);
            Close();
        }

        int read = 0;
        DateTime startTime;
        public void CollectData()
        {
            max_p_monthly13 = 0;
            daily8 = 0;
            monthly25 = 0;
            weekly8 = 0;
            hourly240 = 0;

            //int WillBeReplacedWithDeviceTime;
            //EnergyData.devdata.now_in_E2 = DateTime.Now;
            startTime = DateTime.Now;
            read = 0;
#if MONO_COMPATIBLE
            timerCheck.Start();
#endif
            OnFinish();
        }
        private void OnFinishFake(object sender, EventArgs e)
        {

        }
        private void OnReallyFinish(bool succeed)
        {
#if MONO_COMPATIBLE
                    timerCheck.Stop();
#endif
            object sender = this;
            if (!succeed)
                sender = null;
            status = STATUS.IDLE;
            FinishTransmission.Invoke(sender, null);
            TRACE("Finished. {0}", (DateTime.Now - startTime).ToString());
        }
        private void OnFinish()
        {
            switch(read++)
            {
                case 0:
                    RequestDateTime();
                    break;
                case 1:
                    RequestEnergyHourlyDaily(0);
                    break;
                case 2:
                    RequestMaxPowerInMonth(0);
                    break;
                case 3:
                    RequestEnergyDaily(0);
                    break;
                case 4:
                    RequestEnergyWeekly(0);
                    break;
                case 5:
                    RequestEnergyMonthly(0);
                    break;
                case 6:
                    RequestFirmwareVer();
                    break;
                case 7:
                    OnReallyFinish(true);
                    return;
            }
            //TRACE("Step {0} of {1}", read, 5);
            //InStep.Invoke(this, new StepEvent(read, 5));
        }
        private void OnProgress(object sender, EventArgs e)
        {
            ProgressEvent f = (ProgressEvent)e;
            //TRACE("Step {2}: {0} of {1}", f.pos, f.count, read);
        }
        public void Cancel()
        {
            ELMsgCancel req = ELMsgCancel.Instance();
            Send(req);
        }
        public void OnCancel()
        {
            if (status != STATUS.REQUEST_CANCEL)
                return;
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgCancelACK data = (ELMsgCancelACK)d;
            if(data.IsValid())
            {
                status = STATUS.IDLE;
            }
        }
        private object DecodeFromRecv(int size)
        {
            if (size > port.BytesToRead)
                return null;
            byte[] x = new byte[size];
            port.Read(x, 0, size);
            byte[] y = new byte[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                int idx = i;
                if(i<4)
                    idx = ((i & 1) == 1) ? i - 1 : i + 1;

                y[idx] = x[i];
            }

            // header must be 0x454C: "EL"
            //if (y[0] != 0x45 || y[1] != 0x4c) // little-endian
            if (y[0] != 0x4c || y[1] != 0x45)  // big-endian
            {
                string s = string.Format("0x{0:X2}{1:X2}, \"{2}{3}\"",
                    y[0], y[1], (char)y[0], (char)y[1]);
                
                MB.Warning("Transmission Error: Invalid Data Header, should be 'EL', but got "
                    +s);
                return null;
            }

            Type t = null;
            switch ((MSG_TYPE)y[3])
            {
                case MSG_TYPE.FIRMWARE_VER:
                    t = typeof(ELMsgFirmwareVer);
                    break;
                case MSG_TYPE.PROBE:
                    t = typeof(ELMsgProbeACK);
                    break;
                case MSG_TYPE.MAX_POWER_MONTHLY:
                    t = typeof(ELMsgMaxPowerMonth);
                    break;
                case MSG_TYPE.ENERGY_HOURLY:
                    t = typeof(ELMsgEnergyHourlyDaily);
                    break;
                case MSG_TYPE.CANCEL:
                    t = typeof(ELMsgCancelACK);
                    break;
                case MSG_TYPE.ENERGY_DAILY:
                    t = typeof(ELMsgEnergyDaily);
                    break;
                case MSG_TYPE.ENERGY_WEEKLY:
                    t = typeof(ELMsgEnergyWeekly);
                    break;
                case MSG_TYPE.ENERGY_MONTHLY:
                    t = typeof(ELMsgEnergyMonthly);
                    break;
                case MSG_TYPE.DATETIME:
                    t = typeof(ELMsgDateTime);
                    break;
                default:
                    MB.Warning(string.Format(
                        "Transmission Error: Invalid type value, status {0}, but got {1}",
                        status, y[3])
                        );
                    return null;
            }
            object ob;
            unsafe
            {
                fixed (byte* pb = y)
                {
                    ob = Marshal.PtrToStructure((IntPtr)pb, t);
                }
            }
            return ob;
        }
        private void Send(object x)
        {
            if (!port.IsOpen)
                return;
            byte[] flow = ELMsg.EncodeToSend(x);
            port.Write(flow, 0, flow.Length);
        }
#if MONO_COMPATIBLE
        System.Windows.Forms.Timer timerCheck = new System.Windows.Forms.Timer();
#endif
        private void SendProbe()
        {
            if (!port.IsOpen || status != STATUS.IDLE)
                return;

            ELMsgProbe probe = ELMsgProbe.Instance();

            status = STATUS.SENT_PROBE_WATING_ACK;
            Send(probe);
            Thread.Sleep(100);
            Application.DoEvents();
            Send(probe);
            Thread.Sleep(100);
            Application.DoEvents();

            Send(probe);
            Thread.Sleep(100);
            Application.DoEvents();

            Send(probe);
            Thread.Sleep(100);
            Application.DoEvents();
            //Thread.Sleep(300);
        }
        private static void TRACE(string fmt, params object[] args)
        {
            System.Diagnostics.Debug.Print(fmt, args);
        }
        private void OnProbeACK()
        {
            int size = OnRecvCheckValid(); 
            if (0 == size)
                return;
            if (status != STATUS.SENT_PROBE_WATING_ACK)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgProbeACK ack = (ELMsgProbeACK)d;
            if (ack.IsValid())
            {
                TRACE("Got Probe ACK");
                status = STATUS.GOT_PROBE_ACK;
            }
        }
        private int OnRecvCheckValid()
        {
            int expected_bytes = 0;
            switch (status)
            {
                case STATUS.SENT_PROBE_WATING_ACK:
                    expected_bytes = ELMsgProbeACK.ExpectedSize;//Marshal.SizeOf(typeof(ELMsgProbeACK));
                    break;
                case STATUS.REQUEST_MAX_P_MONTHLY:
                    expected_bytes = ELMsgMaxPowerMonth.ExpectedSize;// Marshal.SizeOf(typeof(ELMsgMaxPowerMonth));
                    break;
                case STATUS.REQUEST_ENERGY_DAILY:
                    expected_bytes = ELMsgEnergyDaily.ExpectedSize;// Marshal.SizeOf(typeof(ELMsgEnergyDaily));
                    break;
                case STATUS.REQUEST_ENERGY_WEEKLY:
                    expected_bytes = ELMsgEnergyWeekly.ExpectedSize;// Marshal.SizeOf(typeof(ELMsgEnergyWeekly));
                    break;
                case STATUS.REQUEST_ENERGY_MONTHLY:
                    expected_bytes = ELMsgEnergyMonthly.ExpectedSize;// Marshal.SizeOf(typeof(ELMsgEnergyMonthly));
                    break;
                case STATUS.REQUEST_ENERGY_HOURLY:
                    expected_bytes = ELMsgEnergyHourlyDaily.ExpectedSize;// Marshal.SizeOf(typeof(ELMsgEnergyHourlyDaily));
                    break;
                case STATUS.REQUEST_FIRMWARE_VERSION:
                    expected_bytes = ELMsgFirmwareVer.ExpectedSize;
                    break;
                case STATUS.REQUEST_DATETIME:
                    expected_bytes = ELMsgDateTime.ExpectedSize;
                    break;
                default:
                    return 0;
            }
            if (expected_bytes > port.BytesToRead)
                return 0;

            return expected_bytes;
        }
        public void RequestDateTime()
        {
            ELReqDateTime req = ELReqDateTime.Instance();
            status = STATUS.REQUEST_DATETIME;
            Send(req);

            RetryIfTimeout(new InvokeDelegate(delegate() { RequestDateTime(); }));
        }
        public void OnDateTime()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgDateTime data = (ELMsgDateTime)d;
            if (!data.IsValid())
                return;
            StopRetry();

            DateTime dt = data.DateTime();
            TRACE("Got datetime {0}", data.DateTime().ToString("F"));
            //EnergyData.devdata.firmware_version = data.Value();
            
            /**** 关键数据哦 ****/
//             TimeSpan ts = dt - DateTime.Today;
//             if( ts.TotalHours >= 24 )
//             {
//                 MB.Error("Device time is more than 24 hours different to PC time.\r\nPlease check the time consistency and try again.");
//                 OnReallyFinish(false);
//             }
            EnergyData.devdata.now_in_E2 = dt.ToLocalTime();//data.DateTime();
            EnergyData.devdata.now_in_PC = DateTime.Now;

            status = STATUS.GOT_DATETIME;
            OnFinish();
        }
        private void RequestFirmwareVer()
        {
            ELReqFirmwareVer req = ELReqFirmwareVer.Instance();
            status = STATUS.REQUEST_FIRMWARE_VERSION;
            Send(req);

            RetryIfTimeout(new InvokeDelegate(delegate() { RequestFirmwareVer(); }));
        }
        private void OnFirmwareVer()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgFirmwareVer data = (ELMsgFirmwareVer)d;
            if (!data.IsValid())
                return;
            StopRetry();
            TRACE("Got firmware version {0:0.0}", data.Value());
            EnergyData.devdata.firmware_version = data.Value();
            OnFinish();
        }

        // 0: this month, 1: last month, max=13
        private void RequestMaxPowerInMonth(int month)
        {
            const int MAX_MONTH = 13;
            if (month == MAX_MONTH)
            {
                //FinishTransmission.Invoke(this, null);
                OnFinish();
                return;
            }
            else
            {

                ELReqMaxPowerMonth req = ELReqMaxPowerMonth.Instance(month);
                status = STATUS.REQUEST_MAX_P_MONTHLY;
                Send(req);
            }
            //OnProgress(month, MAX_MONTH);
            InProgress.Invoke(this, new ProgressEvent(max_p_monthly13++, MAX_MONTH));
            RetryIfTimeout(new InvokeDelegate(delegate() { RequestMaxPowerInMonth(--max_p_monthly13); }));
        }
        private void OnMaxPowerMonthly()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgMaxPowerMonth data = (ELMsgMaxPowerMonth)d;
            if (!data.IsValid())
                return;
            StopRetry();
            TRACE("Got max power for month {0}, bytes: {1:X}, {2:X}, {3:X}", 
                data.Month(), data.max_power1, data.max_power2, data.max_power3);
            EnergyData.devdata.max_power_13months[max_p_monthly13 - 1] = data.Float();
#if NEW_FORMAT_MAX_POWER
            EnergyData.devdata.when_max_power_13months[max_p_monthly13 - 1] = data.When();
#endif
            RequestMaxPowerInMonth(max_p_monthly13);
        }
        private void RequestEnergyDaily(int day)
        {
            const int MAX_DAY = 8;
            if (day == MAX_DAY)
            {
                //FinishTransmission.Invoke(this, null);
                OnFinish();
                return;
            }
            else
            {
                ELReqEnergyDaily req = ELReqEnergyDaily.Instance(day);
                status = STATUS.REQUEST_ENERGY_DAILY;
                Send(req);
            }
            //OnProgress(weekday, MAX_DAY);
            InProgress.Invoke(this, new ProgressEvent(daily8++, MAX_DAY));
            RetryIfTimeout(new InvokeDelegate(delegate() { RequestEnergyDaily(--daily8); }));
        }
        private void OnEnergyDaily()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgEnergyDaily data = (ELMsgEnergyDaily)d;
            if (!data.IsValid())
                return;
            StopRetry();
            TRACE("Got Energy Daily for day {0}, bytes: {1:X}, {2:X}, {3:X}", 
                data.Day(), data.energy1, data.energy2, data.energy3);
            EnergyData.devdata.kwh_8days[daily8 - 1] = data.Float();
            RequestEnergyDaily(daily8);
        }
        private void RequestEnergyWeekly(int week) 
        {
            const int MAX_WEEK = 8;
            if (week == MAX_WEEK)
            {
                //FinishTransmission.Invoke(this, null);
                OnFinish();
                return;
            }
            else
            {
                ELReqEnergyWeekly req = ELReqEnergyWeekly.Instance(week);
                status = STATUS.REQUEST_ENERGY_WEEKLY;
                Send(req);
            }
            //OnProgress(week, MAX_WEEK);
            InProgress.Invoke(this, new ProgressEvent(weekly8++, MAX_WEEK));
            RetryIfTimeout(new InvokeDelegate(delegate() { RequestEnergyWeekly(--weekly8); }));
        }
        private void OnEnergyWeekly()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgEnergyWeekly data = (ELMsgEnergyWeekly)d;
            if (!data.IsValid())
                return;
            StopRetry();
            TRACE("Got Energy adv for week {0}, bytes: {1:X}, {2:X}, {3:X}, float: {4}",
                data.Week(), data.energy1, data.energy2, data.energy3,
                Float24bit.Convert(data.energy1, data.energy2, data.energy3));
            EnergyData.devdata.kwh_8weeks[weekly8 - 1] = data.Float();
            RequestEnergyWeekly(weekly8);
        }
        private void RequestEnergyMonthly(int month)
        {
            const int MAX_MONTH = 25;
            if (month == MAX_MONTH)
            {
                //FinishTransmission.Invoke(this, null);
                OnFinish();
                return;
            }
            else
            {
                ELReqEnergyMonthly req = ELReqEnergyMonthly.Instance(month);
                status = STATUS.REQUEST_ENERGY_MONTHLY;
                Send(req);
            }
            //OnProgress(month, MAX_MONTH);
            InProgress.Invoke(this, new ProgressEvent(monthly25++, MAX_MONTH));
            RetryIfTimeout(new InvokeDelegate(delegate() { RequestEnergyMonthly(--monthly25); }));
        }
        private void OnEnergyMonthly()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            ELMsgEnergyMonthly data = (ELMsgEnergyMonthly)d;
            if (!data.IsValid())
                return;
            StopRetry();
            TRACE("Got Energy adv for month {0}, bytes: {1:X}, {2:X}, {3:X}, float: {4}",
                data.Month(), data.energy1, data.energy2, data.energy3,
                Float24bit.Convert(data.energy1, data.energy2,data.energy3));
            EnergyData.devdata.kwh_25months[monthly25 - 1] = data.Float();
            RequestEnergyMonthly(monthly25);
        }
        int hourly240 = 0;
        int monthly25 = 0;
        int daily8 = 0;
        int max_p_monthly13 = 0;
        int weekly8 = 0;
        private void RequestEnergyHourlyDaily(int day) 
        {
            const int MAX_DAY = 240;
            if (day == MAX_DAY)
            {
                //FinishTransmission.Invoke(this, null);
                OnFinish();
                return;
            }
            else
            {
                ELReqEnergyHourlyDaily req = ELReqEnergyHourlyDaily.Instance(day);
                status = STATUS.REQUEST_ENERGY_HOURLY;
                Send(req);
            }
            //OnProgress(weekday, MAX_DAY);
            InProgress.Invoke(this, new ProgressEvent(hourly240++, MAX_DAY));
            RetryIfTimeout(new InvokeDelegate(delegate { RequestEnergyHourlyDaily(--hourly240); }));
        }
        private unsafe void OnEnergyHourlyDaily()
        {
            int size = OnRecvCheckValid();
            if (0 == size)
                return;
            object d = DecodeFromRecv(size);
            if (d == null)
                return;
            StopRetry();
            ELMsgEnergyHourlyDaily data = (ELMsgEnergyHourlyDaily)d;
            if (!data.IsValid())
                return;
            TRACE("Got Energy hourly in day {0}", data.Day());
            EnergyData.devdata.StoreHourly(data.energy, hourly240);
            RequestEnergyHourlyDaily(hourly240);
        }
        private void OnData1(object sender, EventArgs e)
        {
            OnData(null, null);
        }
        private void OnData(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //MB.OKI("Got data "+port.BytesToRead.ToString());
            if (!port.IsOpen)
                return;
            if (port.BytesToRead == 0)
                return;

            switch( status )
            {
                case STATUS.REQUEST_FIRMWARE_VERSION:
                    OnFirmwareVer();
                    break;
                case STATUS.SENT_PROBE_WATING_ACK:
                    OnProbeACK();
                    break;
                case STATUS.REQUEST_MAX_P_MONTHLY:
                    OnMaxPowerMonthly();
                    break;
                case STATUS.REQUEST_ENERGY_DAILY:
                    OnEnergyDaily();
                    break;
                case STATUS.REQUEST_ENERGY_WEEKLY:
                    OnEnergyWeekly();
                    break;
                case STATUS.REQUEST_ENERGY_MONTHLY:
                    OnEnergyMonthly();
                    break;
                case STATUS.REQUEST_ENERGY_HOURLY:
                    OnEnergyHourlyDaily();
                    break;
                case STATUS.REQUEST_DATETIME:
                    OnDateTime();
                    break;
                default:
                    // clear the buffer;
                    int size = port.BytesToRead;
                    TRACE("Throw away garbage {0} bytes", size);
                    byte[] x = new byte[size];
                    port.Read(x, 0, size);
                    return;
            }
        }
        

        public bool GotProbeACK()
        {
            return status == STATUS.GOT_PROBE_ACK;
        }

        // Object independent
        public static SerialCOM Probe(EventHandler onStatus)
        {
            string[] ports = SerialPort.GetPortNames();
            string found = null;
            for (int i = 0; i < 1; i++)
            {
                foreach (string s in ports)
                {
                    SerialCOM sc = new SerialCOM(s);
                    sc.OnStatus += onStatus;
                    //ss.Add(sc);
                    if (sc.Open())
                    {
                        Thread.Sleep(1000);
                        onStatus(sc, new StatusEvent(string.Format("Probing at {0}, {1}", sc.PortName, i)));
                        sc.SendProbe();
                        Thread.Sleep(500);
                        if (sc.GotProbeACK())
                        {
                            found = sc.PortName;
                            sc.Dispose();
                            break;
                        }
                        Application.DoEvents();
                    }
                    else
                    {
                        onStatus(sc, new StatusEvent(string.Format("Failed to open port " + sc.PortName)));
                    }
                    sc.Dispose();
                }
                if (found != null)
                    break;
            }

            if (found == null)
                return null;
            onStatus(null, new StatusEvent(string.Format("Found right port: {0}", found)));
            return new SerialCOM(found);
        }
        
    }

    public class ProgressEvent: EventArgs
    {
        public int value = 0;
        public int pos = 0;
        public int count = 1;
        public ProgressEvent(int p, int c/*, int v*/)
        {
            pos = p;
            count = c;
            //value = v;
        }
    }
    public class StepEvent: EventArgs
    {
        public int count = 0;
        public int step = 0;
        public StepEvent(int s, int c) { count = c; step = s; }
    }
    public class StatusEvent: EventArgs
    {
        public string status = "";
        public StatusEvent(string s) { status = s; }
    }























    ///////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////
    public static class ELMsg
    {
        public static float CombineFloat(byte b1, byte b2, byte b3)
        {
            //return (b1 << 24 | b2 << 16 | b3 << 8);
            return Float24bit.Convert(b1, b2, b3);
        }
        public static short Head()
        {
            return 0x454C;
        }
        public static short MsgType(short type)
        {
            return (short)(type >> 8);
        }
//         public static bool EqualBuffer(byte[] b1, byte[] b2)
//         {
//             if (b1.Length != b2.Length)
//                 return false;
//             for (int theOne = 0; theOne < b1.Length; theOne++ )
//             {
//                 if (b1[theOne] != b2[theOne])
//                     return false;
//             }
//             return true;
//         }

        public static byte[] EncodeToSend(object x)
        {
            byte[] result = null;
            int size = Marshal.SizeOf(x);
            IntPtr p = Marshal.AllocHGlobal(size);
            result = new byte[size];
            try
            {
                Marshal.StructureToPtr(x, p, false);
                unsafe
                {
                    byte* pb = (byte*)p;
                    int idx = 0;
                    for (int i = 0; i < size; i++ )
                    {
                        // big endian
                        idx = ((i & 1)==1) ? i - 1 : i + 1;

                        result[idx] = pb[i];
                    }
                }
            }
            finally
            {
                Marshal.FreeHGlobal(p);
            }
            return result;
        }
    }
    public struct ELReqFirmwareVer
    {
        public short header;
        public short type;
        public static ELReqFirmwareVer Instance()
        {
            ELReqFirmwareVer req = new ELReqFirmwareVer();
            req.header = ELMsg.Head();
            req.type = 0x0000;
            return req;
        }
    }
    public struct ELMsgFirmwareVer
    {
        public short header;
        public short type;
        public bool IsValid()
        {
            return (header == ELMsg.Head() && ELMsg.MsgType(type) == 0);
        }
        public const int ExpectedSize = 4;
        public float Value()
        {
            int hi, lo;
            lo = (type & 0x0F);
            hi = (type >> 4) & 0x0F;
            return (1.0f * hi + 0.1f * lo);
        }
    }
    public struct ELMsgProbe
    {
        public short header;
        public short type;// = 0x0100;
        public static ELMsgProbe Instance() 
        {
            ELMsgProbe p = new ELMsgProbe();
            p.header = 0x454c; 
            p.type = 0x0100;
            return p;
        }
    }
    public struct ELMsgProbeACK
    {
        public short header;
        public short type;
        public bool IsValid() 
        {
            bool result = true;
            result = result && (header == 0x454c);
            //result = result && (ELMsg.MsgType(type) == 1);
            result = result && (type == 0x01A5);
            return result;
        }
        public const int ExpectedSize = 4;
    }
    public struct ELMsgCancel
    {
        public short header;
        public short type;
        public static ELMsgCancel Instance()
        {
            ELMsgCancel p = new ELMsgCancel();
            p.header = 0x454c;
            p.type = 0x0400;
            return p;
        }
    }
    public struct ELMsgDisconnect
    {
        public short header;
        public short type;
        public static ELMsgDisconnect Instance()
        {
            ELMsgDisconnect p = new ELMsgDisconnect();
            p.header = 0x454c;
            p.type = 0x0401;
            return p;
        }
    }

    public struct ELMsgCancelACK
    {
        public short header;
        public short type;
        public bool IsValid() { return (header == 0x454c && ELMsg.MsgType(type) == 1); }
        public const int ExpectedSize = 4;
    }
    public struct ELReqMaxPowerMonth
    {
        public short header;
        public short type;
        public static ELReqMaxPowerMonth Instance(int month)
        {
            ELReqMaxPowerMonth p = new ELReqMaxPowerMonth();
            p.header = 0x454c;
            p.type = (short)(0x0200 + (month&0xFF));
            return p;
        }
    }
    public struct ELMsgMaxPowerMonth
    {
        public short header;
        public short type;
        public byte max_power1;
        public byte max_power2;
        public byte max_power3;
        public bool IsValid() { return (header == 0x454c && ELMsg.MsgType(type) == 2); }
        public int Month() { return type & 0xFF; }
        public float Float() { return ELMsg.CombineFloat(max_power1, max_power2, max_power3); }
#if NEW_FORMAT_MAX_POWER
        public byte day;
        public byte hour;
        public byte minute;
        public int When() { return (day<<16|hour<<8|minute); }
        public const int ExpectedSize = 10;
#else
        public const int ExpectedSize = 7;
#endif
    }
    public struct ELReqDateTime
    {
        public short header;
        public short type;
        public static ELReqDateTime Instance()
        {
            ELReqDateTime p = new ELReqDateTime();
            p.header = 0x454c;
            p.type = 0x0800;
            return p;
        }
    }
    public struct ELMsgDateTime
    {
        // 0x454C	0x08	0x00	YY + MM + DD + HH + mm
        public short header;
        public short type;
        public byte year;
        public byte month;
        public byte day;
        public byte hour;
        public byte minute;
        public bool IsValid() { return header == ELMsg.Head() && ELMsg.MsgType(type) == 8; }
        public const int ExpectedSize = 9;
        public DateTime DateTime()
        {
            return new DateTime(year+2000, month, day, hour, minute, 0, DateTimeKind.Local);
        }
    }
    public struct ELReqEnergyDaily
    {
        public short header;
        public short type;
        public static ELReqEnergyDaily Instance(int day)
        {
            ELReqEnergyDaily p = new ELReqEnergyDaily();
            p.header = 0x454c;
            p.type = (short)(0x0500 + (day & 0xFF));
            return p;
        }
    }
    public struct ELMsgEnergyDaily
    {
        public short header;
        public short type;
        public byte energy1;
        public byte energy2;
        public byte energy3;
        public bool IsValid() { return (header == 0x454c && ELMsg.MsgType(type) == 5); }
        public int Day() { return type & 0xFF; }
        public float Float() { return ELMsg.CombineFloat(energy1, energy2, energy3); }
        public const int ExpectedSize = 7;
    }
    public struct ELReqEnergyWeekly
    {
        public short header;
        public short type;
        public static ELReqEnergyWeekly Instance(int week)
        {
            ELReqEnergyWeekly p = new ELReqEnergyWeekly();
            p.header = 0x454c;
            p.type = (short)(0x0600 + (week & 0xFF));
            return p;
        }
    }
    public struct ELMsgEnergyWeekly
    {
        public short header;
        public short type;
        public byte energy1;
        public byte energy2;
        public byte energy3;
        public bool IsValid() { return (header == 0x454c && ELMsg.MsgType(type) == 6); }
        public int Week() { return type & 0xFF; }
        public float Float() { return ELMsg.CombineFloat(energy1, energy2, energy3); }
        public const int ExpectedSize = 7;
    }
    public struct ELReqEnergyMonthly
    {
        public short header;
        public short type;
        public static ELReqEnergyMonthly Instance(int day)
        {
            ELReqEnergyMonthly p = new ELReqEnergyMonthly();
            p.header = 0x454c;
            p.type = (short)(0x0700 + (day & 0xFF));
            return p;
        }
    }
    public struct ELMsgEnergyMonthly
    {
        public short header;
        public short type;
        public byte energy1;
        public byte energy2;
        public byte energy3;
        public bool IsValid() {return (header==0x454c && ELMsg.MsgType(type)==7);}
        public int Month() { return type & 0xFF; }
        public float Float() { return ELMsg.CombineFloat(energy1, energy2, energy3); }
        public const int ExpectedSize = 7;
    }
    public struct ELReqEnergyHourlyDaily
    {
        public short header;
        public short type;
        public static ELReqEnergyHourlyDaily Instance(int day)
        {
            ELReqEnergyHourlyDaily p = new ELReqEnergyHourlyDaily();
            p.header = 0x454c;
            p.type = (short)(0x0300 + (day & 0xFF));
            return p;
        }
    }
    public unsafe struct ELMsgEnergyHourlyDaily
    {
        public short header;
        public short type;
        public fixed byte energy[72];
        public bool IsValid() { return (header == 0x454c && ELMsg.MsgType(type) == 3); }
        public int Day() { return type & 0xFF; }
        //public float Float() { return ELMsg.CombineFloat(energy1, energy2, energy3); }
        public const int ExpectedSize = 76;

    }

}
