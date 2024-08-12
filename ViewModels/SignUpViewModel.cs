using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.ViewModels
{
    partial class SignUpViewModel : BaseViewModel
    {
        [ObservableProperty]
        string companyname;
        [ObservableProperty]
        string companyphone;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string confirmpassword;
        [ObservableProperty]
        string qrcode;
    }
}
