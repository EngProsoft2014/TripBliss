namespace TripBliss.Models.ResponseWithDistributorVisaDetails
{
    public record ResponseWithDistributorVisaDetailsResponse
    (
         int Id,
         int ResponseWithDistributorVisaId,
         int TravelAgencyGuestId,
         string? Notes
    );
}
