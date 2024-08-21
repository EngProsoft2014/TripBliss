namespace TripBliss.Models.RequestTravelAgencyVisa
{
    public record RequestTravelAgencyVisaRequest
    (
         int RequestTravelAgencyId,
         int VisaId,
         int PersonCount
    );
}
