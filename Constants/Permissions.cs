using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripBliss.Constants
{
    public class PermissionsValues
    {
        public string? RoleId { get; set; }

        public string? CategoryPermissions { get; set; }

        public string? ClaimTitle { get; set; }

        public string? claimTitleAr { get; set; }

        public string? ClaimValue { get; set; }
    }


    public static class Permissions
    {
        public static List<PermissionsValues> LstPermissions { get; set; } = new List<PermissionsValues>();

        // == Details Request ==
        public const string? CategoryPermissionsDetailsRequest = "Details Request";
        public const string Show_Home_Requests = "DetailsRequest: Show_Home_Requests";
        public const string Show_Active_Distributors_for_Request = "DetailsRequest: Show_Active_Distributors_for_Request";
        public const string Show_Response = "DetailsRequest: Show_Response";
        public const string Accept_Price = "DetailsRequest: Accept_Price";
        public const string Payment = "DetailsRequest: Payment";
        public const string Edit_Service = "DetailsRequest: Edit_Service";
        public const string Show_Attachments = "DetailsRequest: Show_Attachments";
        public const string Add_Attachment = "DetailsRequest: Add_Attachment";
        public const string Edit_Attachment = "DetailsRequest: Edit_Attachment";
        public const string Delete_Attachment = "DetailsRequest: Delete_Attachment";
        public const string Delete_DetailsRequest = "DetailsRequest: Delete_DetailsRequest";
        public const string Delete_ResponseWithDistributer = "DetailsRequest: Delete_ResponseWithDistributer";
        //== Title
        public const string Show_Home_RequestsTitle = "Show Home Requests";
        public const string Show_Active_Distributors_for_RequestTitle = "Show Active Distributors for Request";
        public const string Show_ResponseTitle = "Show Response";
        public const string Accept_PriceTitle = "Accept Price";
        public const string PaymentTitle = "Payment";
        public const string Edit_ServiceTitle = "Edit Service";
        public const string Show_AttachmentsTitle = "Show Attachments";
        public const string Add_AttachmentTitle = "Add Attachment";
        public const string Edit_AttachmentTitle = "Edit Attachment";
        public const string Delete_AttachmentTitle = "Delete Attachment";
        public const string Delete_DetailsRequestTitle = "Delete Details Request";
        public const string Delete_ResponseWithDistributerTitle = "Delete Response With Distributer";

        // == Request Travel Agency ==
        public const string? CategoryPermissionsRequestTravelAgency = "Request Travel Agency";
        public const string TR_Show_Distributors = "RequestTravelAgency: TR_Show_Distributors";
        public const string TR_Add_Request = "RequestTravelAgency: TR_Add_Request";
        public const string DS_Show_Agencies = "RequestTravelAgency: DS_Show_Agencies";
        //== Title
        public const string TR_Show_DistributorsTitle = "TR Show Distributors";
        public const string TR_Add_RequestTitle = "TR Add Request";
        public const string DS_Show_AgenciesTitle = "DS Show Agencies";

        // == Offer ==
        public const string? CategoryPermissionsOffer = "Offer";
        public const string Show_Offers = "Offer: Show_Offers";
        public const string TR_Add_Request_from_offer = "Offer: TR_Add_Request_from_offer";
        public const string DS_Add_Offer = "Offer: DS_Add_Offer";
        public const string DS_Show_Offer_Viewers = "Offer: DS_Show_Offer_Viewers";
        //== Title
        public const string Show_OffersTitle = "Show Offers";
        public const string TR_Add_Request_from_offerTitle = "TR Add Request from offer";
        public const string DS_Add_OfferTitle = "DS Add Offer";
        public const string DS_Show_Offer_ViewersTitle = "DS Show Offer Viewers";

        // == Users ==
        public const string? CategoryPermissionsUsers = "Users";
        public const string Show_Users = "Users: Show_Users";
        public const string Add_User = "Users: Add_User";
        public const string Show_User_Details = "Users: Show_User_Details";
        public const string Edit_User_Permission = "Users: Edit_User_Permission";
        public const string Delete_User = "Users: Delete_User";
        //== Title
        public const string Show_UsersTitle = "Show_Users";
        public const string Add_UserTitle = "Add_User";
        public const string Show_User_DetailsTitle = "Show_User_Details";
        public const string Edit_User_PermissionTitle = "Edit_User_Permission";
        public const string Delete_UserTitle = "Users: Delete User";

        // == Documents ==
        public const string? CategoryPermissionsDocuments = "Documents";
        public const string Show_Documents = "Documents: Show_Documents";
        public const string Edit_Documents = "Documents: Edit_Documents";
        //== Title
        public const string Show_DocumentsTitle = "Show Documents";
        public const string Edit_DocumentsTitle = "Edit Documents";

        // == History ==
        public const string? CategoryPermissionsHistory = "History";
        public const string Show_History = "History: Show_History";
        public const string Show_Request_Details_History = "History: Show_Request_Details_History";
        public const string Show_Response_Details_History = "History: Show_Response_Details_History";
        public const string Show_Documents_History = "History: Show_Documents_History";
        //== Title
        public const string Show_HistoryTitle = "Show History";
        public const string Show_Request_Details_HistoryTitle = "Show Request Details History";
        public const string Show_Response_Details_HistoryTitle = "Show Response Details History";
        public const string Show_Documents_HistoryTitle = "Show Documents History";

        // == TravelAgencyCompany ==
        public const string? CategoryPermissionsTravelAgencyCompany = "TravelAgencyCompany";
        public const string ShowTravelAgencyCompanyAccount = "TravelAgencyCompany:ShowTravelAgencyCompanyAccount";
        public const string UpdateTravelAgencyCompanyAccount = "TravelAgencyCompany:UpdateTravelAgencyCompanyAccount";
        //== Title 
        public const string ShowTravelAgencyCompanyAccountTitle = "Show Company Account";
        public const string UpdateTravelAgencyCompanyAccountTitle = "Update Company Account";

        // == DistributorCompany ==
        public const string? CategoryPermissionsDistributorCompany = "DistributorCompany";
        public const string ShowDistributorCompanyAccount = "DistributorCompany:ShowDistributorCompanyAccount";
        public const string UpdateDistributorCompanyAccount = "DistributorCompany:UpdateDistributorCompanyAccount";
        //== Title 
        public const string ShowDistributorCompanyAccountTitle = "Show Company Account";
        public const string UpdateDistributorCompanyAccountTitle = "Update Company Account";


        //public static void DecodeJwtToClass(string token)
        //{
        //    var handler = new JwtSecurityTokenHandler();

        //    // Read the token without validation (or you can validate as per your requirements)
        //    var jwtToken = handler.ReadJwtToken(token);

        //    // Extract the claims
        //    var claims = jwtToken.Claims;

        //    if (claims != null)
        //    {
        //        LstPermissions.Clear();
        //        foreach (var claim in claims )
        //        {
        //            if(claim.Type == "permissions")
        //            {
        //                ObjPermission = JsonConvert.DeserializeObject<PermissionsValues>(claim.Value)!;
        //                LstPermissions.Add(ObjPermission);
        //            }
        //        }
        //    }
        //}

        public static bool CheckPermission(string Name)
        {
            var Result = LstPermissions.Where(x => x.ClaimValue == Name).FirstOrDefault();
            if (Result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }


}
