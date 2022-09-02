using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class DBHandler
    {
        public const string ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=SistemaGestion; Integrated Security=True;";
        public static List<T> Execute<T>(string query, IMapper<T> Obj, SqlParameter[] sqlParameter = null)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    if (sqlParameter != null)
                    {
                        sqlCommand.Parameters.AddRange(sqlParameter);
                    }
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
                    try
                    {
                        int numberOfRows = sqlCommand.ExecuteNonQuery();
                        if (numberOfRows > 0)
                        {
                            result = true;
                        }
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
        public static bool InsertUpdate(string queryInsert, SqlParameter[] sqlParameter)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(sqlParameter);
                    try
                    {
                        int numberOfRows = sqlCommand.ExecuteNonQuery();
                        if (numberOfRows > 0)
                        {
                            result = true;
                        }
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
                sqlConnection.Close();
            }
            return result;
        }
        public static int GetId(string query)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var Id = 0;
                    sqlConnection.Open();   
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Id = Convert.ToInt32(dataReader["ID"]);
                            }
                        }
                    }
                    sqlConnection.Close();
                    return Id;
                }
            }               
        }
        public static void Select<T>(string query, SqlParameter[] sqlParameter, IObj<T> Obj, ref T result)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(sqlParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                result = Obj.CargarObj(dataReader);
                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
        }
    }
}
