using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.RequestTravelAgency
{
    public record RequestTravelAgencyResponse
    {
        public int Id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public bool IsDelete { get; set; }
        public int ResponseWithDistributorCount { get; set; }
        public int TotalPriceDisAcceptCount { get; set; }
        public int TotalPriceAgentAcceptCount { get; set; }
        public TravelAgencyCompanyResponse TravelAgencyCompany { get; set; }
        public List<ResponseWithDistributorResponse>? ResponseWithDistributor { get; set; }

    }
}
