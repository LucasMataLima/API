using System.Data;
using API2.Mappers;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class VentasHandler : DBHandler
    {
        public static List<Venta> GetVentas(int IdUsuario)
        {
            var ventasMapper = new VentasMapper();
            var query = "SELECT V.Id, V.Comentarios FROM Venta as V " +
                        "inner join ProductoVendido as PV on PV.IdVenta = V.Id " +
                        "inner join Producto as P on PV.IdProducto = P.Id " +
                        "WHERE P.IdUsuario = @IdUsuario";
            var sqlParamenter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
            var ventasSqlParammeter = new SqlParameter[] { sqlParamenter };
            var ListaVentas = Execute(query, ventasMapper, ventasSqlParammeter);
            return ListaVentas;
        }
        public static bool CreateNewSale(List<Producto> producto, Venta venta)
        {
            var queryInsert = "INSERT INTO Venta (Comentarios, IdUsuario) " +
                              "VALUES (@comentarios, @IdUsuario)";
            var comentarios = new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };
            var IdUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.VarChar) { Value = venta.IdUsuario };
            var ParametrosVentas = new SqlParameter[] { comentarios, IdUsuario };
            var result = DBHandler.InsertUpdate(queryInsert, ParametrosVentas);
            if (result == true)
            {
                //Selecciono el último Id
                var query = "SELECT TOP (1) [Id] FROM [Venta] ORDER BY Id Desc";
                var idVentas = DBHandler.GetId(query);
                foreach (var prod in producto)
                {
                    // itero la lista de productos y cargo productos vendidos
                    var queryInsertProductoVendido = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) " +
                                                        "VALUES (@Stock, @IdProducto, @IdVenta)";
                    var Stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = prod.Stock};
                    var IdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.Int) { Value = prod.Id};
                    var IdVenta = new SqlParameter("IdVenta", System.Data.SqlDbType.Int) { Value = idVentas };
                    var ParametrosProductosVendidos = new SqlParameter[] { Stock, IdProducto, IdVenta };
                    result = DBHandler.InsertUpdate(queryInsertProductoVendido, ParametrosProductosVendidos);
                    //Comienza el update de la Tabla Producto
                    var QueryProductos = "UPDATE PRODUCTO SET Stock = (Stock - @Stock) WHERE ID = @ID";
                    var StockaDescontar = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = prod.Stock };
                    var IdProductoCorresponde = new SqlParameter("ID", System.Data.SqlDbType.Int) { Value = prod.Id };
                    var ParametersProductoS = new SqlParameter[] { StockaDescontar, IdProductoCorresponde };
                    result = DBHandler.InsertUpdate(QueryProductos, ParametersProductoS);
                }
            }
            return result;
        }
        public static bool EliminarVenta(Venta venta)
        {
            var result = false;
            //Busco todos los Productos vendidos relacionados con el IdVentas y armo una lista
            var PVMapper = new ProductoVendidoMapper();
            var querydeleteProductoVendido = "SELECT * FROM ProductoVendido WHERE IdVenta = @IdVenta";
            var sqlParamenter = new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = venta.Id};
            var ventaSqlParameter = new SqlParameter[] { sqlParamenter };
            var ListaProductosVendidos = Execute(querydeleteProductoVendido, PVMapper, ventaSqlParameter);
            if (ListaProductosVendidos.Any())
            {
                //Itero la lista obtenida de Productos vendidos para restablecer el stock de la tabla Producto
                foreach (var productoVendido in ListaProductosVendidos)
                {
                    //Actualizo tabla Productos
                    var QueryProductos = "UPDATE PRODUCTO SET Stock = (Stock + @Stock) WHERE ID = @ID";
                    var StockaSumar = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                    var IdProductoCorresponde = new SqlParameter("ID", System.Data.SqlDbType.Int) { Value = productoVendido.IdProducto};
                    var ParametrosProductosVendidos = new SqlParameter[] { StockaSumar, IdProductoCorresponde};
                    result = DBHandler.InsertUpdate(QueryProductos, ParametrosProductosVendidos);
                }
                if (result)
                {
                    //Elimino Producto Vendido
                    var queryDeletePv = "DELETE FROM ProductoVendido WHERE IdVenta = @idVenta";
                    var sqlParamenterPv = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = venta.Id };
                    result = DBHandler.Delete(queryDeletePv, sqlParamenterPv);
                }
                if (result)
                {
                    //Elimino Venta
                    var queryDeleteVenta = "DELETE FROM Venta WHERE Id = @id";
                    var sqlParamenterV = new SqlParameter("id", SqlDbType.BigInt) { Value = venta.Id };
                    result = DBHandler.Delete(queryDeleteVenta, sqlParamenterV);
                }
            }
            return result;
        }
    }
}
