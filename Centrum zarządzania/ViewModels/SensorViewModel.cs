using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centrum_zarządzania.ViewModels
{
    public class SensorViewModel: ViewModelBase
    {
        public SensorViewModel() { }

        public SensorViewModel(Sensor sensor)
        {
            _description = sensor.description;
            _value = sensor.value;
            _thresholdData = sensor.thresholdData;
            _minValue = sensor.minValue;
            _maxValue = sensor.maxValue;
        }

        private string? _description { get; set; }
        private double _value { get; set; } = 0;
        private List<ThresholdData> _thresholdData { get; set; } = new List<ThresholdData>();
        private double _minValue { get; set; } = 0;
        private double _maxValue { get; set; } = 1;

        public double Ratio()
        {
            return _value - _minValue / (_maxValue - _minValue);
        }
    }
}
