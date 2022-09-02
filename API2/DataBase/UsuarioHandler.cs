using API2.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class UsuarioHandler : DBHandler 
    {
        public static Usuario GetUsuario(string NombreUsuario)
        {
            UsuarioMapper usuarioMapper = new UsuarioMapper();
            Usuario usuario = new Usuario();


            var query = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario";
            var nombreUsuario = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = NombreUsuario };
            SqlParameter[] sqlParameter = new SqlParameter[] {nombreUsuario};
            //usuario = DBHandler.Select(query, sqlParameter, usuarioMapper);

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddRange(sqlParameter);
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                usuario = usuarioMapper.CargarObj(dataReader);
                            }
                        }
                        sqlConnection.Close();
                    }
                }
            }
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
            bool U;
            var ExisteUsuario = GetUsuario(usuario.NombreUsuario);
            if (ExisteUsuario.Id == 0)
            { 
                string queryInsert = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                                     "VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)";

                var nombre = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                var apellido = new SqlParameter("Apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                var nombreUsuario = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                var contraseña = new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                var mail = new SqlParameter("Mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };
                SqlParameter[] ParametrosUs = new SqlParameter[] { nombre, apellido, nombreUsuario, contraseña, mail };
                U = DBHandler.InsertUpdate(queryInsert, ParametrosUs);
            }
            else
            {
                U = false;
            }
            return U;
        }
        public static bool UpdateUser(Usuario usuario)
        {
            string queryUpdate = "UPDATE [SistemaGestion].[dbo].[Usuario]" +
                                 "SET Nombre = @nombre, Apellido = @apellido, " +
                                 "NombreUsuario = @nombreUsuario, Contraseña = @contraseña, Mail = @mail " +
                                 "WHERE Id = @id ";

            var nombre = new SqlParameter("nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
            var apellido = new SqlParameter("apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
            var nombreUsuario = new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
            var contraseña = new SqlParameter("contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
            var mail = new SqlParameter("mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };
            var id = new SqlParameter("id", System.Data.SqlDbType.BigInt) { Value = usuario.Id };
            SqlParameter[] ParametrosUs = new SqlParameter[] { nombre, apellido, nombreUsuario, contraseña, mail, id };

            var User = DBHandler.InsertUpdate(queryUpdate, ParametrosUs);
            return User;
        }
    }
}