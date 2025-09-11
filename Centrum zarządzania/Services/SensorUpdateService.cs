using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Services
{
    public class SensorUpdateService
    {
        private CancellationTokenSource _cts;

        public ObservableCollection<LocalSensor> sensorList;

        public SensorUpdateService(ObservableCollection<LocalSensor> sensors) { 
            _cts = new CancellationTokenSource();

            sensorList = sensors;

            Task.Factory.StartNew(async () =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    foreach (var sensor in sensorList)
                    {
                        Debug.WriteLine("Sensor Update");
                        sensor.Update();
                    }
                    await Task.Delay(1000);
                }
            });
        }

        ~SensorUpdateService() { 
            _cts.Cancel();
        }
    }
}
