using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers.DTOS
{
    public class PostInicioDeSesion 
    {
        public string? NombreUsuario { get; set; }
        public string? Contraseña { get; set; }
    }
}
