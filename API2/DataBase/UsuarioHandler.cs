using API2.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class UsuarioHandler : DBHandler 
    {
        public static Usuario GetUsuario(string NombreUsuario)
        {
            var usuarioMapper = new UsuarioMapper();            
            var usuario = new Usuario();
            var query = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario";
            var nombreUsuario = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = NombreUsuario };
            var sqlParameter = new SqlParameter[] {nombreUsuario};
            DBHandler.Select(query, sqlParameter, usuarioMapper, ref usuario);
            return usuario;
        }
        public static bool DeleteUser(int Id)
        {
            var queryDelete = "DELETE FROM Usuario WHERE Id = @id";
            var sqlParamenter = new SqlParameter("id", SqlDbType.BigInt) { Value = Id };
            var U = DBHandler.Delete(queryDelete, sqlParamenter);
            return U;
        }
        public static bool InsertUser(Usuario usuario)
        {
            var U = false;
            if (!string.IsNullOrWhiteSpace(usuario.NombreUsuario))
            {
                var ExisteUsuario = GetUsuario(usuario.NombreUsuario);
                if (ExisteUsuario.Id == 0)
                { 
                    var queryInsert = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                                      "VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)";
                    var nombre = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                    var apellido = new SqlParameter("Apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                    var nombreUsuario = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                    var contraseña = new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                    var mail = new SqlParameter("Mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };
                    var ParametrosUs = new SqlParameter[] { nombre, apellido, nombreUsuario, contraseña, mail };
                    U = DBHandler.InsertUpdate(queryInsert, ParametrosUs);
                }
            }
            return U;
        }
        public static bool UpdateUser(Usuario usuario)
        {
            var queryUpdate = "UPDATE [SistemaGestion].[dbo].[Usuario]" +
                              "SET Nombre = @nombre, Apellido = @apellido, " +
                              "NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail " +
                              "WHERE Id = @id ";
            var nombre = new SqlParameter("nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
            var apellido = new SqlParameter("apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
            var nombreUsuario = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
            var contraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
            var mail = new SqlParameter("mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };
            var id = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = usuario.Id };
            var ParametrosUs = new SqlParameter[] { nombre, apellido, nombreUsuario, contraseña, mail, id };
            var User = DBHandler.InsertUpdate(queryUpdate, ParametrosUs);
            return User;
        }
    }
}