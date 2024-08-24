using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.DistributorCompany
{
    public class FavouriteDistributorCompanyResponse
    {
        public int id { get; set; }
        public string? TravelAgencyCompanyId { get; set; }
        public string? TistributorCompanyId { get; set; }
        public bool Private { get; set; }
        public DistributorCompanyResponse? DistributorCompany { get; set; }
        public TravelAgencyCompanyResponse? TravelAgencyCompany { get; set; }
    }
}
