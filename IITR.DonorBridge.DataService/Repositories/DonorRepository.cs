using Dapper;
using IITR.DonorBridge.DataService;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;

namespace IITR.DonorBridge.WebAPI.DataService.Repositories
{
    public class DonorRepository:IDonorRepository
    {
        private readonly DbProvider _dbProvider;
        private readonly ILogger<DonorRepository> _logger;
        public DonorRepository(DbProvider dbProvider, ILogger<DonorRepository> logger)
        {
            _dbProvider = dbProvider;
            _logger = logger;
        }

        private IDbConnection Connection => _dbProvider.GetConnection();
        public async Task<IEnumerable<DonorDonationResponse>> GetAllDonationsAsync(int donorId)
        {   
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Fetching all donations for donorId {DonorId}", donorId);
                await conn.QueryAsync(DbStoredProcedure.FailPendingRecords, commandType: System.Data.CommandType.StoredProcedure);

                return await conn.QueryAsync<DonorDonationResponse>(DbStoredProcedure.Donor_GetAllDonations, new { UserRegistrationID = donorId }, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching donations for donorId {DonorId}", donorId
                    );

                throw;
            }
            
        }
        public async Task<IEnumerable<DonorTransactionResponse>> GetTransactionsByDonationIdAsync(int donationId)
        {   
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Fetching transactions for donationId {DonationId}", donationId);
                await conn.QueryAsync(DbStoredProcedure.FailPendingRecords, commandType: System.Data.CommandType.StoredProcedure);

                return await conn.QueryAsync<DonorTransactionResponse>(DbStoredProcedure.Donor_GetTransactionsByDonationId, new { DonationID = donationId }, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching transactions for donationId {DonationId}", donationId);

                throw;
            }
        }
        public async Task<int> CreateDonationAsync(DonorDonationRequest donationRequest)
        {   
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Creating donation for userRegistrationId {UserRegistrationId}", donationRequest.UserRegistrationId);
                return await conn.ExecuteScalarAsync<int>(DbStoredProcedure.Donor_CreateDonation, donationRequest, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {   
                _logger.LogError(ex ,"Error while creating donation for userRegistrationId {UserRegistrationId}", donationRequest.UserRegistrationId);

                throw;
            }
        }
        public async Task<int> CreateTransactionAsync(DonorTransactionRequest request)
        {
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Creating transaction for donationId {DonationId}", request.DonationId);

                return await conn.ExecuteScalarAsync<int>(DbStoredProcedure.Donor_CreateTransaction, request, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating transaction for donationId {DonationId}", request.DonationId);

                throw;
            }
        }
        public async Task<DonorTransactionResponse> UpdateDonationStatus(TransactionStatusUpdateRequest request)
        {
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Updating donation status with request {@Request}", request);
                return await conn.QuerySingleAsync<DonorTransactionResponse>(DbStoredProcedure.Donor_UpdateDonationStatus, request, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating donation status");

                throw;
            }
        }
        public async Task<int> GetAmountForDonation(int donationId)
        {   
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Fetching amount for donationId {donationId}", donationId);
                return await conn.ExecuteScalarAsync<int>(DbStoredProcedure.Donor_GetAmountForDonation, new { DonationID = donationId }, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching amount for donationId {DonationId}", donationId);

                throw;
            }
        }
    }
}
