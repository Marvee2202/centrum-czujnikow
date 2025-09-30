using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;

namespace Czytnik_Czujników.ViewModels
{
    public class DeviceViewModel
    {
        public ObservableCollection<Sensor> Sensors { get; set; } = new ObservableCollection<Sensor>();

        public DeviceViewModel(Device d) 
        {
            foreach (Sensor s in d.Sensors)
            {
                Sensors.Add(s);
            }
        }
    }
}
