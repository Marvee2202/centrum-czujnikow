using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;
using Microsoft.EntityFrameworkCore;

namespace Czytnik_Czujników.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //public ObservableCollection<DeviceViewModel> Devices { get; set; } = new ObservableCollection<DeviceViewModel>();
        public ObservableCollection<SensorViewModel> Sensors { get; set; } = new ObservableCollection<SensorViewModel>();

        private int _interval = 1000;

        public async Task GenerateList()
        {
            while (true)
            {
                var delay = Task.Delay(_interval);

                ObservableCollection<DeviceViewModel> tmpDevices = new ObservableCollection<DeviceViewModel>();
                ObservableCollection<SensorViewModel> tmpSensors = new ObservableCollection<SensorViewModel>();
                foreach (Device d in App.db.devices.Include(device => device.Sensors).ToList())
                {
                    //tmpDevices.Add(new DeviceViewModel(d));
                    foreach (Sensor s in d.Sensors)
                    {
                        double val = App.db.readings.Where(r => r.SensorId == s.Id).OrderByDescending(r => r.ReadingTime).FirstOrDefault().Value;
                        tmpSensors.Add(new SensorViewModel(s, val));
                    }
                }
                ;
                //Devices.Clear();
                Sensors.Clear();
                foreach (var s in tmpSensors)
                {
                    Sensors.Add(s);
                }
                Debug.WriteLine(Sensors.Count);
                Debug.WriteLine("Update complete");
                await delay;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler IntervalReached;
        protected virtual void OnIntervalReached(object sender, EventArgs e)
        {
            IntervalReached?.Invoke(this, new EventArgs());
        }
    }
}
