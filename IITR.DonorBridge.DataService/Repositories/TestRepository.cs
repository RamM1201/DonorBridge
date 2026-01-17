using Dapper;
using IITR.DonorBridge.DataService.Interfaces;
using IITR.DonorBridge.DataService.Models;

namespace IITR.DonorBridge.DataService.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly DbProvider _dbProvider;
        public TestRepository(DbProvider dbProvider)
        {
            _dbProvider = dbProvider;
        }
        public async Task<TestModel?> GetAllUsersAsync()
        {
            using var conn = _dbProvider.GetConnection();
            return await conn.QueryFirstOrDefaultAsync<TestModel>("select top 1 id,userID,password from tbl_login");
        }
    }
}
