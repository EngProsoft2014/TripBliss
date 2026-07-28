using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.DistributorCompany
{
    public class DistributorCompanyFilterRequest
    {
        public string TravelAgencyCompanyId { get; set; }
        public bool IsHotel { get; set; } = false;
        public bool IsTransportation { get; set; } = false;
        public bool IsAirFlight { get; set; } = false;
        public bool IsVisa { get; set; } = false;
        public bool IsGuide { get; set; } = false;
    };
}
