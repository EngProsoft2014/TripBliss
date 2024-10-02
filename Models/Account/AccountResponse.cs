using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.Account
{
    public class AccountResponse
    {
        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("userName")]
        public string userName { get; set; }

        [JsonProperty("userCategory")]
        public string userCategory { get; set; }

        [JsonProperty("userPermision")]
        public string userPermision { get; set; }

        [JsonProperty("companyName")]
        public string companyName { get; set; }
    }
}
