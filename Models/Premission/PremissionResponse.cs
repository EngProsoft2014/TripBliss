using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models.Premission
{
    public class PremissionResponse
    {
        public int? id { get; set; }
        public int? roleClaimsId { get; set; }
        public string? userId { get; set; }
        public string? categoryPermissions { get; set; }
        public string? claimTitle { get; set; }
        public string? claimTitleAr { get; set; }
        public string? claimValue { get; set; }
        public bool choosenPermission { get; set; }
    }
}
