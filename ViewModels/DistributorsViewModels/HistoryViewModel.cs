﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Models;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    partial class HistoryViewModel : ObservableObject
    {
        #region Prop
        [ObservableProperty]
        public ObservableCollection<RequestClassModel> requests;
        #endregion


        public HistoryViewModel()
        {
            Requests = new ObservableCollection<RequestClassModel>();
            LoadData();
        }



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
    }
}