using API2.DataBase;
using Microsoft.AspNetCore.Mvc;


namespace API2.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet (Name= "GetProductos")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }

        [HttpPut]
        public bool ModificarProducto([FromBody] Producto producto)
        {
            return ProductoHandler.UpdateProducto(new Producto
            {
                Id = producto.Id,
                Descripciones = producto.Descripciones,
                Costo = producto.Costo,
                PrecioVenta = producto.PrecioVenta,
                Stock = producto.Stock,
                IdUsuario = producto.IdUsuario,
            });
        }

        [HttpPost]
        public bool CrearProducto([FromBody] Producto producto)
        {
            return ProductoHandler.InsertProducto(new Producto
            {
                Descripciones=producto.Descripciones,
                Costo=producto.Costo,
                PrecioVenta=producto.PrecioVenta,
                Stock=producto.Stock,
                IdUsuario=producto.IdUsuario,
            });
        }

        [HttpDelete]
        public bool DeleteProduct([FromBody] int id)
        {
            return ProductoHandler.DeleteProduct(id);
        }
    }
}
