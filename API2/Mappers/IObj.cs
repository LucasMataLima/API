using System.Data.SqlClient;

namespace API2.Mappers
{
    public interface IObj <T>
    {
        T Select(SqlDataReader dataReader);
    }
}
