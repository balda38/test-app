using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace test_app.Core
{
    /// <summary>
    /// Класс <c>QueryBuilder</c> предназначен для выполнения SQL-запросов к базе данных.
    /// </summary>
    public class QueryBuilder
    {
        private SqlConnection sqlConnection;

        public QueryBuilder()
        {
            sqlConnection = DbConnection.connection;
        }

        /// <summary>
        /// Метод <c>Execute</c> выполняет SQL-запрос к базе данных
        /// </summary>
        /// <param name="query">Строка, содержащая запрос к базе данных</param>
        /// <returns>List<string> - записи из таблиццы базы данных</returns>
        /// <example>Выполнить SQL-запрос к базе данных можно следующим образом:
        /// <code>
        ///     QueryBuilder queryBuilder = new QueryBuilder();
        ///     List<string> result = queryBuilder.Execute("SELECT * FROM some_table");
        /// </code>
        /// </example>
        public DataTable Execute(string query)
        {
            try
            {
                sqlConnection.Open();

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, sqlConnection);

                DataTable result = new DataTable();
                sqlAdapter.Fill(result);

                return result;
            }
            catch (SqlException e)
            {
                throw new HttpException("Не удалось выполнить SQL-запрос", e);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}