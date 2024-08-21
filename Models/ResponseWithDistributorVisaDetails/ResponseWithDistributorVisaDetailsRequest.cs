namespace TripBliss.Models.ResponseWithDistributorVisaDetails
{
    public record ResponseWithDistributorVisaDetailsRequest
    (
         int ResponseWithDistributorVisaId,
         int TravelAgencyGuestId,
         string? Notes
    );
}
