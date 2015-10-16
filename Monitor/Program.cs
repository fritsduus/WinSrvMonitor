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
            MonitorActorSystem = ActorSystem.Create("WinSrvMonitorMonitor");

            ActorSelection metricCollector = 
                MonitorActorSystem.ActorSelection("akka.tcp://WinSrvMonitorServer@localhost:8041/user/metricCollector");

            IActorRef performanceCounterActor = MonitorActorSystem.ActorOf(
                Props.Create(() => new PerformanceCounterActor(metricCollector)),
                "cpuCounter");

            Console.ReadLine();

            performanceCounterActor.Tell(PoisonPill.Instance);

            MonitorActorSystem.Dispose();
            // blocks the main thread from exiting until the actor system is shut down
            //MonitorActorSystem.AwaitTermination();
            Console.ReadLine();
        }
    }
}
