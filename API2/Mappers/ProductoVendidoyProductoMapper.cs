using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace API2.Mappers
{
    public class ProductoVendidoyProductoMapper : IMapper<ProductoVendidoyProducto> , IObj<ProductoVendidoyProducto>
    {
        public List<ProductoVendidoyProducto> Mapper(SqlDataReader dataReader)
        {
            var productosVendidos = new List<ProductoVendidoyProducto>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    productosVendidos.Add(CargarObj(dataReader));
                }
            }
            return productosVendidos;
        }
        public ProductoVendidoyProducto CargarObj(SqlDataReader dataReader)
        {
            var ProductoVendidoyProductos = new ProductoVendidoyProducto();
            ProductoVendidoyProductos.Producto.Id = Convert.ToInt32(dataReader["PID"]);
            ProductoVendidoyProductos.Producto.Descripciones = dataReader["Descripciones"].ToString();
            ProductoVendidoyProductos.Producto.Costo = Convert.ToDouble(dataReader["Costo"]);
            ProductoVendidoyProductos.Producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
            ProductoVendidoyProductos.Producto.Stock = Convert.ToInt32(dataReader["StockProductos"]);
            ProductoVendidoyProductos.Producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
            ProductoVendidoyProductos.ProductoVendido.Id = Convert.ToInt32(dataReader["ID"]);
            ProductoVendidoyProductos.ProductoVendido.Stock = Convert.ToInt32(dataReader["Stock"]);
            ProductoVendidoyProductos.ProductoVendido.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
            ProductoVendidoyProductos.ProductoVendido.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
            return ProductoVendidoyProductos;
        }
    }
}
