using Microsoft.Maui.Handlers;
using TripBliss.Helpers;
using TripBliss.Pages.ActivateDetailsPages;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.DistributorsPages.Offer;
using TripBliss.Pages.MainPopups;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Pages.TravelAgenciesPages.ActivateDetailsPages;
using TripBliss.Pages.Users;
using TripBliss.Services.Data;
using TripBliss.ViewModels;
using TripBliss.ViewModels.ActivateViewModels;
using TripBliss.ViewModels.DistributorsViewModels;
using TripBliss.ViewModels.DistributorsViewModels.CreateResponse;
using TripBliss.ViewModels.DistributorsViewModels.Offer;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;
using TripBliss.ViewModels.Shared.UsersViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels;
using TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest;
using TripBliss.ViewModels.TravelAgenciesViewModels.Offer;
using TripBliss.ViewModels.TravelAgenciesViewModels.RequestDetails;

namespace TripBliss
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection Services)
        {
            #region ServiceServices
            Services.AddSingleton<ServicesService>();
            #endregion

            #region GenericRepository
            Services.AddScoped<IGenericRepository, GenericRepository>();
            #endregion

            #region ViewModels
            #region Shared ViewModels
            //Main Shared ViewModels
            Services.AddTransient<BaseViewModel>();
            Services.AddTransient<ChangePassViewModel>();
            Services.AddTransient<LoginViewModel>();
            Services.AddTransient<ProfileViewModel>();
            Services.AddTransient<ProviderDetailsViewModel>();
            Services.AddTransient<ResetViewModel>();
            Services.AddTransient<SignUpViewModel>();
            //ActivateViewModels Shared ViewModels
            Services.AddTransient<AirFlightActivateViewModel>();
            Services.AddTransient<HotelActivateViewModel>();
            Services.AddTransient<MainActivateViewModel>();
            Services.AddTransient<TransportActivateViewModel>();
            Services.AddTransient<VisaActivateViewModel>();
            //UsersViewModels Shared ViewModels
            Services.AddTransient<AddUserViewModel>();
            Services.AddTransient<UserPermissionViewModel>();
            Services.AddTransient<UsersViewModel>();
            #endregion

            #region Travel Agencies ViewModels
            //Main TravelAgencies ViewModels
            Services.AddTransient<Tr_AccountViewModel>();
            Services.AddTransient<Tr_DocumentsViewModel>();
            Services.AddTransient<Tr_HistoryViewModel>();
            Services.AddTransient<Tr_HomeViewModel>();
            Services.AddTransient<Tr_MoreViewModel>();
            Services.AddTransient<Tr_ProviderDetailsViewModel>();
            //CreateRequest TravelAgencies ViewModels
            Services.AddTransient<Tr_C_AirFlightServicesViewModel>();
            Services.AddTransient<Tr_C_ChooseDistributorViewModel>();
            Services.AddTransient<Tr_C_HotelServiceViewModel>();
            Services.AddTransient<Tr_C_NewRequestViewModel>();
            Services.AddTransient<Tr_C_TransportaionServiceViewModel>();
            Services.AddTransient<Tr_C_TravelAgencyViewModel>();
            Services.AddTransient<Tr_C_VisaServiceViewModel>();
            //Offer TravelAgencies ViewModels
            Services.AddTransient<Tr_O_ChooseOfferViewModel>();
            Services.AddTransient<Tr_O_OfferDetailsViewModel>();
            //RequestDetails TravelAgencies ViewModels
            Services.AddTransient<Tr_D_AirFlightServicesViewModel>();
            Services.AddTransient<Tr_D_ConfirmResponsePageViewModel>();
            Services.AddTransient<Tr_D_HotelServiceViewModel>();
            Services.AddTransient<Tr_D_PaymentViewModel>();
            Services.AddTransient<Tr_D_RequestDetailsViewModel>();
            Services.AddTransient<Tr_D_ReviewViewModel>();
            Services.AddTransient<Tr_D_TransportaionServiceViewModel>();
            Services.AddTransient<Tr_D_VisaServiceViewModel>();
            #endregion

            #region Distributors ViewModels
            //Main DistributorsViewModels ViewModels
            Services.AddTransient<Dis_AccountViewModel>();
            Services.AddTransient<Dis_DistributorsViewModel>();
            Services.AddTransient<Dis_DocumentsViewModel>();
            Services.AddTransient<Dis_HistoryViewModel>();
            Services.AddTransient<Dis_HomeViewModel>();
            Services.AddTransient<Dis_MoreViewModel>();
            Services.AddTransient<Dis_ProviderDetailsViewModel>();
            //Offer DistributorsViewModels ViewModels
            Services.AddTransient<Dis_O_ChooseOfferViewModel>();
            Services.AddTransient<Dis_O_CreateOfferViewModel>();
            Services.AddTransient<Dis_O_OfferDetailsViewModel>();
            Services.AddTransient<Dis_O_OfferViewersViewModel>();
            //RequestDetails DistributorsViewModels ViewModels
            Services.AddTransient<Dis_D_AirFlightServicesViewModel>();
            Services.AddTransient<Dis_D_HotelServiceViewModel>();
            Services.AddTransient<Dis_D_PaymentViewModel>();
            Services.AddTransient<Dis_D_RequestDetailsViewModel>();
            Services.AddTransient<Dis_D_TransportaionServiceViewModel>();
            Services.AddTransient<Dis_D_VisaServiceViewModel>();
            Services.AddTransient<Dis_ReviewViewModel>();
            #endregion
            #endregion

            #region Pages
            #region ActivateDetailsPages
            Services.AddTransient<AirFlightAttachmentsPage>();
            Services.AddTransient<HotelServicesActivateDetails>();
            Services.AddTransient<MainActivatePage>();
            Services.AddTransient<TransportServicesActivateDetails>();
            Services.AddTransient<VisaAttachmentsPage>();
            #endregion

            #region DistributorsPages
            #region Main DistributorsPages
            Services.AddTransient<Dis_AccountPage>();
            Services.AddTransient<Dis_DocumentsPage>();
            Services.AddTransient<Dis_ProviderDetailsPage>();
            Services.AddTransient<HomeDistributorsPage>();
            #endregion

            #region Offer
            Services.AddTransient<CreateOfferPage>();
            Services.AddTransient<OfferDetailsPage>();
            Services.AddTransient<OfferViewersPage>();
            #endregion

            #region ResponseDetails
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.AirFlightServicePage>();
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.Dis_ComplaintPopup>();
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.Ds_ReviewPopup>();
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.HotelServicePage>();
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.RequestDetailsPage>();
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.TransportaionServicePage>();
            Services.AddTransient<Pages.DistributorsPages.ResponseDetailes.VisaServicePage>();
            #endregion
            #endregion

            #region MainPopups
            Services.AddTransient<AddAttachmentsPopup>();
            Services.AddTransient<AddressPupop>();
            Services.AddTransient<FullScreenImagePopup>();
            Services.AddTransient<LanguagePopup>();
            #endregion

            #region Shared Pages
            Services.AddTransient<ChangePassPage>();
            Services.AddTransient<LoginPage>();
            Services.AddTransient<NoInternetPage>();
            Services.AddTransient<PdfViewerPage>();
            Services.AddTransient<ProfilePage>();
            Services.AddTransient<ResetPage>();
            Services.AddTransient<SignUpPage>();
            Services.AddTransient<UserPermissionPage>();
            #endregion

            #region TravelAgenciesPages
            #region Main TravelAgenciesPages
            Services.AddTransient<HomeAgencyPage>();
            Services.AddTransient<Tr_AccountPage>();
            Services.AddTransient<Tr_DocumentsPage>();
            Services.AddTransient<Tr_ProviderDetailsPage>();
            #endregion

            #region CreateRequest TravelAgenciesPages
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.AirFlightServicePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.ChooseDistributorPage>();
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.HotelServicePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.MapHotelsPage>();
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.NewRequestPage>();
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.TransportaionServicePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.CreateRequest.VisaServicePage>();
            #endregion

            #region Offer TravelAgenciesPages
            Services.AddTransient<OfferDetailsPage>();
            #endregion

            #region RequestDetails TravelAgenciesPages
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.AirFlightServicePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.BankOrCreditPaymentPage>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.ConfirmResponsePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.HotelServicePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.RequestDetailsPage>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.Tr_ComplaintPopup>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.Tr_ReviewPopup>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.TransportaionServicePage>();
            Services.AddTransient<Pages.TravelAgenciesPages.RequestDetails.VisaServicePage>();
            #endregion
            #endregion

            #region Users Pages
            Services.AddTransient<AddUserPage>();
            Services.AddTransient<UsersPage>();
            #endregion 
            #endregion

            return Services;
        }

        public static void ControlsBackground()
        {

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#endif
            });

            Microsoft.Maui.Handlers.SearchBarHandler.Mapper.AppendToMapping(nameof(SearchBarHandler), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
                var textField = handler.PlatformView.ValueForKey(new Foundation.NSString("searchField")) as UIKit.UITextField;
                if (textField != null)
                {
                    textField.BackgroundColor = UIKit.UIColor.Clear; // Set text field background color
                    textField.ClipsToBounds = true;
                }
#endif
            });


            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(PickerHandler), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#endif
            });


            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(EditorHandler), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#endif
            });


            Microsoft.Maui.Handlers.DatePickerHandler.Mapper.AppendToMapping(nameof(DatePickerHandler), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#endif
            });

            Microsoft.Maui.Handlers.TimePickerHandler.Mapper.AppendToMapping(nameof(TimePickerHandler), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
#endif
            });


        }


    }
}
