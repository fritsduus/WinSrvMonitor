using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using WinSrvMonitor.Messages;

namespace WinSrvMonitor.Server
{
    public class MetricCollectorActor : ReceiveActor
    {
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);

        public MetricCollectorActor()
        {
            Receive<Metric>(m => HandleMetrics(m));
        }

        private void HandleMetrics(Metric m)
        {
            _log.Debug("Received metric: {0}-{1}: {2}", m.ServerName, m.MetricName, m.Value);
        }
    }
}
