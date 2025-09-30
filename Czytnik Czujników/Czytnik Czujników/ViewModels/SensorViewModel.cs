using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;

namespace Czytnik_Czujników.ViewModels
{
    public class SensorViewModel : INotifyPropertyChanged
    {
        private double _min, _max, _value;
        private string _name;

        public double Min { get => _min; set { _min = value; OnPropertyChanged("Min"); } }
        public double Max { get => _max; set { _min = value; OnPropertyChanged("Max"); } }
        public double Value { get => _value; set { _min = value; OnPropertyChanged("Value"); } }
        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SensorViewModel(Sensor s, double value)
        {
            _min = s.LowThreshold;
            _max = s.HighThreshold;
            _name = $"{s.DeviceName}/{s.Name}";
            _value = value;
        }
    }
}
