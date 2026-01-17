using Dapper;
using IITR.DonorBridge.DataService;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Repositories
{
    public class DonorRepository:IDonorRepository
    {
        private readonly DbProvider _dbProvider;
        public DonorRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public async Task<IEnumerable<DonorDonationResponse>> GetAllDonationsAsync(int donorId)
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.QueryAsync<DonorDonationResponse>(DbStoredProcedure.Donor_GetAllDonations, new { UserRegistrationID = donorId }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<DonorTransactionResponse>> GetTransactionsByDonationIdAsync(int donationId)
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.QueryAsync<DonorTransactionResponse>(DbStoredProcedure.Donor_GetTransactionsByDonationId, new { DonationID = donationId }, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<int> CreateDonationAsync(DonorDonationRequest donationRequest)
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.ExecuteScalarAsync<int>(DbStoredProcedure.Donor_CreateDonation,donationRequest,commandType:System.Data.CommandType.StoredProcedure);
        }
        public async Task<int> CreateTransactionAsync(DonorTransactionRequest request)
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.ExecuteScalarAsync<int>(DbStoredProcedure.Donor_CreateTransaction,request, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<DonorTransactionResponse> UpdateDonationStatus(TransactionStatusUpdateRequest request)
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.QuerySingleAsync<DonorTransactionResponse>(DbStoredProcedure.Donor_UpdateDonationStatus, request, commandType: System.Data.CommandType.StoredProcedure);
        }
        public async Task<int> GetAmountForDonation(int donationId)
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.ExecuteScalarAsync<int>(DbStoredProcedure.Donor_GetAmountForDonation, new { DonationID = donationId },commandType:System.Data.CommandType.StoredProcedure);
        }
    }
}
