using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Models
{
    public enum ReadingStatus { OK, Warning, Critical };

    public class ThresholdData
    {
        public double value;
        public ReadingStatus severity;
        public bool triggersBelow = false;
    }
    
    public class Sensor : ViewModelBase
    {
        public string? description;
        public double value = 0;
        public List<ThresholdData> thresholdData = new List<ThresholdData>();
        public double minValue = 0, maxValue = 1;
    }
}
