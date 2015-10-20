using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSrvMonitor.Client
{
    public class MovingAverage
    {
        private readonly int _nWindowSize;
        private int _nNextIndex;
        private int _nValueCount;

        private readonly float[] _values;
        private float _sum;

        public MovingAverage(int nWindowSize)
        {
            _nWindowSize = nWindowSize;
            _values = new float[_nWindowSize];

            _nNextIndex = 0;
            _sum = 0;
            _nValueCount = 0;
        }

        public float NextValue(float newValue)
        {
            if (_nValueCount < _nWindowSize)
            {
                _nValueCount++;
            }
            else
            {
                _sum -= _values[_nNextIndex];
            }

            _values[_nNextIndex] = newValue;
            _sum += newValue;
            _nNextIndex = (_nNextIndex + 1) % _nWindowSize;

            return _sum / _nValueCount;
        }
    }
}
