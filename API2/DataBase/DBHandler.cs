using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class DBHandler
    {
        public const string ConnectionString = "Data Source=NKO\\SQLEXPRESS;Initial Catalog=SistemaGestion; Integrated Security=True;";

        public static List<T> Execute<T>(string query, SqlParameter sqlParameter, IMapper<T> Obj)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(sqlParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        var Exe = Obj.Mapper(dataReader);
                        sqlConnection.Close();
                        return Exe;
                    }
                }

            }
        }

        public static bool Delete(string queryDelete, SqlParameter sqlParameter)
        {
            var result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter);
                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }

        public static bool Insert(string queryInsert, SqlParameter sqlParameter1,
                                    SqlParameter sqlParameter2, SqlParameter sqlParameter3,
                                     SqlParameter sqlParameter4, SqlParameter sqlParameter5)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter1);
                    sqlCommand.Parameters.Add(sqlParameter2);
                    sqlCommand.Parameters.Add(sqlParameter3);
                    sqlCommand.Parameters.Add(sqlParameter4);
                    sqlCommand.Parameters.Add(sqlParameter5);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }

        public static bool Update(string queryUpdate, SqlParameter sqlParameter1,SqlParameter sqlParameter2, 
                                  SqlParameter sqlParameter3,SqlParameter sqlParameter4, SqlParameter sqlParameter5, 
                                  SqlParameter sqlParameter6)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(sqlParameter1);
                    sqlCommand.Parameters.Add(sqlParameter2);
                    sqlCommand.Parameters.Add(sqlParameter3);
                    sqlCommand.Parameters.Add(sqlParameter4);
                    sqlCommand.Parameters.Add(sqlParameter5);
                    sqlCommand.Parameters.Add(sqlParameter6);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        result = true;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
    }
}
