using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace EmployeeControl.repository
{

    public class DataLayer
    {
        internal static IDbConnection GetConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("arintelDb");
            return new MySqlConnection(connectionString);
        }
    }
}