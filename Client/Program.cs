using System;
using System.Windows.Forms;
using Akka.Actor;

namespace WinSrvMonitor.Client
{
    static class Program
    {
        public static ActorSystem MonitorActorSystem;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MonitorActorSystem = ActorSystem.Create("WinSrvMonitorClient");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
