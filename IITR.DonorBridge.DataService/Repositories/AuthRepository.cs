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
    public class AuthRepository:IAuthRepository
    {
        private readonly DbProvider _dbProvider;
        private readonly ILogger<AuthRepository> _logger;
        public AuthRepository(DbProvider dbProvider, ILogger<AuthRepository> logger)
        {
            _dbProvider = dbProvider;
            _logger = logger;
        }
        public async Task<LoginResponse?> GetLoginAsync(LoginRequest request)
        {
            try
            {
                using var conn = _dbProvider.GetConnection();

                _logger.LogInformation("Attempting login for username {Username}", request.UserID);
                return await conn.QueryFirstOrDefaultAsync<LoginResponse>(DbStoredProcedure.Auth_GetLogin, request, commandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during login attempt for username {Username}", request.UserID);
                throw;
            }
        }
    }
}
