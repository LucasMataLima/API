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
        public bool NuevaVenta([FromBody] PostVenta Venta)
        {
            return VentasHandler.CreateNewSale(Venta.producto, Venta.venta);
        }

        [HttpGet]
        public List<Venta> GetVentas(int IdUsuario)
        {
            return VentasHandler.GetVentas(IdUsuario);
        }

        [HttpDelete]
        public bool DeleteVenta([FromBody] Venta venta)
        {
            return VentasHandler.EliminarVenta(venta);
        }
    }
}