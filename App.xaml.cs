using Akavache;
using Newtonsoft.Json;
using Plugin.FirebasePushNotifications;
using System.Globalization;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.DistributorsPages;
using TripBliss.Pages.Shared;
using TripBliss.Pages.TravelAgenciesPages;
using TripBliss.Services.Data;
using TripBliss.ViewModels.DistributorsViewModels.ResponseDetails;


namespace TripBliss
{
    public partial class App : Application
    {
        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        readonly Services.Data.SignalRService _signalRService;
        readonly Services.Data.INotificationService _notificationService;
        private readonly IFirebasePushNotification _firebasePushNotification;
        #endregion

        private bool _isNavigating;

        public static Dictionary<string, object>? PendingNavigationData { get; set; }
        public static bool IsNavigatingFromNotification { get; set; }

        public App(IGenericRepository generic, Services.Data.ServicesService service, Services.Data.SignalRService signalRService, Services.Data.INotificationService notificationService, IFirebasePushNotification firebasePushNotification)
        {
            _service = service;
            _signalRService = signalRService;
            _notificationService = notificationService;
            _firebasePushNotification = firebasePushNotification;
            Rep = generic;

            BlobCache.ApplicationName = "TripBlissDB";
            BlobCache.EnsureInitialized();

            LoadSetting();
            InitializeComponent();


            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(ApiConstants.syncFusionLicence);


            // ✅ الاشتراك في الإشعارات في App
            _notificationService.OnNotificationReceived += OnNotificationReceived;

            // ✅ الاتصال بخادم الإشعارات
            _ = ConnectToNotificationService();

            // ✅ التحقق من الإشعارات المعلقة عند فتح التطبيق
            CheckPendingNotification();

            // إعداد الصفحة الرئيسية
            SetMainPage();


            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Preferences.Default.Get(ApiConstants.username, "")))
            {
                int CatUser = Preferences.Default.Get(ApiConstants.userCategory, 0);
                if (CatUser != 0)
                {
                    MainPage = CatUser switch
                    {
                        2 => new NavigationPage(new HomeAgencyPage(
                            new ViewModels.TravelAgenciesViewModels.Tr_HomeViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification),
                            Rep, _service, _signalRService, _notificationService, _firebasePushNotification)),
                        3 => new NavigationPage(new HomeDistributorsPage(
                            new ViewModels.DistributorsViewModels.Dis_HomeViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification),
                            Rep, _service, _signalRService, _notificationService, _firebasePushNotification)),
                        _ => new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification)))
                    };
                }
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage(new ViewModels.LoginViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification)));
            }
        }


        // ✅ التحقق من الإشعارات المعلقة عند فتح التطبيق
        private void CheckPendingNotification()
        {
            var pendingData = Preferences.Default.Get("PendingNotificationData", "");
            if (!string.IsNullOrEmpty(pendingData))
            {
                try
                {
                    var notification = System.Text.Json.JsonSerializer.Deserialize<NotificationDto>(pendingData);
                    if (notification != null)
                    {
                        Console.WriteLine($"📩 Pending notification found: {notification.Title}");

                        // ✅ عرض التنبيه بعد 1 ثانية (للتأكد من تحميل الصفحة)
                        MainThread.BeginInvokeOnMainThread(async () =>
                        {
                            await Task.Delay(1000);

                            var action = await Current!.MainPage!.DisplayAlert(
                                notification.Title ?? "إشعار جديد",
                                notification.Message,
                                "عرض التفاصيل",
                                "تجاهل"
                            );

                            if (action)
                            {
                                await NavigateToRequestDetails(notification);
                            }
                        });

                        // ✅ مسح الإشعار المعلق
                        Preferences.Default.Remove("PendingNotificationData");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error parsing pending notification: {ex.Message}");
                    Preferences.Default.Remove("PendingNotificationData");
                }
            }
        }

        // ✅ الاتصال بخادم الإشعارات
        private async Task ConnectToNotificationService()
        {
            try
            {
                if (_notificationService == null)
                {
                    Console.WriteLine("❌ _notificationService is null");
                    return;
                }

                if (_notificationService.IsConnected)
                {
                    Console.WriteLine("ℹ️ Already connected to notification service");
                    return;
                }

                var token = await _service.UserToken();
                if (!string.IsNullOrEmpty(token))
                {
                    await _notificationService.ConnectAsync(token);
                    Console.WriteLine($"✅ Connected to notification service: {_notificationService.IsConnected}");
                }
                else
                {
                    Console.WriteLine("❌ No token available for notification connection");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error connecting to notification service: {ex.Message}");
            }
        }

        // ✅ معالج الإشعارات العام
        private void OnNotificationReceived(object? sender, NotificationDto notification)
        {
            Console.WriteLine($"📩 Notification received in App: {notification.Title}");

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // ✅ منع التنقل المتكرر
                    if (_isNavigating) return;
                    _isNavigating = true;

                    // ✅ عرض التنبيه للمستخدم
                    var action = await Current!.MainPage!.DisplayAlert(
                        notification.Title ?? "إشعار جديد",
                        notification.Message,
                        "عرض التفاصيل",
                        "تجاهل"
                    );

                    if (action)
                    {
                        await NavigateToRequestDetails(notification);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error handling notification: {ex.Message}");
                }
                finally
                {
                    _isNavigating = false;
                }
            });
        }

        // ✅ التنقل إلى التفاصيل حسب نوع المستخدم
        private async Task NavigateToRequestDetails(NotificationDto notification)
        {
            try
            {
                if (string.IsNullOrEmpty(notification.RequestId))
                {
                    await Current!.MainPage!.DisplayAlert("خطأ", "معرف الطلب غير موجود", "موافق");
                    return;
                }

                // ✅ الحصول على نوع المستخدم الحالي
                int userCategory = Preferences.Default.Get(ApiConstants.userCategory, 0);
                bool isTravelAgency = userCategory == 1;
                bool isDistributor = userCategory == 3;

                if (isTravelAgency)
                {
                    // ✅ وكيل سياحي
                    if (notification.Type == "NewOffer" && !string.IsNullOrEmpty(notification.OfferId))
                    {
                        // عرض جديد من موزع - افتح تفاصيل الطلب مع العرض
                        await NavigateToTravelAgencyRequestWithOffer(notification);
                    }
                    else
                    {
                        // طلب جديد أو تحديث - افتح تفاصيل الطلب
                        await NavigateToTravelAgencyRequestDetails(notification);
                    }
                }
                else if (isDistributor)
                {
                    // ✅ موزع
                    if (notification.Type == "NewRequest")
                    {
                        // طلب جديد من وكيل
                        await NavigateToDistributorRequestDetails(notification);
                    }
                    else if (notification.Type == "NewOffer" && !string.IsNullOrEmpty(notification.OfferId))
                    {
                        // عرض جديد من موزع - افتح تفاصيل الطلب مع العرض
                        await NavigateToTravelAgencyRequestWithOffer(notification);
                    }
                    else
                    {
                        await NavigateToDistributorRequestDetails(notification);
                    }
                }
                else
                {
                    // ✅ مستخدم غير معروف - افتح صفحة تسجيل الدخول
                    await Current!.MainPage!.Navigation.PushAsync(new LoginPage(new ViewModels.LoginViewModel(Rep, _service, _signalRService, _notificationService, _firebasePushNotification)));
                }

                // ✅ تحديث الإشعار كمقروء
                await _notificationService.SendNotificationReadAsync(notification.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error navigating to request details: {ex.Message}");
                await Current!.MainPage!.DisplayAlert("خطأ", "حدث خطأ أثناء فتح الطلب", "موافق");
            }
        }

        #region Navigation Methods for Travel Agency
        private async Task NavigateToTravelAgencyRequestDetails(NotificationDto notification)
        {
            var viewModel = new ViewModels.TravelAgenciesViewModels.RequestDetails.Tr_D_RequestDetailsViewModel(
                notification.RequestId,
                Rep,
                _service,
                _signalRService,
                _notificationService,
                _firebasePushNotification
            );

            await Current!.MainPage!.Navigation.PushAsync(
                new Pages.TravelAgenciesPages.RequestDetails.RequestDetailsPage(viewModel)
            );
        }

        private async Task NavigateToTravelAgencyRequestWithOffer(NotificationDto notification)
        {
            var viewModel = new ViewModels.TravelAgenciesViewModels.RequestDetails.Tr_D_RequestDetailsViewModel(
                notification.RequestId,
                Rep,
                _service,
                _signalRService,
                _notificationService,
                _firebasePushNotification
            );

            await Current!.MainPage!.Navigation.PushAsync(
                new Pages.TravelAgenciesPages.RequestDetails.RequestDetailsPage(viewModel)
            );
        }
        #endregion

        #region Navigation Methods for Distributor
        private async Task NavigateToDistributorRequestDetails(NotificationDto notification)
        {
            var responseId = notification.OfferId; // ✅ الـ ResponseId مخزن في OfferId

            if (string.IsNullOrEmpty(responseId))
            {
                // إذا لم يوجد ResponseId، افتح صفحة الطلب
                await NavigateToDistributorRequestDetails(notification);
                return;
            }

            var requestViewModel = new ViewModels.DistributorsViewModels.ResponseDetails.Dis_D_RequestDetailsViewModel(
                responseId,
                Rep,
                _service,
                _signalRService,
                _notificationService,
                _firebasePushNotification
            );

            var paymentViewModel = new ViewModels.DistributorsViewModels.ResponseDetails.Dis_D_PaymentViewModel(
                new ResponseWithDistributorResponse(),
                Rep,
                _service
            );

            await Current!.MainPage!.Navigation.PushAsync(
                new Pages.DistributorsPages.ResponseDetailes.RequestDetailsPage(
                    requestViewModel,
                    paymentViewModel,
                    Rep,
                    _service
                )
            );
        }


        #endregion

        private async void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, _service));
                return;
            }
        }

        protected async override void OnStart()
        {
            base.OnStart();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, _service));
                return;
            }
        }

        protected async override void OnResume()
        {
            base.OnResume();
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current!.MainPage!.Navigation.PushAsync(new NoInternetPage(Rep, _service));
                return;
            }
        }

        void LoadSetting()
        {
            string Lan = Preferences.Default.Get("Lan", "en");
            if (Lan == "ar")
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar");
                CultureInfo.CurrentUICulture = new CultureInfo("ar");
            }
            else
            {
                CultureInfo.CurrentCulture = new CultureInfo("en");
                CultureInfo.CurrentUICulture = new CultureInfo("en");
            }
        }


    }
}
