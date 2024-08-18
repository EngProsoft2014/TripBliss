﻿namespace TripBliss.Models
{
    public record RequestTravelAgencyHotelResponse
    (
         int Id,
         int RequestTravelAgencyId,
         int LocationId,
         int HotelId,
         int RoomTypeId,
         int RoomViewId,
         int MealId,
         int RoomCount,
         DateTime CheckIn,
         DateTime CheckOut,
         int NumberOfNights
    );
}
