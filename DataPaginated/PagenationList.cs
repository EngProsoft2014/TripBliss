using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.DataPaginated
{
    public class PagenationList<T>
    {
        public List<T>? DataModel { get; set; } = [];
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}
