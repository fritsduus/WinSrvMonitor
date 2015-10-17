using System;
using System.Configuration;
using System.Diagnostics;
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

            List<IActorRef> performanceCounterActors = CreatePerformanceCounterActors();

            Console.ReadLine();

            foreach(IActorRef actor in performanceCounterActors)
                actor.Tell(PoisonPill.Instance);

            MonitorActorSystem.Dispose();
            // blocks the main thread from exiting until the actor system is shut down
            //MonitorActorSystem.AwaitTermination();
            Console.ReadLine();
        }

        private static List<IActorRef> CreatePerformanceCounterActors()
        {
            int collectorIntervalMs = int.Parse(ConfigurationManager.AppSettings["CollectorIntervalMs"]);
            string monitorServer = ConfigurationManager.AppSettings["MonitorServer"];
            string metricCollectorPath = string.Format("akka.tcp://WinSrvMonitorServer@{0}:8041/user/metricCollector", monitorServer);
            ActorSelection metricCollector = MonitorActorSystem.ActorSelection(metricCollectorPath);

            List<IActorRef> performanceCounterActors = new List<IActorRef>();

            performanceCounterActors.Add(CreatePerformanceCounterActor("CPU", metricCollector,
                () => new PerformanceCounter("Processor", "% Processor Time", "_Total", true),
                collectorIntervalMs));
            performanceCounterActors.Add(CreatePerformanceCounterActor("Memory", metricCollector,
                () => new PerformanceCounter("Memory", "% Committed Bytes in Use", true),
                collectorIntervalMs));

            return performanceCounterActors;
        }

        private static IActorRef CreatePerformanceCounterActor(string metricName, ActorSelection metricCollector,
            Func<PerformanceCounter> performanceCounterGenerator, int collectorIntervalMs)
        {
            return MonitorActorSystem.ActorOf(
                Props.Create(() => new PerformanceCounterActor(metricName, metricCollector,
                    performanceCounterGenerator, collectorIntervalMs)),
                metricName.ToLowerInvariant() + "Counter");
        }
    }
}
