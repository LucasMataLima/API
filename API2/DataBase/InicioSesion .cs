using API2.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class InicioSesion : DBHandler
    {
        public static Usuario Login(string NombreUsuario, string Contraseña)
        {
            var usuarioMapper = new UsuarioMapper();
            var usuario = new Usuario();
            var query = @"SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario and Contraseña = @Contraseña";
            var contraseña = new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = Contraseña };
            var nombreUsuario = new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = NombreUsuario };
            var loginParammeters = new SqlParameter[] { contraseña, nombreUsuario };
            DBHandler.Select(query,loginParammeters,usuarioMapper,ref usuario);
            if (usuario.Id == 0)
            {
                usuario.Nombre = "Este Usuario no existe";
            }
            return usuario;
        }
    }
}
