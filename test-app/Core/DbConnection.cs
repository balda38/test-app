using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace test_app.Core
{
    public class DbConnection 
    {
        public SqlConnection connection;

        public DbConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MsSQLConnection"].ConnectionString;
                connection = new SqlConnection(connectionString);
            }
            catch (SqlException e)
            {
                throw new HttpException("Не удалось подключиться к базе данных", e);
            }
        }
    }
}