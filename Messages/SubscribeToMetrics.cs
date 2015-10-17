using Akka.Actor;

namespace WinSrvMonitor.Messages
{
    public class SubscribeToMetrics
    {
        public SubscribeToMetrics(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; private set; }
    }
}
