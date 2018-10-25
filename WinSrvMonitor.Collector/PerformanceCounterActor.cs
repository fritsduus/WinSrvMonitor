using System;
using System.Diagnostics;
using Akka.Actor;
using Akka.Event;
using WinSrvMonitor.Messages;

namespace WinSrvMonitor.Collector
{
    public class PerformanceCounterActor : ReceiveActor
    {
        private readonly string _serverName;
        private readonly string _groupName;
        private readonly string _metricName;
        private readonly Func<PerformanceCounter> _performanceCounterGenerator;
        private PerformanceCounter _counter;
        private readonly ICancelable _cancelPublishing;
        private readonly int _collectIntervalMs;
        private readonly ILoggingAdapter _log = Logging.GetLogger(Context);
        private readonly IActorRef _metricCollector;

        public class GatherMetrics
        {
        }

        public PerformanceCounterActor(string groupName, string serverName, string metricName, IActorRef metricCollector,
            Func<PerformanceCounter> performanceCounterGenerator, int collectIntervalMs)
        {
            _serverName = serverName ?? System.Net.Dns.GetHostName();
            _groupName = groupName;
            _metricName = metricName;
            _collectIntervalMs = collectIntervalMs;
            _metricCollector = metricCollector;

            _performanceCounterGenerator = performanceCounterGenerator;
            _cancelPublishing = new Cancelable(Context.System.Scheduler);

            Receive<GatherMetrics>(gm => HandleGatherMetrics(gm));
        }

        protected override void PreStart()
        {
            Initialize();
            base.PreStart();
        }

        private void Initialize()
        {
            _counter = _performanceCounterGenerator();

            Context.System.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromMilliseconds(_collectIntervalMs),
                TimeSpan.FromMilliseconds(_collectIntervalMs), Self,
                new GatherMetrics(), Self, _cancelPublishing);
        }

        protected override void PostStop()
        {
            try
            {
                _cancelPublishing.Cancel(false);
                _counter.Dispose();
            }
            catch
            {
            }
            finally
            {
                base.PostStop();
            }
        }

        private void HandleGatherMetrics(GatherMetrics gm)
        {
            Metric metric = CreateMetric();
            _log.Debug("Gathered metric: {0}-{1}: {2}", metric.ServerName, metric.MetricName, metric.Value);
            _metricCollector.Tell(metric);
        }

        private Metric CreateMetric()
        {
            try
            {
                return new Metric(_groupName, _serverName, _metricName, _counter.NextValue());
            }
            catch (Exception ex)
            {
                return new Metric(_groupName, _serverName, _metricName, ex.Message);
            }
        }
    }
}
