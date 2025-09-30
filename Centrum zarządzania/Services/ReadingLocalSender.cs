using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Services
{
    public class ReadingLocalSender
    {
        private bool queueLocked = false;

        public void SubscribeSensor(LocalSensor SensorViewModel)
        {
            SensorViewModel.ReadingGenerated += c_ReadingGenerated;
        }

        public void UnsubscribeSensor(LocalSensor SensorViewModel)
        {
            SensorViewModel.ReadingGenerated -= c_ReadingGenerated;
        }

        void c_ReadingGenerated(object sender, ReadingGeneratedArgs e)
        {
            Reading r = new Reading(e.Sensor, e.Reading);
            while (queueLocked)
            {
                Thread.Sleep(1);
            }
            _readingQueue.Add(r);
        }

        private CancellationTokenSource _cts = new CancellationTokenSource();

        private List<Reading> _readingQueue = new List<Reading>();

        public ReadingLocalSender()
        {
            _cts = new CancellationTokenSource();
            Task.Factory.StartNew(() => ProcessNewData());
        }

        ~ReadingLocalSender()
        {
            _cts.Cancel();
        }

        async void ProcessNewData()
        {
            CancellationToken token = _cts.Token;
            while (!token.IsCancellationRequested)
            {
                int records = 0;
                queueLocked = true;
                foreach (Reading x in _readingQueue)
                {
                    records++;
                    App.db.Add<Reading>(x);
                }
                _readingQueue.Clear();
                queueLocked = false;
                if(records > 0) await App.db.SaveChangesAsync();
                Debug.WriteLine($"{records} Readings sent.");
                Thread.Sleep(1000);
            }
        }
    }
}
