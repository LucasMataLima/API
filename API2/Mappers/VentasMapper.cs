using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace API2.Mappers
{
    public class VentasMapper : IMapper<Venta> 
    {
        public List<Venta>Mapper(SqlDataReader dataReader)
        {
            var ventas = new List<Venta>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    ventas.Add(CargarVenta(dataReader));
                }
            }
            return ventas;
        }
        public Venta CargarVenta(SqlDataReader dataReader)
        {
            var venta = new Venta();
            venta.Id = Convert.ToInt32(dataReader["Id"]);
            venta.Comentarios = dataReader["Comentarios"].ToString();
            return venta;
        }
    }
}
