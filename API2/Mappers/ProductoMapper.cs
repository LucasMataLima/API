using System.Data.SqlClient;

namespace API2.Mappers
{
    public class ProductoMapper : IMapper<Producto> , IObj<Producto>
    {
        public List<Producto> Mapper(SqlDataReader dataReader) 
        {
            var productos = new List<Producto>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    productos.Add(CargarObj(dataReader));
                }
            }
            return productos;
        }
        public Producto CargarObj (SqlDataReader dataReader)
        {
            var producto = new Producto();
            producto.Id = Convert.ToInt32(dataReader["ID"]);
            producto.Descripciones = dataReader["Descripciones"].ToString();
            producto.Costo = Convert.ToDouble(dataReader["Costo"]);
            producto.PrecioVenta = Convert.ToDouble(dataReader["PrecioVenta"]);
            producto.Stock = Convert.ToInt32(dataReader["Stock"]);
            producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
            return producto;
        }
    }
}
