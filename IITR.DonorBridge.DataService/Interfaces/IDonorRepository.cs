using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Interfaces
{
    public interface IDonorRepository
    {
        Task<IEnumerable<DonorDonationResponse>> GetAllDonationsAsync(int donorId);
        Task<IEnumerable<DonorTransactionResponse>> GetTransactionsByDonationIdAsync(int donationId);
        Task<int> CreateDonationAsync(DonorDonationRequest donationRequest);
        Task<int> CreateTransactionAsync(int donationId);
    }
}
