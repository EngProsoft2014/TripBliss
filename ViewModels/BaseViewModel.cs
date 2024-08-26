using CommunityToolkit.Mvvm.ComponentModel;
using TripBliss.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Services;

namespace TripBliss.ViewModels
{
    public partial class BaseViewModel: ObservableObject
    {
        public BaseViewModel()
        {
            Lang = Preferences.Default.Get("Lan", "en");
            IsBusy = true;
        }

        [ObservableProperty]
        public bool isBusy = true;

        [ObservableProperty]
        public string lang;
    }
}
