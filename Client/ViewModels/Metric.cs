using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WinSrvMonitor.Client.Annotations;

namespace WinSrvMonitor.Client.ViewModels
{
    public class Metric : INotifyPropertyChanged
    {
        private readonly int _scale;
        private readonly float _minChange;
        public string Name { get; }
        public float Value { get; private set; }
        public float MovingAverage { get; private set; }
        public bool HasError => Error != null;
        public string Error { get; private set; }

        private readonly MovingAverage _movingAverage = new MovingAverage(10);

        public Metric(string name, int scale = 1, float minChange = 0.01f)
        {
            _scale = scale;
            _minChange = minChange;
            Name = name;
        }

        public void UpdateValue(float value)
        {
            value = value/_scale;

            if (Math.Abs(Value - value) > _minChange)
            {
                Value = value;
                OnPropertyChanged(nameof(Value));
            }

            float newAvg = _movingAverage.NextValue(value);
            if (Math.Abs(MovingAverage - newAvg) > _minChange)
            {
                MovingAverage = newAvg;
                OnPropertyChanged(nameof(MovingAverage));
            }

            if (Error != null)
            {
                Error = null;
                OnPropertyChanged(nameof(Error));
            }
        }

        public void SetError(string error)
        {
            Error = error;
            OnPropertyChanged(nameof(Error));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
