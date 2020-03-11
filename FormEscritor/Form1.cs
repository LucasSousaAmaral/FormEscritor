using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FormEscritor
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public PerformanceCounter ramCounter;

        List<string> Levels = new List<string>();
        Random random = new Random();

        public Form1()
        {
            Levels.Add("Warning");
            Levels.Add("Error");
            Levels.Add("Information");

            InitializeComponent();

            string path = @"D:\TestLogs\LogTestDifferent.txt";

            t.Interval = 500; // specify interval time as you want
            t.Tick += new EventHandler(timer_Tick);
            t.Start();

            void timer_Tick(object sender, EventArgs e)
            {
                int nextone = random.Next(0, 3);
                string txt = $"{DateTime.Now} { Levels[nextone] }  < Param > Fderivs.Server.FloatServer | Fderivs.Lib.ModelControl.ContextReader_DbFloat_CurvePackExtension | Unable to find most recent Fderivs.Lib.Model.DividendCurve for CurveKey = TypeName:Fderivs.Lib.Model.DividendCurve,CurveGroupId: 2,Details = VVAR3(Returned 0) </ Param > < DataItem type = System.XmlData time = 2020 - 03 - 06T18: 22:08.0820857 + 00:00 sourceHealthServiceId = F97E56FC - 4EC7 - 2D58 - 5B7B - 067FD93E6E37 >< EventData xmlns = http://schemas.microsoft.com/win/2004/08/events/event><Data>Fderivs.Server.FloatServer|Fderivs.Lib.ModelControl.ContextReader_DbFloat_CurvePackExtension|Unable to find most recent Fderivs.Lib.Model.DividendCurve for CurveKey=TypeName:Fderivs.Lib.Model.DividendCurve,CurveGroupId:2,Details=VVAR3s (Returned 0)</Data></EventData></DataItem>	165	Fderivs.Server.FloatServer|Fderivs.Lib.ModelControl.ContextReader_DbFloat_CurvePackExtension|Unable to find most recent Fderivs.Lib.Model.DividendCurve for CurveKey= TypeName:Fderivs.Lib.Model.DividendCurve,CurveGroupId:2,Details=VVAR3 (Returned 0)			N/A	LCID;1033 Locale;ENU Message;%1	Event	/subscriptions/cf363617-331c-4d15-9af2-c017dd671076/resourcegroups/rg-brazilsouth-vm/providers/microsoft.compute/virtualmachines/fderivsapp20	e1d177cc-c956-4c8c-84cc-753531a175e1	OpsManager	00000000-0000-0000-0000-000000000001	AOI-e1d177cc-c956-4c8c-84cc-753531a175e1";
                WriteToFileThreadSafe(txt, path);
            }
        }
        private static ReaderWriterLockSlim _readWriteLock = new ReaderWriterLockSlim();

        public void WriteToFileThreadSafe(string text, string path)
        {

            // Set Status to Locked
            _readWriteLock.EnterWriteLock();
            try
            {
                // Append text to the file
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(text);
                    sw.Close();
                }
            }
            finally
            {
                // Release lock
                _readWriteLock.ExitWriteLock();
            }
        }
    }
}
