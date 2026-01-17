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
    public class UserRepository:IUserRepository
    {
        private readonly DbProvider _dbProvider;
        public UserRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public async Task<RegistrationResponse?> GetUserRegistrationByIdAsync(int registrationID)
        {
            using var conn=_dbProvider.GetConnection();
            var response = await conn.QueryFirstOrDefaultAsync<RegistrationResponse>(DbStoredProcedure.User_GetUserRegistrationById, new {RegistrationID=registrationID},commandType:System.Data.CommandType.StoredProcedure);
            return response;
        }
        public async Task<LoginResponse?> CreateUserRegistrationAsync(RegistrationRequest request)
        {
            try
            {
                using var conn = _dbProvider.GetConnection();
                var response = await conn.QueryFirstOrDefaultAsync<LoginResponse>(DbStoredProcedure.User_CreateUserRegistration, request, commandType: System.Data.CommandType.StoredProcedure);
                return response;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
            }
    }
}
