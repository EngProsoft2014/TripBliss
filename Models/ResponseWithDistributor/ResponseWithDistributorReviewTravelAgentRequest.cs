using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.ResponseWithDistributor
{
    public class ResponseWithDistributorReviewTravelAgentRequest
    {
        public int ReviewToDistributor { get; set; }
        public string? ReviewTravelAgentNote { set; get; }
    }
}
