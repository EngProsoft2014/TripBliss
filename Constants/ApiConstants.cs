﻿using System;
using System.Collections.Generic;
using System.Text;


namespace TripBliss.Constants
{
    public class ApiConstants
    {
        public static string BaseApiUrl; 

        public const string syncFusionLicence = "Ngo9BigBOggjHTQxAR8/V1NCaF1cWWhIfkxwWmFZfVpgfF9GZ1ZUTGYuP1ZhSXxXdkxjX39WcHNQT2NfVUI="; //Version= 24.x.x

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
    }
}

//