using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WinSrvMonitor.Monitor
{
    class Program
    {
        public static ActorSystem MonitorActorSystem;

        static void Main(string[] args)
        {
            MonitorActorSystem = ActorSystem.Create("WinSrvMonitor");

            // blocks the main thread from exiting until the actor system is shut down
            MonitorActorSystem.AwaitTermination();
        }
    }
}
