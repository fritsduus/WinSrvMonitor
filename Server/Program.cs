using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WinSrvMonitor.Server
{
    class Program
    {
        public static ActorSystem MonitorActorSystem;

        static void Main(string[] args)
        {
            MonitorActorSystem = ActorSystem.Create("WinSrvMonitor");

            IActorRef performanceCounterActor = MonitorActorSystem.ActorOf<PerformanceCounter>

            Console.ReadLine();

            MonitorActorSystem.Dispose();
            // blocks the main thread from exiting until the actor system is shut down
            //MonitorActorSystem.AwaitTermination();
        }
    }
}
