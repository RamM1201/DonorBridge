using Dapper;
using IITR.DonorBridge.DataService;
using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace IITR.DonorBridge.WebAPI.DataService.Repositories
{
    public class AdminRepository:IAdminRepository
    {
        private readonly DbProvider _dbProvider;
        private readonly ILogger<AdminRepository> _logger;
        public AdminRepository(DbProvider dbProvider, ILogger<AdminRepository> logger)
        {
            _dbProvider = dbProvider;
            _logger = logger;
        }
        public async Task<IEnumerable<RegistrationResponse>> GetAllRegistrationsAsync()
        {   
            try
            {
                using var conn=_dbProvider.GetConnection();

                _logger.LogInformation("Fetching all user registrations (admin)");
                return await conn.QueryAsync<RegistrationResponse>(DbStoredProcedure.Admin_GetAllUsers, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all user registrations (admin)");
                throw;
            }
        }
        public async Task<IEnumerable<AdminDonationResponse>> GetAllDonationsAsync()
        {
            try
            {
                using var conn=_dbProvider.GetConnection();

                _logger.LogInformation("Fetching all donations (admin)");
                await conn.QueryAsync(DbStoredProcedure.FailPendingRecords,commandType:System.Data.CommandType.StoredProcedure);
                return await conn.QueryAsync<AdminDonationResponse>(DbStoredProcedure.Admin_GetAllDonations, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all donations (admin)");
                throw;
            }
        }
        public async Task<IEnumerable<AdminTransactionResponse>> GetAllTransactionsAsync()
        {
            try
            {
                using var conn=_dbProvider.GetConnection();

                _logger.LogInformation("Fetching all transactions (admin)");
                await conn.QueryAsync(DbStoredProcedure.FailPendingRecords, commandType: System.Data.CommandType.StoredProcedure);

                return await conn.QueryAsync<AdminTransactionResponse>(DbStoredProcedure.Admin_GetAllTransactions, commandType: System.Data.CommandType.StoredProcedure);
            }

            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all transactions (admin)");
                throw;
            }
        }
    }
}
