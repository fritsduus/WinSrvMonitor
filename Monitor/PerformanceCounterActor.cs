using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using WinSrvMonitor.Messages;
using Akka.Event;

namespace WinSrvMonitor.Monitor
{
    public class PerformanceCounterActor : ReceiveActor
    {
        private readonly string _serverName;
        private readonly string _metricName;
        private readonly Func<PerformanceCounter> _performanceCounterGenerator;
        private PerformanceCounter _counter;
        private readonly ICancelable _cancelPublishing;
        private readonly int _collectIntervalMs;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly ActorSelection _metricCollector;

        public class GatherMetrics { }

        public PerformanceCounterActor()
        {
            _serverName = "localhost";
            _metricName = "CPU";
            _collectIntervalMs = 1000;
            _metricCollector = Context.ActorSelection("akka.tcp://WinSrvMonitorServer@localhost:8041/user/metricCollector");

            _performanceCounterGenerator = () => new PerformanceCounter("Processor", "% Processor Time", "_Total", true);
            _cancelPublishing = new Cancelable(Context.System.Scheduler);
            Initialize();
        }

        public override void AroundPreStart()
        {
            base.AroundPreStart();
        }

        private void Initialize()
        {
            _counter = _performanceCounterGenerator();
            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromMilliseconds(_collectIntervalMs),
                TimeSpan.FromMilliseconds(_collectIntervalMs), Self,
                new GatherMetrics(), Self, _cancelPublishing);
            Receive<GatherMetrics>(gm => HandleGatherMetrics(gm));
        }

        public override void AroundPostStop()
        {
            try
            {
                _cancelPublishing.Cancel(false);
                _counter.Dispose();
            }
            catch { }
            finally
            {
                base.AroundPostStop();
            }
        }

        private void HandleGatherMetrics(GatherMetrics gm)
        {
            Metric metric = new Metric(_serverName, _metricName, _counter.NextValue());
            _log.Debug("Gathered metric: {0}-{1}: {2}", metric.ServerName, metric.MetricName, metric.Value);
            _metricCollector.Tell(metric);
        }
    }
}
