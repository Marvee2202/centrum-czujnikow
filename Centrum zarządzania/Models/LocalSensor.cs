using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Models
{
    public class LocalSensor : Sensor
    {
        private string _path;

        public string Path { get => _path; }

        public void EditSensor()
        {
            App.Main.SwitchToSensorEdit(this);
        }

        public void Update()
        {
            try
            {
                System.Diagnostics.Process senseRead = System.Diagnostics.Process.Start(_path);
                senseRead.StartInfo.UseShellExecute = false;
                senseRead.StartInfo.CreateNoWindow = true;
                senseRead.StartInfo.RedirectStandardOutput = true;
                senseRead.StartInfo.RedirectStandardError = true;
                senseRead.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                senseRead.Start();
                senseRead.WaitForExit();

                Value = Convert.ToDouble(senseRead.StandardOutput.ReadToEnd(), System.Globalization.CultureInfo.InvariantCulture);
                Debug.Write(Value);
            }
            catch(Exception e) {
                Debug.Write(e);
            }
        }

        public LocalSensor(string desc, string path, double min, double max) : base(desc, min, max)
        {
            _path = path;
        }
    };
}
