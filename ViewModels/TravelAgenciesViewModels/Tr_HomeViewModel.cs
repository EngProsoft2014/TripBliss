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
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    public partial class Tr_HomeViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<RequestClassModel> requests = new ObservableCollection<RequestClassModel>();
        #endregion

        #region Services
        readonly Services.Data.ServicesService _service;
        IGenericRepository Rep;
        #endregion

        #region Cons
        public Tr_HomeViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            Requests = Controls.StaticMember.LstRequestClass;
        } 
        #endregion

        #region Methods
        void LoadData()
        {
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 2",
                DistName = "Ali",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 3",
                DistName = "Mohammed",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 4",
                DistName = "Abdullah",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 5",
                DistName = "Hassn",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 6",
                DistName = "Omar",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 7",
                DistName = "Tark",
                Statues = "Active",
                Services = "Hotel - Tickting - Transportion"

            });
            Requests.Add(new RequestClassModel()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Not Active",
                Services = "Hotel - Tickting - Transportion"

            });

        }


        #endregion

        #region RelayCommand
        [RelayCommand]
        async void Selection(RequestClassModel model)
        {
            await App.Current.MainPage.Navigation.PushAsync(new RequestDetailsPage(new RequestDetails.Tr_D_RequestDetailsViewModel(Rep,_service)));
        } 
        #endregion
    }
}
