using Microsoft.AspNetCore.Mvc;
using API2.Controllers.DTOS;
using API2.DataBase;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public bool NuevaVenta([FromBody] PostVenta venta)
        {
            return VentasHandler.CreateNewSale(new Venta
            {
                Comentarios = venta.Comentarios
            });
        }
    }
}
