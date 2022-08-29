using API2.Controllers.DTOS;
using API2.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InicioDeSecionController : ControllerBase
    {
        [HttpPost]
        public Usuario Login([FromBody] PostInicioDeSesion inicioDeSesion )
        {
            return InicioSesion.Login(inicioDeSesion.NombreUsuario, inicioDeSesion.Contraseña);
        }
    }
}
