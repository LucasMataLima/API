using System.Data;
using System.Data.SqlClient;

namespace API2.DataBase
{
    public class UsuarioHandler : DBHandler
    {
        public static List<Usuario> GetUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();
            // el ConnectionString se encuientra en DBHandler
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                var query = "SELECT * FROM Usuario";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlConnection.Open();
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                var usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dataReader["ID"]);
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();
                                usuarios.Add(usuario);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return usuarios;
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
            string queryInsert = "INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                                 "VALUES (@nombre, @apellido, @nombreUsuario, @contraseña, @mail)";
            var nombre = new SqlParameter("Nombre", System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
            var apellido = new SqlParameter("Apellido", System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
            var nombreUsuario = new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
            var contraseña = new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
            var mail = new SqlParameter("Mail", System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

            var U = DBHandler.Insert(queryInsert, nombre,apellido,nombreUsuario,contraseña,mail);
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
            var U = DBHandler.Update(queryUpdate, nombre, apellido, nombreUsuario, contraseña, mail, id);
            return U;

        }
    }
}