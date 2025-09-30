using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Centrum_zarządzania.Models
{
    public class Reading
    {       
        public int Id { get; set; }
        //TODO: implementacja historii odczytów - klasa reprezentująca pojedynczy odczyt
        //public string SensorName { get; set; }
        public double Value { get; set; }
        public DateTime ReadingTime { get; set; }

        public virtual Sensor Sensor { get; set; }

        public int SensorId { get; set; }

        public Reading() { }

        public Reading(Sensor sensor, double value)
        {
            Sensor = sensor;
            SensorId = sensor.Id;
            this.Value = value;
            this.ReadingTime = DateTime.Now;
        }
    }
}
