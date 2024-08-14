using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Pages.MainPopups;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    partial class MoreViewModel : BaseViewModel
    {
        public MoreViewModel()
        {

        }

        [RelayCommand]
        async Task SelectLanguage()
        {
            await MopupService.Instance.PushAsync(new LanguagePopup());
        }
    }
}
