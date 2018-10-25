using System.Collections.Generic;
using Akka.Actor;
using WinSrvMonitor.Messages;

namespace WinSrvMonitor.Collector
{
    public class MetricDistributerActor : ReceiveActor
    {
        private readonly HashSet<IActorRef> _subscriptions;

        public MetricDistributerActor()
        {
            _subscriptions = new HashSet<IActorRef>();
            Receive<SubscribeToMetrics>(stm => HandleSubscribe(stm));
            Receive<UnsubscribeToMetrics>(utm => HandleUnsubcribe(utm));
            Receive<Metric>(m => HandleMetric(m));
        }

        private void HandleMetric(Metric m)
        {
            foreach (IActorRef actor in _subscriptions)
                actor.Tell(m);
        }

        private void HandleUnsubcribe(UnsubscribeToMetrics utm)
        {
            _subscriptions.Remove(utm.Subscriber);
        }

        private void HandleSubscribe(SubscribeToMetrics stm)
        {
            _subscriptions.Add(stm.Subscriber);
        }
    }
}
