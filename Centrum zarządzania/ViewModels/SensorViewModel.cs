using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;

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

        private string? _description;
        private double _value = 0;
        private List<ThresholdData> _thresholdData = new List<ThresholdData>();
        private double _minValue = 0, _maxValue = 1;
    }
}
