using System.Runtime.Remoting.Contexts;
using Akka.Actor;
using Akka.Event;
using WinSrvMonitor.Messages;

namespace WinSrvMonitor.Collector
{
    public class MetricCollectorActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IActorRef _metricDistributerActor;

        public MetricCollectorActor(IActorRef metricDistributerActor)
        {
            _metricDistributerActor = metricDistributerActor;
            Receive<Metric>(m => HandleMetrics(m));
        }

        private void HandleMetrics(Metric m)
        {
            _log.Debug("Received metric: {0}-{1}: {2}", m.ServerName, m.MetricName, m.Value);
            _metricDistributerActor.Tell(m);
        }
    }
}
