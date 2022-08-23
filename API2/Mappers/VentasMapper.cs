using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace API2.Mappers
{
    public class VentasMapper : IMapper<Venta>
    {
        public List<Venta>Mapper(SqlDataReader dataReader)
        {
            var ventas = new List<Venta>();
            var venta = new Venta();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    venta.Id = Convert.ToInt32(dataReader["Id"]);
                    venta.Comentarios = dataReader["Comentarios"].ToString();
                    ventas.Add(venta);
                }
            }
            return ventas;
        }
    }
}
