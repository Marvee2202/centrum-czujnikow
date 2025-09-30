using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Centrum_zarządzania.Models
{
    public class Device
    {
        public string Name { get; set; }
        public ICollection<Sensor> Sensors { get; } = new List<Sensor>();

        public Device(string name)
        {
            Name = name;
        }
    }
}
