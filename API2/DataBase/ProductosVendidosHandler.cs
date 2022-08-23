using API2.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class ProductosVendidosHandler : DBHandler
    {
        public List<ProductoVendido> GetProductoVendido(int IdUsuario)
        {
            ProductoVendidoMapper productoVendidoMapper = new ProductoVendidoMapper();
            var query = "SELECT * FROM Producto AS P " +
                        "INNER JOIN ProductoVendido AS PV on P.Id = PV.IdProducto " +
                        "WHERE P.IdUsuario = @IdUsuario";
            var sqlParamenter = new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = IdUsuario };
            var P = Execute(query, sqlParamenter, productoVendidoMapper);
            return P;

        }
    }
}
