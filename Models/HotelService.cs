using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Models
{
    public class HotelService
    {
        public string? HotelName { get; set; }
        public DateOnly Checkin { get; set; }
        public DateOnly Checkout { get; set; }
        public string? RoomView { get; set; }
        public string? RoomType { get; set; }
        public string? Meals { get; set; }
        public int RoomNo { get; set; }
        public string? Notes { get; set; }
    }
}
