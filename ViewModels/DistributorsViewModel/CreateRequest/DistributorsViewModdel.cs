using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;


namespace TripBliss.ViewModels.DistributorsViewModel.CreateRequest
{
    partial class DistributorsViewModdel : BaseViewModel
    {
        public ObservableCollection<DistributorsModdel>? distributors { get; set; }


        public DistributorsViewModdel()
        {
            distributors = new ObservableCollection<DistributorsModdel>();
            LoadData();
        }


        void LoadData()
        {
            distributors.Add(new DistributorsModdel
            {
                Address = "Egypt",
                Name = "Akl Group",
                Phone = "+20155154110",
                Rate = "4.5",
                Services = "Hotel - Ticketing - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Saudi Arabia",
                Name = "Al Faisal Company",
                Phone = "+966123456789",
                Rate = "4.2",
                Services = "Hotel - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "United Arab Emirates",
                Name = "Dubai Services",
                Phone = "+971987654321",
                Rate = "4.7",
                Services = "Hotel - Ticketing - Tours"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Qatar",
                Name = "Qatar Hospitality",
                Phone = "+974654321987",
                Rate = "4.3",
                Services = "Hotel - Transportation - Ticketing"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Kuwait",
                Name = "Kuwait Travels",
                Phone = "+965321654987",
                Rate = "4.6",
                Services = "Hotel - Ticketing - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Bahrain",
                Name = "Bahrain Tour Services",
                Phone = "+973789456123",
                Rate = "4.4",
                Services = "Hotel - Tours - Transportation"
            });

            distributors.Add(new DistributorsModdel
            {
                Address = "Oman",
                Name = "Oman Travel Agency",
                Phone = "+968456123789",
                Rate = "4.8",
                Services = "Hotel - Ticketing - Transportation"
            });

        }


        [RelayCommand]
        void OnAddRequest()
        {
            App.Current.MainPage.Navigation.PushAsync(new ChooseDistributorPage());
        }

        [RelayCommand]
        void OnBackPressed()
        {
            App.Current.MainPage.Navigation.PopAsync();
        }
        [RelayCommand]
        void OnSelection()
        {
            App.Current.MainPage.Navigation.PushAsync(new NewRequestPage());
        }

    }
}
