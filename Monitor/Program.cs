using System;
using System.Configuration;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Topshelf;

namespace WinSrvMonitor.Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            //RunAsCommandLine();
            RunAsService();
        }

        private static void RunAsService()
        {
            HostFactory.Run(x =>                                 
            {
                x.Service<MonitorHost>(s =>                        
                {
                    s.ConstructUsing(name => new MonitorHost());     
                    s.WhenStarted(tc => tc.Start());              
                    s.WhenStopped(tc => tc.Stop());               
                });
                x.RunAsLocalSystem();                            

                x.SetDescription("Monitors CPU and memory usage on Windows Servers");        
                x.SetDisplayName("Windows Server Monitor");                       
                x.SetServiceName("WinSrvMonitor");

                x.StartAutomatically();

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(0);
                });               
            });                                                  
        }

        private static void RunAsCommandLine()
        {
            MonitorHost host = new MonitorHost();
            host.Start();

            Console.ReadLine();
            host.Stop();

            Console.ReadLine();
        }
    }
}
