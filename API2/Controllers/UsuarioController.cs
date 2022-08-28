using API2;
using API2.DataBase;
using API2.Controllers.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public Usuario GetUsuarios(string UserName)
        {
            return UsuarioHandler.GetUsuario(UserName);
        }

        [HttpDelete]
        public bool DeleteUser([FromBody] int id)
        {
            return UsuarioHandler.DeleteUser(id);
        }

        [HttpPost]
        public bool CrearNuevoUsuario([FromBody] PostUsuario usuario)
        {
            return UsuarioHandler.InsertUser(new Usuario
            {
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail,
            });
        }

        [HttpPut]
        public bool ModificarUsuario([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.UpdateUser(new Usuario
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                Mail = usuario.Mail

            });
        }

    }
}
