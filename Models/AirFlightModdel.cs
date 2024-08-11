using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public class AirFlightModdel
    {
        public string? Carrier { get; set; }
        public DateOnly Date { get; set; }
        public string? Class { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? ETD { get; set; }
        public string? ETA { get; set; }
        public int Adult { get; set; }
        public int Child { get; set; }
        public int Infant { get; set; }
        public string? Notes { get; set; }
        public string? price { get; set; }
    }
}
