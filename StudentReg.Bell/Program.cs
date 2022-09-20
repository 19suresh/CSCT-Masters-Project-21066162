using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace StudentReg.Bell
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
           


#if DEBUG
            Service1 svc = new Service1();
            svc.OnDebug();
            System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
#else
           ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
#endif

        }



    }
}
