using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public class RequestClassModel
    {
        public string? RugestName { get; set; }
        public string? DistName { get; set; }
        public string? Services { get; set; }
        public string? Statues { get; set; }
        public DateOnly? Date { get; set; }
    }
}
