﻿using System.Text.Json.Serialization;

namespace TripBliss.Models
{
    public record TravelAgencyCompanyRequest
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string locationlatitude { get; set; }
        public string locationlongitude { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalcodeZIP { get; set; }
        public string? Website { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Logo { get; set; }
        public DateOnly? ExpireDateAcc { get; set; }
        public byte[]? ImgFile { get; set; }
        public string? Extension { get; set; } = string.Empty;
        public bool? ShowAllDistributors { get; set; } = true;
        public int? Review { get; set; } = 0;


    }
}
