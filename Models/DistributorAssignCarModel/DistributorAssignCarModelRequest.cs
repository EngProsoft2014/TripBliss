﻿namespace TripBliss.Models
{
    public record DistributorAssignCarModelRequest
    {
        public string? DistributorCompanyId { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int Count { get; set; }
        public bool IsTrackCount { get; set; }
        public bool IsTrackingDate { get; set; }
    }
}
