using System;
using System.Collections.Generic;
using System.Text;


namespace TripBliss.Constants
{
    public class ApiConstants
    {
        public static string BaseApiUrl;

        //public const string syncFusionLicence = "Ngo9BigBOggjHTQxAR8/V1NCaF1cWWhIfkxwWmFZfVpgfF9GZ1ZUTGYuP1ZhSXxXdkxhWn5Xc3BQRmVbUUE="; //Version= 24.x.x
        public const string syncFusionLicence = "Ngo9BigBOggjHTQxAR8/V1NDaF5cWWtCf1FpRmJGdld5fUVHYVZUTXxaS00DNHVRdkdnWXZcc3VcRWldUkN0V0Y="; //Version= 27.x.x


        #region Preferences Keys
        // Preferences Key
        public static string userid = "userid";
        public static string email = "email";
        public static string username = "username";
        public static string password = "password";
        public static string userPermision = "userPermision";
        public static string userCategory = "userCategory"; //1 = system , 2 = Travel Agency , 3 = Distributor
        public static string travelAgencyCompanyId = "travelAgencyCompanyId";
        public static string distributorCompanyId = "distributorCompanyId";
        public static string permissions = "permissions";
        public static string review = "review";
        //End Preferences Key 
        #endregion

        #region Login & Register Api
        // Login Api
        public static string LoginApi = "api/ApplicationUser/Login";
        // End Login Api

        // Register Api
        public static string RegisterApi = "api/ApplicationUser/Register";
        // End Register Api 
        #endregion

        #region Hotel Service Apis
        // AllLocations Api
        public static string GetAllLocationsApi = "api/Location/current";
        // End AllLocations Api

        // AllHotels Api
        public static string GetAllHotelsApi = "api/Hotel/current";
        // End AllHotels Api

        // AllMeals Api
        public static string GetAllMealsApi = "api/Meal/current";
        // End AllMeals Api

        // AllRoomTypes Api
        public static string GetAllRoomTypesApi = "api/RoomType/current";
        // End RoomTypes Api

        // AllRoomViews Api
        public static string GetAllRoomViewsApi = "api/RoomView/current";
        // End AllRoomViews Api 
        #endregion

        #region Visa Service Api
        // AllVisa Api
        public static string GetVisasApi = "api/Visa/current";
        // End AllVisa Api 
        #endregion

        #region AirFlight Service
        // AllAirFlight Api
        public static string GetAllAirFlightApi = "api/AirFlight/current";
        // End AllAirFlight Api

        // AllClassesAirFlight Api
        public static string GetAllClassesAirFlightApi = "api/ClassAirFlight/current";
        // End AllClassesAirFlight Api 
        #endregion

        #region Transportation Service Api
        // AllCarBrands Api
        public static string GetAllCarBrandsApi = "api/CarBrand/current";
        // End AllCarBrands Api 

        // AllCarModels Api
        public static string GetAllCarModelsApi = "api/CarModel/current";
        // End AllCarModels Api 

        // AllCarTypes Api
        public static string GetAllCarTypesApi = "api/CarType/current";
        // End AllCarTypes Api  
        #endregion

        #region Distributor Api
        // AllDistributorCompanys Api
        public static string GetDistributorCompaniesApi = "api/DistributorCompany/current/";
        // End AllDistributorCompanys Api 
        #endregion

        #region favourites  Api
        // Allfavourites  Api
        public static string GetfavouritesApi = "TravelAgencywith/";
        // End Allfavourites  Api 

        // Addfavourites  Api
        public static string AddfavouritesApi = "TravelAgencywith/";
        // End Addfavourites  Api 

        // Delete favourites  Api
        public static string DeletefavouritesApi = "TravelAgencywith/";
        // End Delete favourites  Api 
        #endregion

        #region Requests Api
        // Add Request Api
        public static string AddRequestApi = "TravelAgency/";
        // End Add Request Api 

        // Get All Request Api
        public static string AllRequestApi = "TravelAgency/";
        // End Get All Request Api 


        // Get Request DetailesApi
        public static string RequestDetailesApi = "TravelAgency/";
        // End Get Request Detailes Api 


        #endregion

        #region Response Dist
        // Get All Response Dist Api
        public static string AllResponseDistApi = "Distributor/";
        // End Get All Response Dist Api 

        // Get  Response Detalis Dist Api
        public static string ResponseDetailsDistApi = "Distributor/";
        // End Get  Response Detalis Dist Api  
        #endregion

        #region Payment
        // Get All Payment Api
        public static string AllPaymentApi = "ResponseWithDistributor/";
        // End Get All Payment Api  
        #endregion

        #region Hotel Active
        // Get All Hotel Active Details Api
        public static string HotelActive = "api/ResponseWithDistributorHotelDetails/ResponseWithDistributor/";
        // End Get All Hotel Active Details Api  


        // Delete Active Details Api
        public static string DeleteHotelActive = "api/ResponseWithDistributorHotelDetails/ResponseWithDistributorHotel/";
        // End Delete Active Details Api   
        #endregion

        #region Transport Active
        // Get All Transport Active Details Api
        public static string TransportActive = "api/ResponseWithDistributorTransportDetails/ResponseWithDistributor/";
        // End Get All Transport Active Details Api  


        // Delete Active Details Api
        public static string DeleteTransportActive = "api/ResponseWithDistributorTransportDetails/ResponseWithDistributorTransport/";
        // End Delete Active Details Api   
        #endregion

        #region AirFlight Active
        // Get All AirFlight Active Details Api
        public static string AirFlightActive = "api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributor/";
        // End Get All AirFlight Active Details Api  


        // Delete Active Details Api
        public static string DeleteAirFlightActive = "api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributorAirFlight/";
        // End Delete Active Details Api   
        #endregion

        #region Visa Active
        // Get All Visa Active Details Api
        public static string VisaActive = "api/ResponseWithDistributorVisaDetails/ResponseWithDistributor/";
        // End Get All Visa Active Details Api  


        // Delete Active Details Api
        public static string DeleteVisaActive = "api/ResponseWithDistributorVisaDetails/ResponseWithDistributorVisa/";
        // End Delete Active Details Api   
        #endregion

        #region Guest
        // Get All Guests Details Api
        public static string GuestApi = "TravelAgencyCompany/";
        // End Get All Guests Details Api   
        #endregion

        #region Delete  AirFlight Image
        // Delete  AirFlight Image Api
        public static string DeleteAirFlightImageApi = "api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributorAirFlight/";
        // End Delete  AirFlight Image Api   
        #endregion

        #region Delete All AirFlight Image
        // Delete All AirFlight Image Api
        public static string DeleteAllAirFlightImageApi = "api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributorAirFlight/";
        // End Delete All AirFlight Image Api   
        #endregion

        #region Get AirFlight Image
        // AirFlight Attachments Api
        public static string GetAirFlightImageApi = "api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributor/";
        // End Get AirFlight Image Api   
        #endregion

        #region Post AirFlight Image
        // Post AirFlight Image Api
        public static string PostAirFlightImageApi = "api/ResponseWithDistributorAirFlightDetails/ResponseWithDistributor/";
        // End Post AirFlight Image Api   
        #endregion

        #region Delete Visa Image
        //Delete Visa Image Api
        public static string DeleteVisaImageApi = "api/ResponseWithDistributorVisaDetails/ResponseWithDistributorVisa/";
        // End Delete Visa Image Api   
        #endregion

        #region Delete All Visa Image
        // Delete All Visa Image Api
        public static string DeleteMultiVisaImageApi = "api/ResponseWithDistributorVisaDetails/ResponseWithDistributorVisa/";
        // End Delete All Visa Image Api   
        #endregion

        #region Get  Visa Image
        // Visa Attachments Api
        public static string GetVisaImageApi = "api/ResponseWithDistributorVisaDetails/ResponseWithDistributor/";
        // End Get Visa Image Api   
        #endregion

        #region Post Visa Image
        // Visa Attachments Api
        public static string PostVisaImageApi = "api/ResponseWithDistributorVisaDetails/ResponseWithDistributor/";
        // End Get Visa Image Api   
        #endregion

        #region Get Tavel Doc
        // Get Tavel Doc Api
        public static string GetTravelDocApi = "TravelAgencyCompany/";
        // End Get Tavel Doce Api   
        #endregion

        #region Post Tavel Doc
        // Get Tavel Doc Api
        public static string PostTravelDocApi = "TravelAgencyCompany/";
        // End Get Tavel Doce Api   
        #endregion

        #region Get Travel User Api

        // Get User Api
        public static string GetTravelUserApi = "UserPermissions/TravelAgencyCompany/";
        // End Get User Api 
        #endregion

        #region Get Dist User Api

        // Get User Api
        public static string GetDistUserApi = "UserPermissions/DistributorCompany/";
        // End Get User Api 
        #endregion

        #region Get Dist Doc
        // Get Dist Doc Api
        public static string GetDistDocApi = "DistributorCompany/";
        // End Get Dist Doce Api   
        #endregion

        #region Post Tavel Doc
        // Get Tavel Doc Api
        public static string PostDistDocApi = "DistributorCompany/";
        // End Get Tavel Doce Api   
        #endregion

        #region Post Verify Email
        // post Verify Email
        public static string PostVerifyApi = "api/ApplicationUser/resend-confirm-email";
        // End post Verify Email
        #endregion

        #region Get Dist Company Details
        // Get Dist Company Details
        public static string GetDistCompanyDetailsApi = "api/DistributorCompany/";
        // End Get Dist Company Details 
        #endregion

        #region put Dist Company Details
        // put Dist Company Details
        public static string PutDistCompanyDetailsApi = "api/DistributorCompany/";
        // End put Dist Company Details 

        // Put Delete Company Account
        public static string PutDSCompanyAccountDelete = "api/DistributorCompany/";
        // Put Delete Company Account
        #endregion

        #region Get Travel Company Details
        // Get Travel Company Details
        public static string GetTravelCompanyDetailsApi = "api/TravelAgencyCompany/";
        // End Get Travel Company Details 
        #endregion

        #region Get Travel Companys
        // Get Travel Companys
        public static string GetTravelCompanysApi = "api/TravelAgencyCompany/current";
        // End Get Travel Companys 
        #endregion

        #region put Travel Company Details
        // put Travel Company Details
        public static string PutTravelCompanyDetailsApi = "api/TravelAgencyCompany/";
        // End put Travel Company Details 

        // Put Delete Company Account
        public static string PutTRCompanyAccountDelete = "api/TravelAgencyCompany/";
        // Put Delete Company Account
        #endregion

        #region Post Reset Password
        // post Reset Password
        public static string PosResetApi = "api/ApplicationUser/forget-Password";
        // End post Reset Password
        #endregion

        #region put Change Password
        // put Change Password
        public static string PutChangePassApi = "Account";
        // End put Change Password
        #endregion

        #region Get User Account
        // Get User Account
        public static string GetUserAccountApi = "Account";
        // Get User Account   
        
        // Put Disable User Account
        public static string PutUserAccountEnableOrDisable = "UserPermissions/UserDisabled/";
        // Put Disable User Account

        #endregion

        #region Premission List
        public static string GetPremissionListApi = "UserPermissions/User/";
        #endregion

        #region Update Premission List
        public static string UpdatePremissionListApi = "UserPermissions/";
        #endregion
    }
}

