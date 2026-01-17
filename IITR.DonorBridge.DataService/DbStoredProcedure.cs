using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService
{
    public static class DbStoredProcedure
    {
        public static string Auth_GetLogin= "sp_GetLogin";
        public static string User_GetUserRegistrationById = "sp_userGetRegistrationById";
        public static string User_CreateUserRegistration = "sp_userCreateRegistration";
        public static string Admin_GetAllUsers = "sp_adminGetAllUsers";
        public static string Admin_GetAllDonations = "sp_adminGetAllDonations";
        public static string Admin_GetAllTransactions = "sp_adminGetAllTransactions";
        public static string Donor_GetAllDonations= "sp_donorGetAllDonations";
        public static string Donor_GetTransactionsByDonationId= "sp_donorGetTransactionsByDonationId";
        public static string Donor_CreateTransaction = "sp_donorCreateTransaction";
        public static string Donor_CreateDonation= "sp_donorCreateDonation";
        public static string Donor_UpdateDonationStatus= "sp_donorUpdateDonationStatus";
        public static string Donor_GetAmountForDonation= "sp_donorGetAmountForDonationId";
    }
}
