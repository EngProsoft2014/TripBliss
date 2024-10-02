using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using static TripBliss.Helpers.ErrorsResult;

namespace TripBliss.ViewModels
{
    public partial class ResetViewModel : BaseViewModel
    {
        new class ResetEmail
        {
            [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
            public string? Email { get; set; }
        }

        #region Prop
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        [ObservableProperty]
        string email; 
        #endregion

        #region Service
        readonly IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Cons
        public ResetViewModel(IGenericRepository GenericRep, Services.Data.ServicesService service)
        {
            Rep = GenericRep;
            _service = service;
        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply()
        {
            IsBusy = false;

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                ResetEmail model = new ResetEmail { Email = Email };
                if (!string.IsNullOrEmpty(model.Email))
                {
                    if (Regex.IsMatch(model.Email, pattern))
                    {
                        string UserToken = await _service.UserToken();
                        UserDialogs.Instance.ShowLoading();
                        var Postjson = await Rep.PostTRAsync<ResetEmail, ErrorResult>($"{ApiConstants.PosResetApi}", model!, UserToken);
                        UserDialogs.Instance.HideHud();
                        if (Postjson.Item2 == null)
                        {
                            await App.Current!.MainPage!.Navigation.PopAsync();
                        }
                        else
                        {
                            var toast = Toast.Make($"Error : {Postjson.Item2!.errors!.Values.FirstOrDefault()}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                            await toast.Show();
                        }
                    }
                    else
                    {
                        var toast = Toast.Make("Please enter a valid email address.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                        await toast.Show();
                    }
                }
                else
                {
                    var toast = Toast.Make("Please enter your E-mail", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }

            IsBusy = true;

        } 
        #endregion
    }
}
