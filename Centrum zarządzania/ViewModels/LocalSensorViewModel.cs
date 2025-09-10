using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;

namespace Centrum_zarządzania.ViewModels
{
    public class LocalSensorViewModel : SensorViewModel
    {
        private string _path;

        public LocalSensorViewModel(LocalSensor sensor) : base(sensor)
        {
            _path = sensor.Path;
        }

        override public void Update()
        {
            System.Diagnostics.Process senseRead = System.Diagnostics.Process.Start(_path);
            senseRead.StartInfo.UseShellExecute = false;
            senseRead.StartInfo.CreateNoWindow = true;
            senseRead.StartInfo.RedirectStandardOutput = true;

            senseRead.Start();
            senseRead.WaitForExit();

            Value = Convert.ToDouble(senseRead.StandardOutput.ReadToEnd());
        }
    }
}
