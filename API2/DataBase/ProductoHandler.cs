using System.Data;
using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class ProductoHandler : DBHandler
    {
        public static List<Producto> GetProductos()
        {
            ProductoMapper productoMapper = new ProductoMapper();
            List<Producto> ListaProductos = new List<Producto>();
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM Producto";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        ListaProductos = productoMapper.Mapper(dataReader);
                    }
                    sqlConnection.Close();
                }
            }
            return ListaProductos;
        }
        //public List<Producto> GetProductos(int IdUsuario = 0)
        //{
        //    var productoMapper = new ProductoMapper();   
        //    var query = "SELECT * FROM Producto WHERE IdUsuario = @IdUsuario";
        //    var sqlParamenter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
        //    var P = DBHandler.Execute(query, sqlParamenter, productoMapper);
        //    return P;
        //}
        public static bool DeleteProduct(int Id)
        {
            bool result = false;

            //--Prodicto Vendido ---
            string queryDelete = "DELETE ProductoVendido FROM Producto AS P INNER JOIN ProductoVendido AS PV on P.ID = PV.IDPRODUCTO WHERE P.ID = @id";
            var sqlParamenter = new SqlParameter("id", SqlDbType.BigInt) { Value = Id };
            result = DBHandler.Delete(queryDelete, sqlParamenter);

            //--- Producto ---- 
            string queryDelete2 = "DELETE FROM Producto WHERE Id = @id";
            var sqlParamenter2 = new SqlParameter("id", SqlDbType.BigInt) { Value = Id };
            result = DBHandler.Delete(queryDelete2, sqlParamenter2);

            return result;
        }
        public static bool InsertProducto(Producto producto)
        {
            string queryInsert = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                    "VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)";

            List<SqlParameter> productos = new List<SqlParameter>();
            var descripciones = new SqlParameter("Descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
            productos.Add(descripciones);
            
            var costo = new SqlParameter("Costo", System.Data.SqlDbType.Decimal) { Value = producto.Costo };
            productos.Add(costo);
            
            var precioVenta = new SqlParameter("PrecioVenta", System.Data.SqlDbType.Decimal) { Value = producto.PrecioVenta };
            productos.Add(precioVenta);
            
            var stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
            productos.Add(stock);
            
            var idUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };
            productos.Add(idUsuario);

            var result = DBHandler.InsertUpdate(queryInsert, productos);

            return result;
        }
        public static bool UpdateProducto(Producto producto)
        {
            string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto]" +
                                 "SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario " +
                                 "WHERE Id = @id ";

            List<SqlParameter> productos = new List<SqlParameter>();
            var descripciones = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
            var costo = new SqlParameter("costo", System.Data.SqlDbType.Decimal) { Value = producto.Costo };
            var precioVenta = new SqlParameter("precioVenta", System.Data.SqlDbType.Decimal) { Value = producto.PrecioVenta };
            var stock = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
            var idUsuario = new SqlParameter("idUsuario", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };
            var id = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = producto.Id };
            productos.Add(descripciones);
            productos.Add(costo);
            productos.Add(precioVenta);
            productos.Add(stock);
            productos.Add(idUsuario);
            productos.Add(id);
            var P = DBHandler.InsertUpdate(queryUpdate, productos);
            return P;
        }
    }
}
