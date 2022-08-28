using API2.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class ProductosVendidosHandler : DBHandler
    {
        public static List<ProductoVendidoyProducto> GetProductoVendido(int IdUsuario)
        {
            List<ProductoVendidoyProducto> ListaProductoVendidoyProductos = new List<ProductoVendidoyProducto>();


            var query = "SELECT * FROM Producto AS P " +
                        "INNER JOIN ProductoVendido AS PV on P.Id = PV.IdProducto " +
                        "WHERE P.IdUsuario = @IdUsuario";

            var sqlParameter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    sqlCommand.Parameters.Add(sqlParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendidoyProducto ProductoVendidoyProductos = new ProductoVendidoyProducto();
                                ProductoVendidoyProductos.Producto.Id = Convert.ToInt32(dataReader["Id"]);
                                ProductoVendidoyProductos.Producto.Descripciones = dataReader["Descripciones"].ToString();
                                ProductoVendidoyProductos.Producto.Costo = Convert.ToDouble(dataReader["Costo"]);
                                ProductoVendidoyProductos.Producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
                                ProductoVendidoyProductos.Producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                ProductoVendidoyProductos.Producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                ProductoVendidoyProductos.ProductoVendido.Id = Convert.ToInt32(dataReader["ID"]);
                                ProductoVendidoyProductos.ProductoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                                ProductoVendidoyProductos.ProductoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                ProductoVendidoyProductos.ProductoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                                ListaProductoVendidoyProductos.Add(ProductoVendidoyProductos);
                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
            return ListaProductoVendidoyProductos;

        }
    }
}
