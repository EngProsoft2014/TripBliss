﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels.TravelAgenciesVieModels.CreateRequest
{
    public class TransportaitionRequestDetailesModdel
    {
        public DateOnly Date { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Vechil { get; set; }
        public int? No { get; set; }
    }
}