﻿using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Pages.MainPopups;

namespace TripBliss.ViewModels.TravelAgenciesViewModels
{
    partial class Tr_MoreViewModel : BaseViewModel
    {
        IGenericRepository Rep;
        public Tr_MoreViewModel(IGenericRepository generic)
        {
            Rep = generic;
        }

        [RelayCommand]
        async Task SelectLanguage()
        {
            await MopupService.Instance.PushAsync(new LanguagePopup(Rep));
        }
    }
}