﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Akka.Actor;

namespace WinSrvMonitor.Collector
{
    public class MetricCollectorFactory
    {
        private readonly ActorSystem _actorSystem;

        public MetricCollectorFactory(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }

        public List<IActorRef> CreatePerformanceCounterActors(string group, string server,
            int collectorIntervalMs, IActorRef metricCollector)
        {
            List<IActorRef> performanceCounterActors = new List<IActorRef>();

            server = server ?? System.Net.Dns.GetHostName();
            CreatePerformanceCounterActor(performanceCounterActors, group, server, "Cpu", metricCollector,
                () => new PerformanceCounter("Processor", "% Processor Time", "_Total", true)
                { MachineName = server },
                collectorIntervalMs);
            CreatePerformanceCounterActor(performanceCounterActors, group, server, "Memory", metricCollector,
                () => new PerformanceCounter("Memory", "% Committed Bytes in Use", true)
                { MachineName = server },
                collectorIntervalMs);
            CreatePerformanceCounterActor(performanceCounterActors, group, server, "RequestsPerSec", metricCollector,
                () => new PerformanceCounter("W3SVC_W3WP", "Requests / Sec", "_Total", true)
                { MachineName = server },
                collectorIntervalMs);

            CreateNetworkCounter(group, server, collectorIntervalMs, metricCollector,
                performanceCounterActors, counterName: "Packets/sec", metricName: "PackagesPerSec");

            CreateNetworkCounter(group, server, collectorIntervalMs, metricCollector,
                performanceCounterActors, counterName: "Bytes Total/sec", metricName: "BytesPerSec");

            CreatePerformanceCounterActor(performanceCounterActors, group, server, "ActiveConnections", metricCollector,
                () => new PerformanceCounter("TCPv4", "Connections Active", "", true)
                { MachineName = server },
                collectorIntervalMs);

            return performanceCounterActors;
        }

        private void CreateNetworkCounter(string group, string server, int collectorIntervalMs, IActorRef metricCollector,
            List<IActorRef> performanceCounterActors, string counterName, string metricName)
        {
            Func<PerformanceCounter> counterFunc = null;
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface", server);
            try
            {
                string[] instancenames = category.GetInstanceNames();
                counterFunc = FindCounterWithValues(instancenames, counterName, server);

                if (counterFunc == null)
                {
                    System.Threading.Thread.Sleep(500);
                    counterFunc = FindCounterWithValues(instancenames, counterName, server);
                    if (counterFunc == null)
                    {
                        System.Threading.Thread.Sleep(1000);
                        counterFunc = FindCounterWithValues(instancenames, counterName, server);
                    }
                }
            }
            catch
            {
                int i = 0;
                i++;
            }

            if (counterFunc != null)
            {
                CreatePerformanceCounterActor(performanceCounterActors, group, server,
                    metricName, metricCollector, counterFunc, collectorIntervalMs);
            }
            else
            {
                int i = 0;
                i++;
            }
        }

        private Func<PerformanceCounter> FindCounterWithValues(string[] instancenames, string counterName, string server)
        {
            Func<PerformanceCounter> selectedCounterFunc = null;
            float currentMax = 0;

            foreach (string name in instancenames)
            {
                Func<PerformanceCounter> counterFunc = () => new PerformanceCounter("Network Interface",
                        counterName, name, true)
                { MachineName = server };

                var counter = counterFunc();
                float value = counter.NextValue();
                value += counter.NextValue();
                if (value > currentMax)
                {
                    selectedCounterFunc = counterFunc;
                    currentMax = value;
                }
            }
            return selectedCounterFunc;
        }

        private bool DoesPerformanceCounterExist(Func<PerformanceCounter> counterFunc)
        {
            try
            {
                var performanceCounter = counterFunc();
                float test = performanceCounter.NextValue();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not exist"))
                    return false;
            }
            return true;
        }

        private void CreatePerformanceCounterActor(List<IActorRef> performanceCounterActors,
            string group, string server, string metricName, IActorRef metricCollector,
            Func<PerformanceCounter> performanceCounterGenerator, int collectorIntervalMs)
        {
            if (!DoesPerformanceCounterExist(performanceCounterGenerator))
                return;

            IActorRef actor = _actorSystem.ActorOf(
                Props.Create(() => new PerformanceCounterActor(group, server, metricName,
                    metricCollector, performanceCounterGenerator, collectorIntervalMs)),
                server + metricName.ToLowerInvariant() + "Counter");

            performanceCounterActors.Add(actor);
        }
    }
}
