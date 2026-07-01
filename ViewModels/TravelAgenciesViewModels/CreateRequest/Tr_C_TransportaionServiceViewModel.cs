using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripBliss.Constants;
using TripBliss.Helpers;
using TripBliss.Models;
using TripBliss.Pages.Shared;

namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
{
    public partial class Tr_C_TransportaionServiceViewModel : BaseViewModel
    {
        #region Prop
        [ObservableProperty]
        RequestTravelAgencyTransportRequest? transportRequestModel = new RequestTravelAgencyTransportRequest();
        [ObservableProperty]
        RequestTravelAgencyTransportResponse? transportResponseModel = new RequestTravelAgencyTransportResponse();

        [ObservableProperty]
        ObservableCollection<CarBrandResponse> carBrands = new ObservableCollection<CarBrandResponse>();
        [ObservableProperty]
        ObservableCollection<CarModelResponse> carModel = new ObservableCollection<CarModelResponse>();
        [ObservableProperty]
        ObservableCollection<CarTypeResponse> carTypes = new ObservableCollection<CarTypeResponse>();

        [ObservableProperty]
        CarBrandResponse selectrdBrand = new CarBrandResponse();
        [ObservableProperty]
        CarModelResponse selectrdModel = new CarModelResponse();
        [ObservableProperty]
        CarTypeResponse selectrdType = new CarTypeResponse();

        #endregion
        public delegate void TransportDelegte(RequestTravelAgencyTransportRequest TransportRequest, RequestTravelAgencyTransportResponse TransportResponse);
        public event TransportDelegte TransportClose;

        #region Services
        IGenericRepository Rep;
        readonly Services.Data.ServicesService _service;
        #endregion

        #region Const
        public Tr_C_TransportaionServiceViewModel(IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            _service = service;
            TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
            TransportRequestModel!.DateVM = DateTime.Now;
            Init();
        }
        public Tr_C_TransportaionServiceViewModel(RequestTravelAgencyTransportResponse model, IGenericRepository generic, Services.Data.ServicesService service)
        {
            Rep = generic;
            //TransportResponseModel = model;
            TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
            TransportRequestModel!.DateVM = DateTime.Now;
            _service = service;
            Init(model);
        }
        #endregion

        #region Methods
        async Task Init(RequestTravelAgencyTransportResponse model)
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
            UserDialogs.Instance.HideHud();

            TransportRequestModel = new RequestTravelAgencyTransportRequest
            {
                FromLocation = model.FromLocation,
                ToLocation = model.ToLocation,
                Date = model.Date,
                DateVM = model.Date.ToDateTime(new TimeOnly(0, 0)),
                Notes = model.Notes,
                Time = model.Time,
                TransportCount = model.TransportCount,
            };
            SelectrdBrand = CarBrands.FirstOrDefault(a => a.Id == model.CarBrandId)!;
            SelectrdModel = CarModel.FirstOrDefault(a => a.Id == model.CarModelId)!;
            SelectrdType = CarTypes.FirstOrDefault(a => a.Id == model.CarTypeId)!;

        }
        async Task Init()
        {
            UserDialogs.Instance.ShowLoading();
            await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
            UserDialogs.Instance.HideHud();
        }
        async Task GetCarBrands()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarBrandResponse>>(ApiConstants.GetAllCarBrandsApi, UserToken);

                if (json != null)
                {
                    CarBrands = json;
                }
            }

        }

        async Task GetCarModels()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarModelResponse>>(ApiConstants.GetAllCarModelsApi, UserToken);

                if (json != null)
                {
                    CarModel = json;
                }
            }

        }

        async Task GetCarTypes()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await Rep.GetAsync<ObservableCollection<CarTypeResponse>>(ApiConstants.GetAllCarTypesApi, UserToken);

                if (json != null)
                {
                    CarTypes = json;
                }
            }

        }
        #endregion

        #region RelayCommand
        [RelayCommand]
        async Task OnApply(RequestTravelAgencyTransportRequest request)
        {
            if (SelectrdType == null || SelectrdType?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarType, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectrdBrand == null || SelectrdBrand?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarBrand, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (SelectrdModel == null || SelectrdModel?.Id == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarModel, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.TransportCount == 0)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_TransportCount, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (request.DateVM < DateTime.Now)
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Date, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(request.FromLocation))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FromLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else if (string.IsNullOrEmpty(request.ToLocation))
            {
                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_ToLocation, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
                await toast.Show();
            }
            else
            {
                IsBusy = false;
                UserDialogs.Instance.ShowLoading();

                request.Date = DateOnly.FromDateTime(request.DateVM);

                request.CarBrandId = SelectrdBrand!.Id;
                request.CarTypeId = SelectrdType!.Id;
                request.CarModelId = SelectrdModel!.Id;

                TransportResponseModel!.CarBrandId = SelectrdBrand!.Id;
                TransportResponseModel.CarTypeId = SelectrdType!.Id;
                TransportResponseModel.CarModelId = SelectrdModel!.Id;
                TransportResponseModel!.FromLocation = request.FromLocation;
                TransportResponseModel.ToLocation = request.ToLocation;
                TransportResponseModel.Date = request.Date;
                TransportResponseModel.TransportCount = request.TransportCount;
                TransportResponseModel.TypeName = SelectrdType.TypeName;
                TransportResponseModel.TypeNameAr = SelectrdType.TypeNameAr;
                TransportResponseModel.Notes = request.Notes;
                Controls.StaticMember.EndRequestStatic = (request.Date > DateOnly.FromDateTime(Controls.StaticMember.EndRequestStatic)) ? request.Date.ToDateTime(new TimeOnly(0, 0)) : Controls.StaticMember.EndRequestStatic;

                TransportClose.Invoke(request, TransportResponseModel);
                await App.Current!.MainPage!.Navigation.PopAsync();

                UserDialogs.Instance.HideHud();
                IsBusy = true;
            }

        }

        [RelayCommand]
        async Task OnBackButtonClicked()
        {
            await App.Current!.MainPage!.Navigation.PopAsync();
        }
        #endregion
    }
}


//using CommunityToolkit.Maui.Alerts;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Controls.UserDialogs.Maui;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TripBliss.Constants;
//using TripBliss.Helpers;
//using TripBliss.Models;
//using TripBliss.Pages.Shared;
//using TripBliss.Services; // ✅ إضافة لخدمة NHTSA

//namespace TripBliss.ViewModels.TravelAgenciesViewModels.CreateRequest
//{
//    public partial class Tr_C_TransportaionServiceViewModel : BaseViewModel
//    {
//        #region Prop

//        [ObservableProperty]
//        RequestTravelAgencyTransportRequest? transportRequestModel = new RequestTravelAgencyTransportRequest();
//        [ObservableProperty]
//        RequestTravelAgencyTransportResponse? transportResponseModel = new RequestTravelAgencyTransportResponse();

//        // ========================================================================
//        // ✅ الخصائص الجديدة (NHTSA API)
//        // ========================================================================

//        /// <summary>
//        /// قائمة الماركات من NHTSA API
//        /// </summary>
//        [ObservableProperty]
//        ObservableCollection<CarMakeModel> carMakes = new ObservableCollection<CarMakeModel>();

//        /// <summary>
//        /// قائمة الموديلات من NHTSA API
//        /// </summary>
//        [ObservableProperty]
//        ObservableCollection<CarModelData> carModels = new ObservableCollection<CarModelData>();

//        /// <summary>
//        /// الماركة المختارة من NHTSA API
//        /// </summary>
//        [ObservableProperty]
//        CarMakeModel selectedMake = new CarMakeModel();

//        /// <summary>
//        /// الموديل المختار من NHTSA API
//        /// </summary>
//        [ObservableProperty]
//        CarModelData selectedModel = new CarModelData();

//        /// <summary>
//        /// السنة مثبتة على العام الحالي
//        /// </summary>
//        [ObservableProperty]
//        int selectedYear = DateTime.Now.Year;

//        // ========================================================================
//        // ❌ الخصائص القديمة (معلقة - من قاعدة البيانات المحلية)
//        // ========================================================================

//        // [ObservableProperty]
//        // ObservableCollection<CarBrandResponse> carBrands = new ObservableCollection<CarBrandResponse>();
//        // 
//        // [ObservableProperty]
//        // ObservableCollection<CarModelResponse> carModel = new ObservableCollection<CarModelResponse>();
//        // 
//        // [ObservableProperty]
//        // ObservableCollection<CarTypeResponse> carTypes = new ObservableCollection<CarTypeResponse>();
//        // 
//        // [ObservableProperty]
//        // CarBrandResponse selectrdBrand = new CarBrandResponse();
//        // 
//        // [ObservableProperty]
//        // CarModelResponse selectrdModel = new CarModelResponse();
//        // 
//        // [ObservableProperty]
//        // CarTypeResponse selectrdType = new CarTypeResponse();

//        #endregion

//        public delegate void TransportDelegte(RequestTravelAgencyTransportRequest TransportRequest, RequestTravelAgencyTransportResponse TransportResponse);
//        public event TransportDelegte TransportClose;

//        #region Services
//        IGenericRepository Rep;
//        readonly Services.Data.ServicesService _service;

//        // ✅ إضافة خدمة NHTSA
//        readonly NHTSAService _nhtsaService;
//        #endregion

//        #region Const

//        // ========================================================================
//        // ✅ الكونستركتور الجديد (مع NHTSA API)
//        // ========================================================================

//        public Tr_C_TransportaionServiceViewModel(
//            IGenericRepository generic,
//            Services.Data.ServicesService service)
//        {
//            Rep = generic;
//            _service = service;

//            // ✅ إنشاء خدمة NHTSA مباشرة (بدون DI)
//            _nhtsaService = new NHTSAService();

//            TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
//            TransportRequestModel!.DateVM = DateTime.Now;

//            // ✅ جلب الماركات من NHTSA API عند تحميل الصفحة
//            LoadCarMakes();
//        }

//        public Tr_C_TransportaionServiceViewModel(
//            RequestTravelAgencyTransportResponse model,
//            IGenericRepository generic,
//            Services.Data.ServicesService service) 
//        {
//            Rep = generic;
//            _service = service;

//            // ✅ إنشاء خدمة NHTSA مباشرة (بدون DI)
//            _nhtsaService = new NHTSAService();

//            TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
//            TransportRequestModel!.DateVM = DateTime.Now;

//            // ✅ جلب الماركات من NHTSA API مع تعبئة النموذج السابق
//            LoadCarMakes(model);

//        }

//        // ========================================================================
//        // ❌ الكونستركتور القديم (معلق - من قاعدة البيانات المحلية)
//        // ========================================================================

//        // public Tr_C_TransportaionServiceViewModel(IGenericRepository generic , Services.Data.ServicesService service)
//        // {
//        //     Rep = generic;
//        //     _service = service;
//        //     TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
//        //     TransportRequestModel!.DateVM = DateTime.Now;
//        //     Init();
//        // }
//        // 
//        // public Tr_C_TransportaionServiceViewModel(RequestTravelAgencyTransportResponse model , IGenericRepository generic , Services.Data.ServicesService service)
//        // {
//        //     Rep = generic;
//        //     TransportRequestModel!.Date = DateOnly.FromDateTime(DateTime.Now);
//        //     TransportRequestModel!.DateVM = DateTime.Now;
//        //     _service = service;
//        //     Init(model);
//        // }

//        #endregion

//        #region Methods

//        // ========================================================================
//        // ✅ الطرق الجديدة (NHTSA API)
//        // ========================================================================

//        /// <summary>
//        /// جلب جميع الماركات من NHTSA API عند تحميل الصفحة
//        /// </summary>
//        async void LoadCarMakes(RequestTravelAgencyTransportResponse model = null)
//        {
//            try
//            {
//                UserDialogs.Instance.ShowLoading("جاري تحميل الماركات...");

//                // استدعاء NHTSA API لجلب جميع الماركات
//                var makes = await _nhtsaService.GetAllMakesAsync();

//                // تحويل النتيجة إلى ObservableCollection وترتيبها أبجدياً
//                CarMakes = new ObservableCollection<CarMakeModel>(
//                    makes.Select(m => new CarMakeModel(m)).OrderBy(m => m.Name)
//                );

//                UserDialogs.Instance.HideHud();

//                // إذا كان هناك نموذج سابق (تعديل)، نختار الماركة والموديل
//                if (model != null)
//                {
//                    TransportRequestModel = new RequestTravelAgencyTransportRequest
//                    {   
//                        FromLocation = model.FromLocation,
//                        ToLocation = model.ToLocation,
//                        Date = model.Date,
//                        DateVM = model.Date.ToDateTime(new TimeOnly(0, 0)),
//                        Notes = model.Notes,
//                        Year = model.Year,
//                        Time = model.Time,
//                        TransportCount = model.TransportCount,
//                    };


//                    SelectedMake = CarMakes.FirstOrDefault(a => a.Name == model.BrandName)!;
//                    SelectedYear = TransportRequestModel.Year;

//                    // تعيين الماركة المختارة سابقاً (إن وجدت)
//                    SelectedMake = CarMakes.FirstOrDefault(a => a.Id == model.CarBrandId) ?? new CarMakeModel();

//                    // جلب الموديلات للماركة المختارة
//                    if (SelectedMake.Id != 0)
//                    {
//                        await LoadCarModels(SelectedMake.Name);

//                        // تعيين الموديل المختار سابقاً (إن وجد)
//                        SelectedModel = CarModels.FirstOrDefault(a => a.Name == model.ModelName) ?? new CarModelData();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                UserDialogs.Instance.HideHud();
//                await Toast.Make($"حدث خطأ أثناء تحميل الماركات: {ex.Message}",
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15).Show();
//            }
//        }

//        /// <summary>
//        /// جلب الموديلات لماركة معينة من NHTSA API
//        /// </summary>
//        async Task LoadCarModels(string makeName)
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(makeName))
//                    return;

//                UserDialogs.Instance.ShowLoading("جاري تحميل الموديلات...");

//                CarModels.Clear();

//                // استدعاء NHTSA API لجلب الموديلات للماركة والسنة المحددة
//                var models = await _nhtsaService.GetModelsForMakeYearAsync(makeName, SelectedYear);

//                // تحويل النتيجة إلى ObservableCollection وترتيبها أبجدياً
//                CarModels = new ObservableCollection<CarModelData>(
//                    models.Select(m => new CarModelData(m, SelectedMake.Id)).OrderBy(m => m.Name)
//                );

//                UserDialogs.Instance.HideHud();
//            }
//            catch (Exception ex)
//            {
//                UserDialogs.Instance.HideHud();
//                await Toast.Make($"حدث خطأ أثناء تحميل الموديلات: {ex.Message}",
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15).Show();
//            }
//        }

//        // ========================================================================
//        // ❌ الطرق القديمة (معلقة - من قاعدة البيانات المحلية)
//        // ========================================================================

//        // async Task Init(RequestTravelAgencyTransportResponse model)
//        // {
//        //     UserDialogs.Instance.ShowLoading();
//        //     await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
//        //     UserDialogs.Instance.HideHud();
//        // 
//        //     TransportRequestModel = new RequestTravelAgencyTransportRequest
//        //     {
//        //         FromLocation = model.FromLocation,
//        //         ToLocation = model.ToLocation,
//        //         Date = model.Date,
//        //         DateVM = model.Date.ToDateTime(new TimeOnly(0, 0)),
//        //         Notes = model.Notes,
//        //         Time = model.Time,
//        //         TransportCount = model.TransportCount,
//        //     };
//        //     SelectrdBrand = CarBrands.FirstOrDefault(a=>a.Id == model.CarBrandId)!;
//        //     SelectrdModel = CarModel.FirstOrDefault(a=>a.Id == model.CarModelId)!;
//        //     SelectrdType = CarTypes.FirstOrDefault(a=>a.Id == model.CarTypeId)!;
//        // 
//        // }
//        // 
//        // async Task Init()
//        // {
//        //     UserDialogs.Instance.ShowLoading();
//        //     await Task.WhenAll(GetCarBrands(), GetCarModels(), GetCarTypes());
//        //     UserDialogs.Instance.HideHud();
//        // }
//        // 
//        // async Task GetCarBrands()
//        // {
//        //     if (Connectivity.NetworkAccess == NetworkAccess.Internet)
//        //     {
//        //         string UserToken = await _service.UserToken();
//        //         var json = await Rep.GetAsync<ObservableCollection<CarBrandResponse>>(ApiConstants.GetAllCarBrandsApi, UserToken);
//        //         if (json != null)
//        //         {
//        //             CarBrands = json;
//        //         }
//        //     }
//        // }
//        // 
//        // async Task GetCarModels()
//        // {
//        //     if (Connectivity.NetworkAccess == NetworkAccess.Internet)
//        //     {
//        //         string UserToken = await _service.UserToken();
//        //         var json = await Rep.GetAsync<ObservableCollection<CarModelResponse>>(ApiConstants.GetAllCarModelsApi, UserToken);
//        //         if (json != null)
//        //         {
//        //             CarModel = json;
//        //         }
//        //     }
//        // }
//        // 
//        // async Task GetCarTypes()
//        // {
//        //     if (Connectivity.NetworkAccess == NetworkAccess.Internet)
//        //     {
//        //         string UserToken = await _service.UserToken();
//        //         var json = await Rep.GetAsync<ObservableCollection<CarTypeResponse>>(ApiConstants.GetAllCarTypesApi, UserToken);
//        //         if (json != null)
//        //         {
//        //             CarTypes = json;
//        //         }
//        //     }
//        // }

//        #endregion

//        #region RelayCommand

//        /// <summary>
//        /// ✅ عند تغيير الماركة المختارة، يتم جلب الموديلات تلقائياً
//        /// </summary>
//        partial void OnSelectedMakeChanged(CarMakeModel value)
//        {
//            if (value != null && !string.IsNullOrEmpty(value.Name) && value.Id != 0)
//            {
//                // جلب الموديلات لهذه الماركة تلقائياً
//                Task.Run(async () => await LoadCarModels(value.Name));
//            }
//        }

//        [RelayCommand]
//        async Task OnApply(RequestTravelAgencyTransportRequest request)
//        {
//            // ====================================================================
//            // ✅ التحقق من صحة البيانات (باستخدام الخصائص الجديدة)
//            // ====================================================================

//            // ✅ التحقق من اختيار الماركة
//            if (SelectedMake == null || SelectedMake?.Id == 0)
//            {
//                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarBrand,
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//                await toast.Show();
//                return;
//            }

//            // ✅ التحقق من اختيار الموديل
//            if (SelectedModel == null || SelectedModel?.Id == 0)
//            {
//                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarModel,
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//                await toast.Show();
//                return;
//            }

//            // ❌ تم إلغاء التحقق من CarType (نوع السيارة)
//            // if (SelectrdType == null || SelectrdType?.Id == 0)
//            // {
//            //     var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarType, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//            //     await toast.Show();
//            // }
//            // else if (SelectrdBrand == null || SelectrdBrand?.Id == 0)
//            // {
//            //     var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarBrand, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//            //     await toast.Show();
//            // }
//            // else if (SelectrdModel == null || SelectrdModel?.Id == 0)
//            // {
//            //     var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_SelectCarModel, CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//            //     await toast.Show();
//            // }

//            else if (request.TransportCount == 0)
//            {
//                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_TransportCount,
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//                await toast.Show();
//            }
//            else if (request.DateVM < DateTime.Now)
//            {
//                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_Date,
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//                await toast.Show();
//            }
//            else if (string.IsNullOrEmpty(request.FromLocation))
//            {
//                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_FromLocation,
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//                await toast.Show();
//            }
//            else if (string.IsNullOrEmpty(request.ToLocation))
//            {
//                var toast = Toast.Make(TripBliss.Resources.Language.AppResources.Required_ToLocation,
//                    CommunityToolkit.Maui.Core.ToastDuration.Long, 15);
//                await toast.Show();
//            }
//            else
//            {
//                IsBusy = false;
//                UserDialogs.Instance.ShowLoading();

//                request.Date = DateOnly.FromDateTime(request.DateVM);

//                // ====================================================================
//                // ✅ تعيين المعرفات (باستخدام الخصائص الجديدة)
//                // ====================================================================

//                //request.CarBrandId = SelectedMake!.Id;
//                //request.CarModelId = SelectedModel!.Id;
//                //request.CarTypeId = 0; // ✅ تم إلغاء نوع السيارة

//                // ====================================================================
//                // ❌ الكود القديم (معلق)
//                // ====================================================================

//                // request.CarBrandId = SelectrdBrand!.Id;
//                // request.CarTypeId = SelectrdType!.Id;
//                // request.CarModelId = SelectrdModel!.Id;

//                // ====================================================================
//                // ✅ تعيين الـ Response (باستخدام الخصائص الجديدة)
//                // ====================================================================

//                //TransportResponseModel!.CarBrandId = SelectedMake!.Id;
//                //TransportResponseModel.CarModelId = SelectedModel!.Id;
//                //TransportResponseModel.CarTypeId = 0; // ✅ تم إلغاء نوع السيارة

//                // ====================================================================
//                // ❌ الكود القديم (معلق)
//                // ====================================================================

//                // TransportResponseModel!.CarBrandId = SelectrdBrand!.Id;
//                // TransportResponseModel.CarTypeId = SelectrdType!.Id;
//                // TransportResponseModel.CarModelId = SelectrdModel!.Id;

//                // ====================================================================
//                // ✅ تعبئة باقي البيانات
//                // ====================================================================

//                TransportResponseModel.FromLocation = request.FromLocation;
//                TransportResponseModel.ToLocation = request.ToLocation;
//                TransportResponseModel.Date = request.Date;
//                TransportResponseModel.TransportCount = request.TransportCount;
//                TransportResponseModel.Notes = request.Notes;

//                // ✅ إضافة أسماء الموديل 

//                request.CarBrandName = SelectedMake.Name;
//                TransportResponseModel.BrandName = SelectedMake.Name;
//                TransportResponseModel.BrandNameAr = SelectedMake.Name;

//                // ✅ إضافة أسماء الموديل 
//                request.CarModelName = SelectedModel.Name;
//                TransportResponseModel.ModelName = SelectedModel.Name;
//                TransportResponseModel.ModelNameAr = SelectedModel.Name;

//                // ====================================================================
//                // ❌ الكود القديم (معلق)
//                // ====================================================================

//                // TransportResponseModel.TypeName = SelectrdType.TypeName;
//                // TransportResponseModel.TypeNameAr = SelectrdType.TypeNameAr;

//                Controls.StaticMember.EndRequestStatic = (request.Date > DateOnly.FromDateTime(Controls.StaticMember.EndRequestStatic))
//                    ? request.Date.ToDateTime(new TimeOnly(0, 0))
//                    : Controls.StaticMember.EndRequestStatic;

//                TransportClose.Invoke(request, TransportResponseModel);
//                await App.Current!.MainPage!.Navigation.PopAsync();

//                UserDialogs.Instance.HideHud();
//                IsBusy = true;
//            }
//        }

//        [RelayCommand]
//        async Task OnBackButtonClicked()
//        {
//            await App.Current!.MainPage!.Navigation.PopAsync();
//        }
//        #endregion
//    }
//}