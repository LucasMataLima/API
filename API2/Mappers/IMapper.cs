using System.Data.SqlClient;


namespace API2.Mappers
{
    public interface IMapper<T>
    {
        List<T>Mapper (SqlDataReader dataReader);

    }
}
