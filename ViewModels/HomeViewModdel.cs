using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;
using TripBliss.Pages.TravelAgenciesPages.RequestDeatiels;

namespace TripBliss.ViewModels
{
    partial class HomeViewModdel : BaseViewModel
    {
        public ObservableCollection<RequestClass> requests{ get; set; }
        public HomeViewModdel()
        {
            requests = new ObservableCollection<RequestClass>();
            LoadData();
        }


        void LoadData()
        {
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Complated",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 2",
                DistName = "Ali",
                Statues = "Pinding",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 3",
                DistName = "Mohammed",
                Statues = "Accepted",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 4",
                DistName = "Abdullah",
                Statues = "Complated",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 5",
                DistName = "Hassn",
                Statues = "Accepted",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 6",
                DistName = "Omar",
                Statues = "Complated",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 7",
                DistName = "Tark",
                Statues = "Complated",
                Services = "Hotel - Tickting - Transportion"

            });
            requests.Add(new RequestClass()
            {
                Date = DateOnly.FromDateTime(DateTime.Now),
                RugestName = "Group 1",
                DistName = "Tark",
                Statues = "Complated",
                Services = "Hotel - Tickting - Transportion"

            });

        }
        [RelayCommand]
        void Selection()
        {
            App.Current.MainPage.Navigation.PushAsync(new RequestDeatielsPage());
        }
    }
}
