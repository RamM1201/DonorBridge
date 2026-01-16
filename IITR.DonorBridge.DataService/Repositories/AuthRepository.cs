using Dapper;
using IITR.DonorBridge.DataService;
using IITR.DonorBridge.DataService.Models;
using IITR.DonorBridge.WebAPI.DataService.Interfaces;
using IITR.DonorBridge.WebAPI.DataService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IITR.DonorBridge.WebAPI.DataService.Repositories
{
    public class AuthRepository:IAuthRepository
    {
        private readonly DbProvider _dbProvider;
        public AuthRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public async Task<LoginResponse?> GetLoginAsync(LoginRequest request)
        {
            using var conn= _dbProvider.GetConnection();

            return await conn.QueryFirstOrDefaultAsync<LoginResponse>(DbStoredProcedure.Auth_GetLogin,request, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
