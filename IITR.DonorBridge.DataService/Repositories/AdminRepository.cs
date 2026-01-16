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
    public class AdminRepository:IAdminRepository
    {
        private readonly DbProvider _dbProvider;
        public AdminRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public async Task<IEnumerable<RegistrationResponse>> GetAllRegistrationsAsync()
        {
            using var conn=_dbProvider.GetConnection();
            return await conn.QueryAsync<RegistrationResponse>(DbStoredProcedure.Admin_GetAllUsers,commandType:System.Data.CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<AdminDonationResponse>> GetAllDonationsAsync()
        {
            using var conn=_dbProvider.GetConnection();
            return await conn.QueryAsync<AdminDonationResponse>(DbStoredProcedure.Admin_GetAllDonations,commandType:System.Data.CommandType.StoredProcedure);
        }
    }
}
