using Akka.Actor;
using WinSrvMonitor.Collector;

namespace WinSrvMonitor.Server
{
    public class ServerHost
    {
        private ActorSystem _monitorActorSystem;
        private IActorRef _metricDistributerActor;
        private IActorRef _metricCollectorActor;

        public void Start()
        {
            _monitorActorSystem = ActorSystem.Create("WinSrvMonitorServer");

            _metricDistributerActor = _monitorActorSystem.ActorOf<MetricDistributerActor>("metricDistributer");

            _metricCollectorActor = _monitorActorSystem.ActorOf(
                Props.Create(() => new MetricCollectorActor(_metricDistributerActor)),
                "metricCollector");
        }

        public void Stop()
        { 
            _metricCollectorActor.Tell(PoisonPill.Instance);
            _metricDistributerActor.Tell(PoisonPill.Instance);

            _monitorActorSystem.Dispose();
        }
    }
}
