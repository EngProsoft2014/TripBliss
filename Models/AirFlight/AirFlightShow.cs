using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.AirFlight
{
    public class AirFlightShow
    {
        public RequestTravelAgencyAirFlightRequest? AirFlightRequest { get; set; }
        public string? AirLine { get; set; }
        public DateTime Date { get; set; }
        public string? AirportFrom { get; set; }
        public string? AirportTo { get; set; }
        public int TotalPerson { get { return AirFlightRequest!.InfoAdultCount + AirFlightRequest.InfoChildCount + AirFlightRequest.InfoInfantCount; } set { } }
    }
}
