using Microsoft.Data.SqlClient;

namespace SysMetricsAPI.Connection
{
    public class SqlConnectionFactory
    {
        private readonly IConfiguration _configuration;
        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public SqlConnection CreateConnection()
        //    => new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));


        public SqlConnection CreateConnection()
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }
    }
}
