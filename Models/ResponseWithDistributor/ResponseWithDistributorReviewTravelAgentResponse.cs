using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.ResponseWithDistributor
{
    public class ResponseWithDistributorReviewTravelAgentResponse
    {
        public string RequestName { get; set; } = string.Empty;
        public int ReviewToDistributor { get; set; }
        public string? ReviewTravelAgentNote { set; get; }
        public DateTime? ReviewTravelAgentDateTime { get; set; }
        public string? ReviewUserTravelAgentName { set; get; }
    }
}
