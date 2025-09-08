using System;
using System.Collections.Generic;
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

    public class Sensor
    {
        public Sensor() { }

        public Sensor(string desc, double min, double max)
        {
            description = desc;
            minValue = min;
            maxValue = max;
        }

        public int id;
        public string? description { get; set; } = "czujnik";
        public double value { get; set; } = 0;
        public List<ThresholdData> thresholdData { get; set; } = new List<ThresholdData>();
        public double minValue { get; set; } = 0;
        public double maxValue { get; set; } = 1;

        public double Ratio() {
            return value - minValue / (maxValue - minValue);
        }
    }
}
