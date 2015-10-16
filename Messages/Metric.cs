using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSrvMonitor.Messages
{
    public class Metric
    {
        public string ServerName { get; }
        public string MetricName { get; }
        public float Value { get; }
        public DateTime Created { get; }

        public Metric(string serverName, string metricName, float value)
        {
            ServerName = serverName;
            MetricName = metricName;
            Value = value;
            Created = DateTime.Now;
        }
    }
}
