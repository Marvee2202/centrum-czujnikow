using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centrum_zarządzania.ViewModels
{
    public enum ReadingStatus { OK, Warning, Critical };

    public class ThresholdData
    {
        ThresholdData() { }

        ThresholdData(int threshold, ReadingStatus status, bool lowBound)
        {
            value = threshold;
            severity = status;
            triggersBelow = lowBound;
        }

        public double value = 0;
        public ReadingStatus severity = ReadingStatus.OK;
        public bool triggersBelow = false;
    }

    public class Sensor : INotifyPropertyChanged
    {
        public Sensor() { }

        public Sensor(string desc, double min, double max)
        {
            _description = desc;
            _minValue = min;
            _maxValue = max;
        }

        public int id;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string? _description { get; set; } = "czujnik";

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private double _value { get; set; } = 0;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        public List<ThresholdData> thresholdData { get; set; } = new List<ThresholdData>();
        public double _minValue { get; set; } = 0;
        public double _maxValue { get; set; } = 1;

        public double Ratio() {
            return _value - _minValue / (_maxValue - _minValue);
        }
    }
}
