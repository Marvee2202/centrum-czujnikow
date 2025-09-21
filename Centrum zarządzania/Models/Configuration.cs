using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Models
{
    public class Configuration
    {
        public ObservableCollection<LocalSensor> LocalSensorList = new ObservableCollection<LocalSensor>();

        public Configuration() { }
        public Configuration(ObservableCollection<LocalSensor> list)
        {
            LocalSensorList = list;
        }
    }
}
