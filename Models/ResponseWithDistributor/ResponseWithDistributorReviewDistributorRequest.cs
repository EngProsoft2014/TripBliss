using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.ResponseWithDistributor
{
    public class ResponseWithDistributorReviewDistributorRequest
    {
        public int ReviewToTravelAgency { get; set; }
        public string? ReviewDistributorNote { set; get; }
    }
}
