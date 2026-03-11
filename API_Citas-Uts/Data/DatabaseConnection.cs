using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
namespace API_Citas_Uts.Data
{
    public class DatabaseConnection
    {
        private readonly string _connectionString;
        public DatabaseConnection(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
