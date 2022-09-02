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
            var V = Execute(query, sqlParamenter, ventasMapper);
            return V;
        }
        public static bool CreateNewSale(List<Producto> producto, Venta venta)
        {
            bool result = false;
            string queryInsert = "INSERT INTO Venta (Comentarios, IdUsuario) " +
                                    "VALUES (@comentarios, @IdUsuario)";


            SqlParameter comentarios = new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };
            SqlParameter IdUsuario = new SqlParameter("IdUsuario", System.Data.SqlDbType.VarChar) { Value = venta.IdUsuario };

            SqlParameter[] ParametrosVentas = new SqlParameter[] { comentarios, IdUsuario };
            result = DBHandler.InsertUpdate(queryInsert, ParametrosVentas);
            
            if (result == true)
            {
                //Selecciono el último Id
                string query = "SELECT TOP (1) [Id] FROM [Venta] ORDER BY Id Desc";
                var idVentas = DBHandler.GetId(query);
                foreach (var prod in producto)
                {
                    // itero la lista de productos y cargo productos vendidos
                    string queryInsertProductoVendido = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) " +
                                                            "VALUES (@Stock, @IdProducto, @IdVenta)";

                    SqlParameter Stock = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = prod.Stock};
                    SqlParameter IdProducto = new SqlParameter("IdProducto", System.Data.SqlDbType.Int) { Value = prod.Id};
                    SqlParameter IdVenta = new SqlParameter("IdVenta", System.Data.SqlDbType.Int) { Value = idVentas };

                    SqlParameter[] ParametrosProductosVendidos = new SqlParameter[] { Stock, IdProducto, IdVenta };
                    result = DBHandler.InsertUpdate(queryInsertProductoVendido, ParametrosProductosVendidos);
                    
                    //Comienza el update de la Tabla Producto
                    string QueryProductos = "UPDATE PRODUCTO SET Stock = (Stock - @Stock) WHERE ID = @ID";

                    SqlParameter StockaDescontar = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = prod.Stock };
                    SqlParameter IdProductoCorresponde = new SqlParameter("ID", System.Data.SqlDbType.Int) { Value = prod.Id };
                    SqlParameter[] ParametersProductoS = new SqlParameter[] { StockaDescontar, IdProductoCorresponde };

                    result = DBHandler.InsertUpdate(QueryProductos, ParametersProductoS);
                }
            }
                
            return result;
        }

        public static bool EliminarVenta(Venta venta)
        {
            bool result = false;
            //Busco todos los Productos vendidos relacionados con el IdVentas y armo una lista
            ProductoVendidoMapper PVMapper = new ProductoVendidoMapper();
            string querydeleteProductoVendido = "SELECT * FROM ProductoVendido WHERE IdVenta = @IdVenta";
            var sqlParamenter = new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = venta.Id};
            var ListaProductosVendidos = Execute(querydeleteProductoVendido, sqlParamenter, PVMapper);

            if (ListaProductosVendidos!=null)
            {
                //Itero la lista obtenida de Productos vendidos para restablecer el stock de la tabla Producto
                foreach (var productoVendido in ListaProductosVendidos)
                {
                    //Actualizo tabla Productos
                    string QueryProductos = "UPDATE PRODUCTO SET Stock = (Stock + @Stock) WHERE ID = @ID";
                    SqlParameter StockaSumar = new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = productoVendido.Stock };
                    SqlParameter IdProductoCorresponde = new SqlParameter("ID", System.Data.SqlDbType.Int) { Value = productoVendido.IdProducto};
                
                    SqlParameter[] ParametrosProductosVendidos = new SqlParameter[] { StockaSumar, IdProductoCorresponde};
                    result = DBHandler.InsertUpdate(QueryProductos, ParametrosProductosVendidos);
                }

                if (result)
                {
                    //Elimino Producto Vendido
                    string queryDeletePv = "DELETE FROM ProductoVendido WHERE IdVenta = @idVenta";
                    var sqlParamenterPv = new SqlParameter("idVenta", SqlDbType.BigInt) { Value = venta.Id };
                    result = DBHandler.Delete(queryDeletePv, sqlParamenterPv);
                }
                if (result)
                {
                    //Elimino Venta
                    string queryDeleteVenta = "DELETE FROM Venta WHERE Id = @id";
                    var sqlParamenterV = new SqlParameter("id", SqlDbType.BigInt) { Value = venta.Id };
                    result = DBHandler.Delete(queryDeleteVenta, sqlParamenterV);
                }
            }

            return result;
        }
    }
}
