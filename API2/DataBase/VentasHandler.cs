using System.Data;
using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class VentasHandler : DBHandler
    {
        public List<Venta> GetVentas(int IdUsuario)
        {
            var ventasMapper = new VentasMapper();
            var query = "SELECT V.Id, V.Comentarios FROM Venta as V " +
                "inner join ProductoVendido as PV on PV.IdVenta = V.Id " +
                "inner join Producto as P on PV.IdProducto = P.Id " +
                "WHERE P.IdUsuario = @IdUsuario";
            var sqlParamenter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
            var V = Execute(query, sqlParamenter, ventasMapper);
            return V;
        }
        public static bool CreateNewSale(Venta venta)
        {
            bool result = false;
            using (SqlConnection sqlConnection = new SqlConnection(DBHandler.ConnectionString))
            {
                string queryInsert = "INSERT INTO Venta (Comentarios) " +
                    "VALUES (@comentarios)";
                SqlParameter comentarios = new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(comentarios);

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
