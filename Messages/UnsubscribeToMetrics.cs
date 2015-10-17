using Akka.Actor;

namespace WinSrvMonitor.Messages
{
    public class UnsubscribeToMetrics
    {
        public UnsubscribeToMetrics(IActorRef subscriber)
        {
            Subscriber = subscriber;
        }

        public IActorRef Subscriber { get; private set; }
    }
}
