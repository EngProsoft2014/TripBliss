﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels.Offer
{
    public partial class Dis_O_OfferViewersViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<TravelAgenciesModel>? travelAgencies;

        IGenericRepository Rep;
        public Dis_O_OfferViewersViewModel(IGenericRepository generic)
        {
            Rep = generic;
            TravelAgencies = new ObservableCollection<TravelAgenciesModel>();
            LoadData();
        }


        void LoadData()
        {
            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "Egypt",
                Name = "Akl Group",
                Phone = "+20155154110",
                Rate = "4.5",
                Services = "Hotel - Ticketing - Transportation"
            });

            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "Saudi Arabia",
                Name = "Al Faisal Company",
                Phone = "+966123456789",
                Rate = "4.2",
                Services = "Hotel - Transportation"
            });

            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "United Arab Emirates",
                Name = "Dubai Services",
                Phone = "+971987654321",
                Rate = "4.7",
                Services = "Hotel - Ticketing - Tours"
            });

            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "Qatar",
                Name = "Qatar Hospitality",
                Phone = "+974654321987",
                Rate = "4.3",
                Services = "Hotel - Transportation - Ticketing"
            });

            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "Kuwait",
                Name = "Kuwait Travels",
                Phone = "+965321654987",
                Rate = "4.6",
                Services = "Hotel - Ticketing - Transportation"
            });

            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "Bahrain",
                Name = "Bahrain Tour Services",
                Phone = "+973789456123",
                Rate = "4.4",
                Services = "Hotel - Tours - Transportation"
            });

            TravelAgencies.Add(new TravelAgenciesModel
            {
                Address = "Oman",
                Name = "Oman Travel Agency",
                Phone = "+968456123789",
                Rate = "4.8",
                Services = "Hotel - Ticketing - Transportation"
            });

        }

        [RelayCommand]
        async void BackClicked()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
