using System.Data.SqlClient;

namespace API2.Mappers
{
    public class UsuarioMapper :IMapper<Usuario>
    {
        public List<Usuario> Mapper(SqlDataReader dataReader)
        {
            var usuarios = new List<Usuario>();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    usuarios.Add(CargarUsuario(dataReader));
                }
            }
            return usuarios;
        }

        public Usuario CargarUsuario(SqlDataReader dataReader)
        {
            var usuario = new Usuario();
            usuario.Id = Convert.ToInt32(dataReader["ID"]);
            usuario.Nombre = dataReader["Nombre"].ToString();
            usuario.Apellido = dataReader["Apellido"].ToString();
            usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
            usuario.Contraseña = dataReader["Contraseña"].ToString();
            usuario.Mail = dataReader["Mail"].ToString();
            return usuario;
        }
    }
}
