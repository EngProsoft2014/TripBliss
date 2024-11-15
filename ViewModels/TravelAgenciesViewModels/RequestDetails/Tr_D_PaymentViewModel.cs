using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using Microsoft.AspNet.SignalR.Client.Http;
using Mopups.Services;
using Syncfusion.Maui.Data;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.RequestDetails;


namespace TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails
{
    
    public partial class Tr_D_PaymentViewModel : BaseViewModel
    {
        #region Prop
        string ReqId;
        int Totalprice;
        int Totalpayment;
        [ObservableProperty]
        int outStandingprice;
        [ObservableProperty]
        bool isAllPyment;
        [ObservableProperty]
        ObservableCollection<ResponseWithDistributorPaymentResponse> payments;  
        [ObservableProperty]
        string cardNumber ="";

        [ObservableProperty]
        string holderName = "";

        [ObservableProperty]
        string expirationDate = "";

        [ObservableProperty]
        string cvv = "";
        [ObservableProperty]
        int payMethod;
        [ObservableProperty]
        string photo = "";
        [ObservableProperty]
        string photoPath = "";

        [ObservableProperty]
        bool isStrip;

        [ObservableProperty]
        bool isBank;

        #endregion

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        ResponseWithDistributorResponse _distributorResponse;
        #endregion

        #region Cons
        public Tr_D_PaymentViewModel(bool isStripv, bool isBankv, string id,int totalPrice,int totalPayment, ResponseWithDistributorResponse distributorResponse, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            _distributorResponse = distributorResponse;
            IsStrip = isStripv;
            IsBank = isBankv;
            PayMethod = (isStripv==true && isBankv == true) ? 3 : (isStripv == true && isBankv == false) ? 2 : 3;
            ReqId = id;
            Totalpayment = totalPayment;
            OutStandingprice = totalPrice - totalPayment;
            Totalprice = totalPrice;
            IsAllPyment = true;
            Init();
        }
        #endregion


        [RelayCommand]
        async Task AddPayment()
        {
            bool answer = await App.Current!.MainPage!.DisplayAlert(TripBliss.Resources.Language.AppResources.Question, TripBliss.Resources.Language.AppResources.Are_You_Accept_To_Pay, TripBliss.Resources.Language.AppResources.Yes, TripBliss.Resources.Language.AppResources.No);
            IsBusy = false;

            
            if (Connectivity.NetworkAccess == NetworkAccess.Internet && answer)
            {

                string UserToken = await _service.UserToken();
                string[] parts = [];
                if (!string.IsNullOrEmpty(ExpirationDate))
                {
                    parts = ExpirationDate.Split('/');
                }
                ResponseWithDistributorPaymentRequest paymentRequest = new ResponseWithDistributorPaymentRequest
                {
                    AmountPayment = IsAllPyment == true ? OutStandingprice : (Totalprice - Totalpayment - OutStandingprice),
                    PaymentMethod = PayMethod,
                    dbcr = 1,
                    Notes = "",
                    Refnumber = "stetrrcc",
                    CardholderName = HolderName,
                    CardNumber = CardNumber,
                    Cvc = Cvv,
                    ImgFile = Convert.FromBase64String(Photo),
                    Extension = PhotoPath

                };
                if (!string.IsNullOrEmpty(ExpirationDate))
                {
                    paymentRequest.ExpirationMonth = Convert.ToInt32(parts[0]);
                    paymentRequest.ExpirationYear = Convert.ToInt32(parts[1]);
                }
                UserDialogs.Instance.ShowLoading();
                var json = await Rep.PostTRAsync<ResponseWithDistributorPaymentRequest, ResponseWithDistributorPaymentResponse>(ApiConstants.AllPaymentApi + $"{ReqId}/ResponseWithDistributorPayment", paymentRequest, UserToken);
                UserDialogs.Instance.HideHud();

                if (json.Item1 != null)
                {
                    var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Successfully_for_Paying, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                    Totalpayment = (int)(Totalpayment + paymentRequest.AmountPayment);
                    //OutStandingprice = IsAllPyment == true ? 0 : OutStandingprice - paymentRequest.AmountPayment.Value;
                    await GetPayDetailes();     
                }
                else
                {
                    var toast = Toast.Make($"{json.Item2!.errors!.FirstOrDefault().Value}", CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                    await toast.Show();
                }
            }
            
            IsBusy = true;
        }

        [RelayCommand]
        async Task BackButtonClicked()
        {
            new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service);
            await App.Current!.MainPage!.Navigation.PopAsync();
            //await App.Current!.MainPage!.Navigation.PushAsync(new ConfirmResponsePage(new Tr_D_ConfirmResponsePageViewModel(_distributorResponse, Rep, _service), Rep));
            //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
        }

        [RelayCommand]
        async Task Attachreceipt()
        {
            IsBusy = false;

            if (Constants.Permissions.CheckPermission(Constants.Permissions.Add_Attachment))
            { 
                var page = new Pages.MainPopups.AddAttachmentsPopup();
                page.ImageClose += async (img, imgPath) =>
                {
                    if (!string.IsNullOrEmpty(img))
                    {
                        byte[] bytes = Convert.FromBase64String(img);

                        Photo = img;
                        PhotoPath = Path.GetExtension(imgPath);
                        
                        await MopupService.Instance.PopAsync();
                    }
                };

                await MopupService.Instance.PushAsync(page);
            }
            else
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.PermissionAlert, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }

            IsBusy = true;
        }

        #region Methods
        async void Init()
        {
            UserDialogs.Instance.ShowLoading();
            await GetPayDetailes();
            UserDialogs.Instance.HideHud();
        }
        async Task GetPayDetailes()
        {
            IsBusy = false;
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json1 = await Rep.GetAsync<ObservableCollection<ResponseWithDistributorPaymentResponse>>(ApiConstants.AllPaymentApi + $"{ReqId}/ResponseWithDistributorPayment", UserToken);

                if (json1 != null)
                {
                    Payments = json1;
                }
            }

            IsBusy = true;
        } 

        public async Task CalcOutPrice(int CustomPrice)
        {
            if (CustomPrice > Totalprice)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.This_value_is_greater_than_the_total_price, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (CustomPrice !=0)
            {
                OutStandingprice = Totalprice - Totalpayment - CustomPrice;
                IsAllPyment = false;
            }
            else
            {
                OutStandingprice = Totalprice == Totalpayment ? 0 : Totalprice - Totalpayment;
                IsAllPyment = true;
            }
        }

        //[RelayCommand]
        //async void CreditPayNow(InvoiceModel model)
        //{
        //    IsBusy = true;

        //    try
        //    {
        //        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        //        {
        //            await App.Current!.MainPage!.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
        //            return;
        //        }
        //        else
        //        {
        //            UserDialogs.Instance.ShowLoading();
        //            OneInvoice = model;

        //            OnePayment.Type = 0;
        //            OnePayment.Method = 3; //Debit Card

        //            await PayViaStripe();
        //            UserDialogs.Instance.HideHud();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        await App.Current!.MainPage!.DisplayAlert("Error", "Please Complate All informations !!!", "OK");
        //    }

        //    IsBusy = false;
        //}

        //public async Task InitiolizModel(InvoiceModel model)
        //{
        //    try
        //    {
        //        if (Connectivity.NetworkAccess != NetworkAccess.Internet)
        //        {
        //            await App.Current!.MainPage!.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
        //            return;
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(SignatureImageByte64))
        //            {
        //                if (OnePayment.Amount <= model.Net || OnePayment.Amount == null)
        //                {
        //                    string UserToken = await _service.UserToken();

        //                    OnePayment.AccountId = model.AccountId;
        //                    OnePayment.BrancheId = model.BrancheId;
        //                    OnePayment.CustomerId = model.CustomerId;
        //                    //OnePayment.ContractId = model.ContractId;
        //                    OnePayment.InvoiceId = model.Id;
        //                    //OnePayment.ExpensesId = model.ExpensesId;
        //                    OnePayment.PaymentDate = DateTime.Now;
        //                    OnePayment.SignatureDraw = SignatureImageByte64;

        //                    OnePayment.Amount = OnePayment.Amount == null ? model.Net : OnePayment.Amount;
        //                    //OnePayment.OverAmount = model.OverAmount;

        //                    //OnePayment.IncreaseDecrease = model.IncreaseDecrease;
        //                    //OnePayment.TransactionID = model.TransactionID;
        //                    //OnePayment.CheckNumber = model.CheckNumber;
        //                    //OnePayment.BankName = model.BankName;
        //                    //OnePayment.AccountNumber = model.AccountNumber;
        //                    OnePayment.Notes = model.Notes;
        //                    OnePayment.Active = model.Active;
        //                    OnePayment.CreateUser = model.CreateUser;
        //                    OnePayment.CreateDate = model.CreateDate;

        //                    UserDialogs.Instance.ShowLoading();
        //                    var json = await ORep.PostDataAsync("api/Payments/InsertPayment", OnePayment, UserToken);
        //                    UserDialogs.Instance.HideHud();

        //                    if (json != null && json != "api not responding")
        //                    {
        //                        await App.Current.MainPage.DisplayAlert("FixPro", "Succes Payment for This Job.", "Ok");
        //                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        //                    }
        //                    else
        //                    {
        //                        await App.Current.MainPage.DisplayAlert("FixPro", "Field Payment for This Job.", "Ok");
        //                    }
        //                }
        //                else
        //                {
        //                    await App.Current.MainPage.DisplayAlert("FixPro", "Please Enter Right Amount.", "Ok");
        //                }
        //            }
        //            else
        //            {
        //                await App.Current.MainPage.DisplayAlert("FixPro", "Please Enter Signature.", "Ok");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        await App.Current!.MainPage!.DisplayAlert("Error", ex.Message, "OK");
        //    }

        //}

        //async Task GetSkretKey(int? BranchId)
        //{
        //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        UserDialogs.Instance.ShowLoading();
        //        string UserToken = await _service.UserToken();

        //        var json = await ORep.GetAsync<StripeAccountModel>(string.Format("api/Payments/GetStripeAccount?" + "BranchId=" + BranchId), UserToken);

        //        if (json != null)
        //        {
        //            StripeModel = json;
        //        }

        //        UserDialogs.Instance.HideHud();
        //    }
        //}

        //public async Task PayViaStripe()
        //{
        //    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
        //    {
        //        await GetSkretKey(OneInvoice.BrancheId);

        //        StripeConfiguration.ApiKey = StripeModel.SecretKey;

        //        //StripeConfiguration.ApiKey = "sk_test_IHINMHgrNTLUWqh3IcTcMdNB";

        //        // step 2: Assign card to token object
        //        var stripeCard = new TokenCreateOptions
        //        {
        //            Card = new TokenCardOptions
        //            {
        //                Number = CardNumber,
        //                Name = HolderName,
        //                ExpMonth = ExpirationDate.Split('/')[0],
        //                ExpYear = ExpirationDate.Split('/')[1],
        //                Cvc = cvv,
        //            }
        //        };

        //        Stripe.TokenService service = new Stripe.TokenService();
        //        Stripe.Token newToken = service.Create(stripeCard);

        //        // step 3: assign the token to the source
        //        var option = new SourceCreateOptions
        //        {
        //            Type = SourceType.Card,
        //            Currency = "USD",
        //            Token = newToken.Id
        //        };

        //        var sourceService = new SourceService();
        //        Stripe.Source source = sourceService.Create(option);

        //        // step 4: create customer
        //        CustomerCreateOptions customer = new CustomerCreateOptions
        //        {
        //            Name = CustomerDetails.FirstName + "" + CustomerDetails.LastName,
        //            Email = CustomerDetails.Email,
        //            Description = OneInvoice.ScheduleName,
        //            Address = new AddressOptions { City = CustomerDetails.City, Country = CustomerDetails.Country, Line1 = CustomerDetails.Address, Line2 = "", PostalCode = CustomerDetails.PostalcodeZIP, State = CustomerDetails.State }
        //        };

        //        var customerService = new CustomerService();
        //        var cust = customerService.Create(customer);

        //        // step 5: charge option
        //        var chargeoption = new ChargeCreateOptions
        //        {
        //            Amount = Convert.ToInt64(OneInvoice.Total * 100),
        //            Currency = "USD",
        //            ReceiptEmail = CustomerDetails.Email,
        //            Customer = cust.Id,
        //            Source = source.Id
        //        };

        //        // step 6: charge the customer
        //        var chargeService = new ChargeService();
        //        Charge charge = chargeService.Create(chargeoption);
        //        if (charge.Status == "succeeded")
        //        {
        //            // success
        //            await InitiolizModel(OneInvoice);
        //        }
        //        else
        //        {
        //            // failed
        //            await App.Current!.MainPage!.DisplayAlert("Alert", "failed Payment for This Job.", "Ok");
        //        }
        //    }

        //}
        #endregion
    }
}
