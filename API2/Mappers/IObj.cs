using System.Data.SqlClient;

namespace API2.Mappers
{
    public interface IObj <T>
    {
        T CargarObj(SqlDataReader dataReader);
    }
}
