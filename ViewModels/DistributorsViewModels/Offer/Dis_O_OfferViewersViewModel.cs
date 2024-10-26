using CommunityToolkit.Mvvm.ComponentModel;
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
        public ObservableCollection<TravelAgencyCompanyResponse>? travelAgencies = new ObservableCollection<TravelAgencyCompanyResponse>();

        IGenericRepository Rep;
        public Dis_O_OfferViewersViewModel(IGenericRepository generic)
        {
            Rep = generic;
            LoadData();
        }


        void LoadData()
        {
            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "Egypt",
                CompanyName = "Akl Group",
                Phone = "+20155154110",
                ReviewToTravelAgency = 4.5,
                Logo = "Hotel - Ticketing - Transportation"
            });

            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "Saudi Arabia",
                CompanyName = "Al Faisal Company",
                Phone = "+966123456789",
                ReviewToTravelAgency = 4.2,
                Logo = "Hotel - Transportation"
            });

            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "United Arab Emirates",
                CompanyName = "Dubai Services",
                Phone = "+971987654321",
                ReviewToTravelAgency = 4.7,
                Logo = "Hotel - Ticketing - Tours"
            });

            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "Qatar",
                CompanyName = "Qatar Hospitality",
                Phone = "+974654321987",
                ReviewToTravelAgency = 4.3,
                Logo = "Hotel - Transportation - Ticketing"
            });

            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "Kuwait",
                CompanyName = "Kuwait Travels",
                Phone = "+965321654987",
                ReviewToTravelAgency = 4.6,
                Logo = "Hotel - Ticketing - Transportation"
            });

            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "Bahrain",
                CompanyName = "Bahrain Tour Services",
                Phone = "+973789456123",
                ReviewToTravelAgency = 4.4,
                Logo = "Hotel - Tours - Transportation"
            });

            TravelAgencies.Add(new TravelAgencyCompanyResponse
            {
                Address = "Oman",
                CompanyName = "Oman Travel Agency",
                Phone = "+968456123789",
                ReviewToTravelAgency = 4.8,
                Logo = "Hotel - Ticketing - Transportation"
            });

        }

        [RelayCommand]
        async void BackClicked()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
