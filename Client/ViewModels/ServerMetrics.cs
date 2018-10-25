

using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WinSrvMonitor.Client.Annotations;

namespace WinSrvMonitor.Client.ViewModels
{
    public class ServerMetrics : INotifyPropertyChanged
    {
        public string Group { get; }
        public string Name { get; }
        public Metric Cpu { get; }
        public Metric Memory { get; }
        public Metric RequestsPerSec { get; }
        public Metric PackagesPerSec { get; }
        public Metric BytesPerSec { get; }
        public Metric ActiveConnections { get; }
        public bool HasError => Error != null;
        public string Error { get; private set; }
        private readonly Metric[] _metrics;

        public ServerMetrics(string group, string name)
        {
            Group = group;
            Name = name;
            Cpu = new Metric("Cpu");
            Memory = new Metric("Memory");
            RequestsPerSec = new Metric("RequestsPerSec", minChange: 1);
            PackagesPerSec = new Metric("PackagesPerSec", minChange: 1);
            BytesPerSec = new Metric("BytesPerSec", 1024*1024/8, 1); // Bytes / sec --> Mb/s --> Mbit/s
            ActiveConnections = new Metric("ActiveConnections", minChange: 1);
            _metrics = new[] { Cpu, Memory, RequestsPerSec, PackagesPerSec, BytesPerSec, ActiveConnections };
        }

        public void UpdateMetric(Messages.Metric metric)
        {
            Metric m = _metrics.FirstOrDefault(mm => mm.Name == metric.MetricName);
            if (string.IsNullOrEmpty(metric.Error))
            {
                Error = null;
                m?.UpdateValue(metric.Value);
            }
            else
            {
                Error = metric.Error;
                m?.SetError(metric.Error);
            }
            OnPropertyChanged(m?.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
