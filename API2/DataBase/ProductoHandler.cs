using System.Data;
using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class ProductoHandler : DBHandler
    {
        public static List<Producto> GetProductos()
        {
            var productoMapper = new ProductoMapper();
            var query = "SELECT * FROM Producto";
            var P = DBHandler.Execute(query, productoMapper);
            return P;
        }
        public static bool DeleteProduct(int Id)
        {
            //--Prodicto Vendido ---
            var queryDelete = "DELETE ProductoVendido FROM Producto AS P INNER JOIN ProductoVendido AS PV on P.ID = PV.IDPRODUCTO WHERE P.ID = @id";
            var sqlParamenter = new SqlParameter("id", SqlDbType.BigInt) { Value = Id };
            var result = DBHandler.Delete(queryDelete, sqlParamenter);
            if (result)
            {
                //--- Producto ---- 
                var queryDelete2 = "DELETE FROM Producto WHERE Id = @id";
                var sqlParamenter2 = new SqlParameter("id", SqlDbType.BigInt) { Value = Id };
                result = DBHandler.Delete(queryDelete2, sqlParamenter2);
            }
            return result;
        }
        public static bool InsertProducto(Producto producto)
        {
            var queryInsert = "INSERT INTO Producto (Descripciones, Costo, PrecioVenta, Stock, IdUsuario) " +
                              "VALUES (@descripciones, @costo, @precioVenta, @stock, @idUsuario)";
            var descripciones = new SqlParameter("Descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
            var costo = new SqlParameter("Costo", System.Data.SqlDbType.Decimal) { Value = producto.Costo };
            var precioVenta = new SqlParameter("PrecioVenta", System.Data.SqlDbType.Decimal) { Value = producto.PrecioVenta };
            var stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
            var idUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };
            var ProductosParammeters = new SqlParameter[] { descripciones, costo, precioVenta, stock, idUsuario };
            var result = DBHandler.InsertUpdate(queryInsert, ProductosParammeters);
            return result;
        }
        public static bool UpdateProducto(Producto producto)
        {
            var queryUpdate = "UPDATE [SistemaGestion].[dbo].[Producto]" +
                                 "SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario " +
                                 "WHERE Id = @id ";
            var descripciones = new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = producto.Descripciones };
            var costo = new SqlParameter("costo", System.Data.SqlDbType.Decimal) { Value = producto.Costo };
            var precioVenta = new SqlParameter("precioVenta", System.Data.SqlDbType.Decimal) { Value = producto.PrecioVenta };
            var stock = new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = producto.Stock };
            var idUsuario = new SqlParameter("idUsuario", System.Data.SqlDbType.Int) { Value = producto.IdUsuario };
            var id = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = producto.Id };
            var ProductosParammeters = new SqlParameter[] { descripciones, costo, precioVenta, stock, idUsuario,id };
            var P = DBHandler.InsertUpdate(queryUpdate, ProductosParammeters);
            return P;
        }
    }
}
