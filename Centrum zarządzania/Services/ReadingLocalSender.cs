using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Centrum_zarządzania.Models;
using Centrum_zarządzania.ViewModels;

namespace Centrum_zarządzania.Services
{
    public class ReadingLocalSender
    {
        public void SubscribeSensor(LocalSensor sensor)
        {
            sensor.ReadingGenerated += c_ReadingGenerated;
        }

        public void UnsubscribeSensor(LocalSensor sensor)
        {
            sensor.ReadingGenerated -= c_ReadingGenerated;
        }

        void c_ReadingGenerated(object sender, ReadingGeneratedArgs e)
        {
            App.db.Add(new Reading(e.Name, e.Reading));
            App.db.SaveChangesAsync();
            Debug.WriteLine("Reading saved");
        }
    }
}
