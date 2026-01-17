using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<RegistrationResponse>> GetAllRegistrationsAsync();
        Task<IEnumerable<AdminDonationResponse>> GetAllDonationsAsync();
        Task<IEnumerable<AdminTransactionResponse>> GetAllTransactionsAsync();
    }
}
