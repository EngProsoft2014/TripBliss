using System.Text.Json.Serialization;

namespace TripBliss.Models
{
    public record TravelAgencyCompanyRequest
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Logo { get; set; }
        public DateOnly? ExpireDateAcc { get; set; }
        public bool? ShowAllDistributors { get; set; }
        public int? Review { get; set; }
    }
}
