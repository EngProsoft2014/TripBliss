using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Helpers
{
    public class ErrorsResult
    {

        public class ErrorResult
        {
            public string? type { get; set; }
            public string? title { get; set; }
            public int? status { get; set; }
            public Dictionary<string, object>? errors { get; set; }
            public string? traceId { get; set; }
        }
    }
}
