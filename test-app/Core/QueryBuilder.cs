using System.Collections.Generic;
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
        private Dictionary<string, CommandType> commandTypes = new Dictionary<string, CommandType>
        {
            { "procedure", CommandType.StoredProcedure },
            { "table", CommandType.TableDirect },
            { "text", CommandType.Text }
        };
        private CommandType commandType;

        public QueryBuilder()
        {
            sqlConnection = DbConnection.connection;
        }

        /// <summary>
        /// Метод <c>SetCommandType</c> устанавливает тип комманды для SQL-запроса
        /// </summary>
        /// <param name="commandType">Тип команды строкой</param>
        /// <returns>void</returns>
        public void SetCommandType(string commandType)
        {
            this.commandType = commandTypes[commandType]; 
        }

        /// <summary>
        /// Метод <c>Execute</c> выполняет SQL-запрос к базе данных
        /// </summary>
        /// <param name="query">Строка, содержащая запрос к базе данных</param>
        /// <returns>List<string> - записи из таблиццы базы данных</returns>
        /// <example>Выполнить SQL-запрос к базе данных можно следующим образом:
        /// <code>
        ///     QueryBuilder queryBuilder = new QueryBuilder();
        ///     queryBuilder.SetCommandType("text");
        ///     List<string> result = queryBuilder.Execute("SELECT * FROM some_table");
        /// </code>
        /// </example>
        public List<string> Execute(string query)
        {
            try
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                List<string> result = new List<string>();
                int count = 0;
                while (sqlReader.Read())
                {
                    result.Add(sqlReader.GetString(count));
                    count++;
                }

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