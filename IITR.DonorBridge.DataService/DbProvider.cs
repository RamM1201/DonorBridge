using Microsoft.Data.SqlClient;

namespace IITR.DonorBridge.DataService
{
    public class DbProvider
    {
        private readonly string _connectionString;
        public DbProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
