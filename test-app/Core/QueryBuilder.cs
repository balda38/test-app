using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace test_app.Core
{
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
            DbConnection dbConnection = new DbConnection();
            sqlConnection = dbConnection.connection;
        }

        public void SetCommandType(string commandType)
        {
            this.commandType = commandTypes[commandType]; 
        }

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