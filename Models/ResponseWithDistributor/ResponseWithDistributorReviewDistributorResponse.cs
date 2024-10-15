using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.ResponseWithDistributor
{
    public class ResponseWithDistributorReviewDistributorResponse
    {
        public string RequestName { get; set; } = string.Empty;
        public int ReviewToTravelAgency { get; set; }
        public string? ReviewDistributorNote { set; get; }
        public DateTime? ReviewDistributorDateTime { get; set; }
        public string? ReviewUserDistributorName { get; set; }
    }
}
