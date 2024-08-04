using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public class TransportaionServiceModdel
    {
        public string? CarType { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Nos { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly time { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Notes { get; set; }
    }
}
