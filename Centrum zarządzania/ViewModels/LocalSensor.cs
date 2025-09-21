using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Centrum_zarządzania.ViewModels
{
    public class ReadingGeneratedArgs
    {
        public ReadingGeneratedArgs(string desc, double val) 
        {
            Name = desc;
            Reading = val;
        }
        public string Name { get; }
        public double Reading { get; }
    }

    public class LocalSensor : Sensor
    {
        public event EventHandler IntervalReached;

        protected virtual void OnIntervalReached(object sender, EventArgs e)
        {
            IntervalReached?.Invoke(this, new EventArgs());
        }

        //public delegate void IntervalReachedEventHandler(object sender, EventArgs e);

        public event ReadingGeneratedEventHandler ReadingGenerated;

        public delegate void ReadingGeneratedEventHandler(object sender, ReadingGeneratedArgs arg);

        protected virtual void OnReadingGenerated(ReadingGeneratedArgs arg)
        {
            ReadingGenerated?.Invoke(this, arg);
        }

        private string _path = "";

        public string Path { get => _path; set { _path = value; } }

        private int _interval = 100;
        public int Interval { get => _interval; set { _interval = value; OnPropertyChanged("Delay"); } }

        public void EditSensor()
        {
            App.Main.SwitchToSensorEdit(this);
        }

        public async void Update()
        {
            try
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                CancellationToken cancellationToken = cts.Token;
                Task.Factory.StartNew(async () =>
                {
                    cts.CancelAfter(_interval * 2);
                    await Task.Delay(_interval);
                    OnIntervalReached(this, EventArgs.Empty);
                });
                if (!_ready) return;
                Process senseRead = Process.Start(_path);
                senseRead.StartInfo.UseShellExecute = false;
                senseRead.StartInfo.CreateNoWindow = true;
                senseRead.StartInfo.RedirectStandardOutput = true;
                senseRead.StartInfo.RedirectStandardError = true;
                senseRead.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                _ready = false;
                senseRead.Start();
                await senseRead.WaitForExitAsync(cancellationToken);

                Value = Convert.ToDouble(senseRead.StandardOutput.ReadToEnd(), System.Globalization.CultureInfo.InvariantCulture);
                OnReadingGenerated(new ReadingGeneratedArgs(Description, Value));
                senseRead.Kill();
                _ready = true;
            }
            catch(Exception e) {
                Debug.Write(e);
                _ready = true;
            }
        }

        private bool _ready = true;

        public LocalSensor(string desc, string path, double min, double max, int interval) : base(desc, min, max)
        {
            _path = path;
            _interval = interval;
        }

        public LocalSensor() : base()
        {

        }
    };
}
