using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centrum_zarządzania.ViewModels
{
    public abstract class SensorViewModel: ViewModelBase
    {
        public SensorViewModel() { }

        public SensorViewModel(Sensor sensor)
        {
            _description = sensor.Description;
            _value = sensor.Value;
            _thresholdData = sensor.thresholdData;
            _minValue = sensor._minValue;
            _maxValue = sensor._maxValue;
        }

        private string? _description { get; set; }
        private double _value { get; set; } = 0;
        private List<ThresholdData> _thresholdData { get; set; } = new List<ThresholdData>();
        private double _minValue { get; set; } = 0;
        private double _maxValue { get; set; } = 1;

        public double Value { get => _value; set { _value = Value; } }

        public abstract void Update();

        public double Ratio()
        {
            return _value - _minValue / (_maxValue - _minValue);
        }
    }
}
