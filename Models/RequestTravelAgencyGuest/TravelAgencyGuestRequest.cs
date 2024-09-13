using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.RequestTravelAgencyGuest
{
    public class TravelAgencyGuestRequest
    {
        public int Id { get; set; }
        public string? GuestName { get; set; }
        public string? Nationality { get; set; }
        public int? TypeIdNumber { get; set; }
        public string? IDNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AdultCount { get; set; }
        public string? ChildCount { get; set; }
        public string? InfantCount { get; set; }

    }
}
