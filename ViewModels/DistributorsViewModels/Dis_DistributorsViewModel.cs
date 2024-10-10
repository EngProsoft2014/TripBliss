using CommunityToolkit.Maui.Alerts;
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
using TripBliss.Pages;
using TripBliss.Pages.DistributorsPages.ResponseDetailes;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;
using TripBliss.ViewModels.ActivateViewModels;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;


namespace TripBliss.ViewModels.DistributorsViewModels.CreateResponse
{
    partial class Dis_DistributorsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<TravelAgenciesModel>? travelAgencies;

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion
        public Dis_DistributorsViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            TravelAgencies = new ObservableCollection<TravelAgenciesModel>();
            Init();
            _service = service;
        }

        async void Init()
        {
            if (Constants.Permissions.CheckPermission(Constants.Permissions.DS_Show_Agencies))
            {
                LoadData();
            }
            else
            {
                var toast = Toast.Make("Permission not allowed for this action.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
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
        async void OnAddRequest()
        {
            //await App.Current.MainPage.Navigation.PushAsync(new RequestDetailsPage(new Dis_D_RequestDetailsViewModel(Rep,_service)));
        }

        [RelayCommand]
        async void OnBackPressed()
        {
           await App.Current.MainPage.Navigation.PopAsync();
        }


    }
}
