using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Services
{
    public class SensorUpdater()
    {
        public void SubscribeSensor(LocalSensor SensorViewModel)
        {
            SensorViewModel.IntervalReached += c_IntervalReached;
            SensorViewModel.Update();
        }

        public void UnsubscribeSensor(LocalSensor SensorViewModel)
        {
            SensorViewModel.IntervalReached -= c_IntervalReached;
        }

        void c_IntervalReached(object sender, EventArgs e)
        {
            if(sender is LocalSensor SensorViewModel)
            {
                SensorViewModel.Update();
            }
        }
    }
}
