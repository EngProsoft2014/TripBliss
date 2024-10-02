
using CommunityToolkit.Maui.Alerts;
using Mopups.Services;
using TripBliss.Constants;
using TripBliss.Helpers;
namespace TripBliss.Pages.MainPopups;

public partial class ChangePasswordPopup : Mopups.Pages.PopupPage
{
    new class ChangePass
    {
        public string? currentPassword { get; set; }
        public string? newPassword { get; set; }
    }

    #region Services
    readonly Services.Data.ServicesService _service;
    IGenericRepository Rep;
    #endregion

    #region Cons
    public ChangePasswordPopup(IGenericRepository generic, Services.Data.ServicesService service)
    {
        InitializeComponent();
        Rep = generic;
        _service = service;
    } 
    #endregion

    private void TapGestureRecognizer_Tapped_OldPassword(object sender, TappedEventArgs e)
    {
        entryOldPass.IsPassword = (entryOldPass.IsPassword == true) ? false : true;
    }

    private void TapGestureRecognizer_Tapped_NewPassword(object sender, TappedEventArgs e)
    {
        entryNewPass.IsPassword = (entryNewPass.IsPassword == true) ? false : true;
    }

    private async void TapGestureRecognizer_Tapped_Confirm(object sender, TappedEventArgs e)
    {
        if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        {
            if (string.IsNullOrEmpty(entryOldPass.Text))
            {
                var toast = Toast.Make("Please Enter a Required : Old Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(entryNewPass.Text))
            {
                var toast = Toast.Make("Please Enter a Required : New Password.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                ChangePass changePass = new ChangePass()
                {
                    currentPassword = entryOldPass.Text,
                    newPassword = entryNewPass.Text
                };
                string UserToken = await _service.UserToken();
                var json = await Rep.PutAsync<ChangePass>(ApiConstants.PutChangePassApi, changePass, UserToken);

                if (json.currentPassword != null && json.newPassword != null)
                {
                    await MopupService.Instance.PopAsync();
                    var toast = Toast.Make("Password changed successfully.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
                else
                {
                    var toast = Toast.Make("Error Please Check Data again.", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }
            
        }
    }
}