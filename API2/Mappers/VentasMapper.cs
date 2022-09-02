using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace API2.Mappers
{
    public class VentasMapper : IMapper<Venta> , IObj<Venta>
    {
        public List<Venta>Mapper(SqlDataReader dataReader)
        {
            var ventas = new List<Venta>();

            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    ventas.Add(CargarObj(dataReader));
                }
            }
            return ventas;
        }
        public Venta CargarObj(SqlDataReader dataReader)
        {
            var venta = new Venta();
            venta.Id = Convert.ToInt32(dataReader["Id"]);
            venta.Comentarios = dataReader["Comentarios"].ToString();
            return venta;
        }
    }
}
