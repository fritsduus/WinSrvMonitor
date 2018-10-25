using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSrvMonitor.Messages
{
    public class Metric
    {
        public string GroupName { get; }
        public string ServerName { get; }
        public string MetricName { get; }
        public float Value { get; }
        public DateTime Created { get; }
        public string Error { get; }

        public Metric(string groupName, string serverName, string metricName, float value)
        {
            GroupName = groupName;
            ServerName = serverName;
            MetricName = metricName;
            Value = value;
            Created = DateTime.Now;
        }

        public Metric(string groupName, string serverName, string metricName, string error)
        {
            GroupName = groupName;
            ServerName = serverName;
            MetricName = metricName;
            Error = error;
            Created = DateTime.Now;
        }
    }
}
