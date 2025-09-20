using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Services
{
    public class SensorUpdater()
    {
        public void RegisterSensor(LocalSensor sensor)
        {
            sensor.IntervalReached += c_IntervalReached;
            Debug.WriteLine(sensor.Description + " Registered");
            sensor.Update();
        }

        public void UnregisterSensor(LocalSensor sensor)
        {
            sensor.IntervalReached -= c_IntervalReached;
            Debug.WriteLine(sensor.Description + " Unregistered");
        }

        void c_IntervalReached(object sender, EventArgs e)
        {
            if(sender is LocalSensor sensor)
            {
                sensor.Update();
            }
        }
    }
}
