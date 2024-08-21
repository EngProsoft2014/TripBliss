namespace TripBliss.Models.RequestTravelAgencyVisa
{
    public record RequestTravelAgencyVisaResponse
    (
         int Id,
         int RequestTravelAgencyId,
         int VisaId,
         int PersonCount
    );
}
