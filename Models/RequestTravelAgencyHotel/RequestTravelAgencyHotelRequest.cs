﻿using System.ComponentModel;

namespace TripBliss.Models
{
    public record RequestTravelAgencyHotelRequest : INotifyPropertyChanged
    {

        public int LocationId { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomViewId { get; set; }
        public int MealId { get; set; }
        int _RoomCount;
        public int RoomCount
                {
                    get
                    {
                        return _RoomCount;
                    }
                    set
                    {
                        _RoomCount = value;
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this, new PropertyChangedEventArgs("RoomCount"));
                        }
                    }
                }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfNights { get { return (CheckOut - CheckIn).Days - 1; } }

        public string? Notes { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
