using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Centrum_zarządzania.Models
{
    public class Sensor
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public string Name { get; set; }
        public string ConnectionData { get; set; }
        public ICollection<Reading> Readings { get; } = new List<Reading>();
    }
}
