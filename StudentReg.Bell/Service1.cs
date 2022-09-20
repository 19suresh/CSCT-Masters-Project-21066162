using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace StudentReg.Bell
{
    public partial class Service1 : ServiceBase
    {
        readonly System.Timers.Timer timer = new System.Timers.Timer(30000);

        const string _0730 = "0730";
        const string _0750 = "0750";


        public Service1()
        {
            InitializeComponent();
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
        }

        private void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            string curTime = DateTime.Now.ToString("HHmm");

            if (curTime == _0730 ||
                curTime == _0750)
            {
                RingBell();
            }
        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
        }

        public void OnDebug()
        {

            Debug.WriteLine(DateTime.Now.ToString());
            timer.Start();

        }

        protected override void OnStop()
        {
        }

        private void RingBell()
        {
            Debug.WriteLine(DateTime.Now.ToString());

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Beep();
                }
                Thread.Sleep(500);
            }
        }
    }
}
