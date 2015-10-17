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
    public class MonitorHost
    {
        private ActorSystem _monitorActorSystem;
        private List<IActorRef> _performanceCounterActors;

        public void Start()
        {
            _monitorActorSystem = ActorSystem.Create("WinSrvMonitorMonitor");

            _performanceCounterActors = CreatePerformanceCounterActors();
        }

        public void Stop()
        {
            if (_performanceCounterActors != null)
            {
                foreach (IActorRef actor in _performanceCounterActors)
                    actor.Tell(PoisonPill.Instance);

                _monitorActorSystem.Dispose();
            }
            _performanceCounterActors = null;
        }

        private List<IActorRef> CreatePerformanceCounterActors()
        {
            int collectorIntervalMs = int.Parse(ConfigurationManager.AppSettings["CollectorIntervalMs"]);
            string monitorServer = ConfigurationManager.AppSettings["MonitorServer"];
            string metricCollectorPath = string.Format("akka.tcp://WinSrvMonitorServer@{0}:8041/user/metricCollector", monitorServer);
            ActorSelection metricCollector = _monitorActorSystem.ActorSelection(metricCollectorPath);

            List<IActorRef> performanceCounterActors = new List<IActorRef>();

            performanceCounterActors.Add(CreatePerformanceCounterActor("CPU", metricCollector,
                () => new PerformanceCounter("Processor", "% Processor Time", "_Total", true),
                collectorIntervalMs));
            performanceCounterActors.Add(CreatePerformanceCounterActor("Memory", metricCollector,
                () => new PerformanceCounter("Memory", "% Committed Bytes in Use", true),
                collectorIntervalMs));

            return performanceCounterActors;
        }

        private IActorRef CreatePerformanceCounterActor(string metricName, ActorSelection metricCollector,
            Func<PerformanceCounter> performanceCounterGenerator, int collectorIntervalMs)
        {
            return _monitorActorSystem.ActorOf(
                Props.Create(() => new PerformanceCounterActor(metricName, metricCollector,
                    performanceCounterGenerator, collectorIntervalMs)),
                metricName.ToLowerInvariant() + "Counter");
        }
    }
}
