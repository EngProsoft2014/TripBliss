using CommunityToolkit.Mvvm.ComponentModel;
using TripBliss.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        public bool isBusy;

        [ObservableProperty]
        public string lang;
    }
}
