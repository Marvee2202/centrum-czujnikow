using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Centrum_zarządzania.Models
{
    [PrimaryKey("Id")]
    public class Reading
    {       
        public int Id { get; set; }
        //TODO: implementacja historii odczytów - klasa reprezentująca pojedynczy odczyt
        public string SensorName { get; set; }
        public double Value { get; set; }
        public DateTime ReadingTime { get; set; }

        public Reading(string sensorName, double value)
        {
            this.SensorName = sensorName;
            this.Value = value;
            this.ReadingTime = DateTime.Now;
        }
    }
}
