using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Model;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductosVendidosController : ControllerBase
    {

        [HttpGet]
        public List<GetProductoVendido> GetProductos(int IdUsuario)
        {
            List<GetProductoVendido> resultados = new List<GetProductoVendido>();
            try
            {
                List<Producto> productosPorUsuario = ProductoHandler.GetProductosPorIdUsuario(IdUsuario);
                foreach (Producto producto in productosPorUsuario)
                {
                    List<GetProductoVendido> productosVendidos = ProductoVendidoHandler.ObtenerProductosVendidosPorUsuario(producto.IdUsuario);
                    foreach (GetProductoVendido productoVendido in productosVendidos)
                    {
                        resultados.Add(productoVendido);
                    }
                }
                return resultados;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return resultados;
            }
        }
    }
}
