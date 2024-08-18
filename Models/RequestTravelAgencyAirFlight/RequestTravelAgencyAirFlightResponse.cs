namespace TripBliss.Models
{
    public record RequestTravelAgencyAirFlightResponse
    (
         int Id,
         int RequestTravelAgencyId,
         int AirFlightId,
         int ClassAirFlightId,
         DateTime Date,
         string AirportFrom,
         string AirportTo,
         TimeOnly ETA,
         TimeOnly ETD,
         int InfoAdultCount,
         int InfoChildCount,
         int InfoInfantCount,
         int TotalPerson
    );
}
