using System;
using System.Collections.Generic;
using System.Text;


namespace TripBliss.Constants
{
    public class ApiConstants
    {
        public static string BaseApiUrl; 

        public const string syncFusionLicence = "Ngo9BigBOggjHTQxAR8/V1NCaF1cWWhAYVZpR2Nbe055flRGal9VVAciSV9jS3pTfkVlWXZfcHdQRGZbVQ=="; //Version= 24.x.x

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
        public static string GetDistributorCompaniesApi = "api/DistributorCompany/current";
        // End AllDistributorCompanys Api 
        #endregion


    }
}
