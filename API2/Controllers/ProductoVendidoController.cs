using API2.DataBase;
using Microsoft.AspNetCore.Mvc;

namespace API2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet]
        public List<ProductoVendidoyProducto> GetProductosVendidos(int IdUsuario)
        {
            return ProductosVendidosHandler.GetProductoVendido(IdUsuario);
        }
    }
}
