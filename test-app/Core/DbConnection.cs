using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace test_app.Core
{
    /// <summary>
    /// Класс <c>DbConnection</c> устанавливает подключение к базе данных
    /// </summary>
    public static class DbConnection 
    {
        public static SqlConnection connection { get; set; }

        static DbConnection()
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