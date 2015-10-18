using System;
using Topshelf;

namespace WinSrvMonitor.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "commandline")
                RunAsCommandLine();
            else
                RunAsService();
        }

        private static void RunAsService()
        {
            HostFactory.Run(x =>
            {
                x.Service<ServerHost>(s =>
                {
                    s.ConstructUsing(name => new ServerHost());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Collects data from WinSrvMonitor.Monitor's and distributes the data to WinSrvMonitor.Client's");
                x.SetDisplayName("Windows Server Monitor Server");
                x.SetServiceName("WinSrvMonitorServer");

                x.StartAutomatically();

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(0);
                });
            });
        }

        private static void RunAsCommandLine()
        {
            ServerHost serverHost = new ServerHost();
            serverHost.Start();

            Console.ReadLine();
            serverHost.Stop();

            Console.ReadLine();
        }
    }
}
