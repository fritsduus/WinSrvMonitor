using System.ComponentModel;

namespace WinSrvMonitor.Client
{
    public class MetricViewItem : INotifyPropertyChanged
    {
        public string Server { get; set; }
        public string Metric { get; set; }

        private float metricValue;
        public float Value
        {
            get { return metricValue; }
            set
            {
                if (metricValue != value)
                {
                    metricValue = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
