using System.Text.Json.Serialization;

namespace TripBliss.Models
{
    public record ResponseWithDistributorRequest
    {
        public string? DistributorCompanyId { get; set; }
        public List<ResponseWithDistributorHotelRequest>? ResponseWithDistributorHotel { get; set; }
        public List<ResponseWithDistributorTransportRequest>? ResponseWithDistributorTransport { get; set; }
        public List<ResponseWithDistributorAirFlightRequest>? ResponseWithDistributorAirFlight { get; set; }
        public List<ResponseWithDistributorVisaRequest>? ResponseWithDistributorVisa { get; set; }
    }
}
