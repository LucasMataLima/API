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
        public bool NuevaVenta([FromBody] List<Producto> producto, Venta venta)
        {
            return VentasHandler.CreateNewSale( producto, venta);
        }
    }
}
