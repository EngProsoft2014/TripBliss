using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages;
using TripBliss.Pages.TravelAgenciesPages.CreateRequest;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class TravelAgencyViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<DistributorsModel>? distributors;


        readonly IGenericRepository Rep;
        public TravelAgencyViewModel(IGenericRepository GenericRep)
        {
            Rep = GenericRep;

            Distributors = new ObservableCollection<DistributorsModel>();

            Distributors = Controls.StaticMember.LstDistributors;

            Lang = Preferences.Default.Get("Lan", "en");
            //if (Controls.StaticMember.WayOfTab == 1)
            //{
            //    UserDialogs.Instance.ShowLoading();
            //    LoadData();
            //    UserDialogs.Instance.HideHud();
            //}
                
        }


        void LoadData()
        {
            Distributors.Add(new DistributorsModel
            {
                Address = "Egypt",
                Name = "Akl Group",
                Phone = "+20155154110",
                Rate = "4.5",
                Services = "Hotel - Ticketing - Transportation"
            });

            Distributors.Add(new DistributorsModel
            {
                Address = "Saudi Arabia",
                Name = "Al Faisal Company",
                Phone = "+966123456789",
                Rate = "4.2",
                Services = "Hotel - Transportation"
            });

            Distributors.Add(new DistributorsModel
            {
                Address = "United Arab Emirates",
                Name = "Dubai Services",
                Phone = "+971987654321",
                Rate = "4.7",
                Services = "Hotel - Ticketing - Tours"
            });

            Distributors.Add(new DistributorsModel
            {
                Address = "Qatar",
                Name = "Qatar Hospitality",
                Phone = "+974654321987",
                Rate = "4.3",
                Services = "Hotel - Transportation - Ticketing"
            });

            Distributors.Add(new DistributorsModel
            {
                Address = "Kuwait",
                Name = "Kuwait Travels",
                Phone = "+965321654987",
                Rate = "4.6",
                Services = "Hotel - Ticketing - Transportation"
            });

            Distributors.Add(new DistributorsModel
            {
                Address = "Bahrain",
                Name = "Bahrain Tour Services",
                Phone = "+973789456123",
                Rate = "4.4",
                Services = "Hotel - Tours - Transportation"
            });

            Distributors.Add(new DistributorsModel
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
            App.Current.MainPage.Navigation.PushAsync(new NewRequestPage(new NewRequestViewModel(Rep)));
        }

    }
}
