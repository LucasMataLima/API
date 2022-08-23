using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace API2.Mappers
{
    public class ProductoVendidoMapper : IMapper<ProductoVendido>
    {
        public List<ProductoVendido> Mapper(SqlDataReader dataReader)
        {
            List<ProductoVendido> productosVendidos = new List<ProductoVendido>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    var productoVendido = new ProductoVendido();
                    productoVendido.Id = Convert.ToInt32(dataReader["Id"]);
                    productoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
                    productoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
                    productoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);;
                    productosVendidos.Add(productoVendido);
                }
            }
            return productosVendidos;
        }
    }
}
