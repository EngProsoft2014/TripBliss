﻿using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Helpers;
using TripBliss.Pages.MainPopups;

namespace TripBliss.ViewModels.DistributorsViewModels
{
    partial class Dis_MoreViewModel : BaseViewModel
    {
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        public Dis_MoreViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
        }

        [RelayCommand]
        async Task SelectLanguage()
        {
            await MopupService.Instance.PushAsync(new LanguagePopup(Rep, _service));
        }
    }
}
