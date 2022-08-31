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

        public static bool InsertUpdate(string queryInsert, SqlParameter[] sqlParameter)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    //foreach (var item in sqlParameter)
                    //{
                    sqlCommand.Parameters.AddRange(sqlParameter);

                    //}

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

        //public static T Select <T>(string query, SqlParameter sqlParameter, IObj<T> Obj)
        //{
        //    var Reader = Obj;
        //    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
        //    {
        //        sqlConnection.Open();
        //        using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
        //        {
        //            sqlCommand.Parameters.Add(sqlParameter);
        //            using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
        //            {
        //                if (dataReader.HasRows)
        //                {
        //                    while (dataReader.Read())
        //                    {
        //                        var Reader = Obj.Select(dataReader);
        //                    }
        //                }
        //                sqlConnection.Close();
        //            }
        //        }
        //    }
        //    return Reader;
        //}
    }
}
