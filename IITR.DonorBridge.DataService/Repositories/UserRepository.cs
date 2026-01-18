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
    public class UserRepository:IUserRepository
    {
        private readonly DbProvider _dbProvider;
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(DbProvider dbProvider, ILogger<UserRepository> logger)
        {
            _dbProvider = dbProvider;
            _logger = logger;
        }
        public async Task<RegistrationResponse?> GetUserRegistrationByIdAsync(int registrationID)
        {   
            try
            {
                using var conn=_dbProvider.GetConnection();

                _logger.LogInformation("Fetching user registration for RegistrationID {RegistrationID}", registrationID);
                var response = await conn.QueryFirstOrDefaultAsync<RegistrationResponse>(DbStoredProcedure.User_GetUserRegistrationById, new {RegistrationID=registrationID},commandType:System.Data.CommandType.StoredProcedure);
                return response;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user registration for RegistrationID {RegistrationID}", registrationID);
                return null;
            }
        }
        public async Task<LoginResponse?> CreateUserRegistrationAsync(RegistrationRequest request)
        {
            try
            {
                using var conn = _dbProvider.GetConnection();
                _logger.LogInformation("Creating user registration for UserID {UserID}", request.UserId);

                var response = await conn.QueryFirstOrDefaultAsync<LoginResponse>(DbStoredProcedure.User_CreateUserRegistration, request, commandType: System.Data.CommandType.StoredProcedure);
                return response;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user registration for UserID {UserID}", request.UserId);
                throw;
            }
        }
    }
}
