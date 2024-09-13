using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.RequestTravelAgencyGuest
{
    public class TravelAgencyGuestResponse
    {
        public int Id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public string? TravelAgencyCompanyName { get; set; }
        public string? GuestName { get; set; }
        public string? Nationality { get; set; }
        int? TypeIdNumber { get; set; }
        public string? IDNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? AdultCount { get; set; }
        public string? ChildCount { get; set; }
        public string? InfantCount { get; set; }

    }
}
