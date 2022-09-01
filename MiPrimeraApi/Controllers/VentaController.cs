using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Controllers.DTOS;
using MiPrimeraApi.Repository;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpPost]
        public void CargarVenta([FromBody] CargarVenta cargarVenta)
        {
            try
            {
                bool ventaCargada = VentaHandler.CargarVenta(cargarVenta.Comentarios);
                if (ventaCargada)
                {
                    int lastIdVenta = VentaHandler.GetLastIdVenta();
                    foreach (CargarVentaArray productoVendido in cargarVenta.ProductosVendidos)
                    {
                        int cantidadStock = ProductoHandler.CheckStockProductoPostVenta(productoVendido.IdProducto, productoVendido.Stock);
                        if (cantidadStock != -1)
                        {
                            bool modificarProductoResult = ProductoHandler.ModificarStockProductoPostVenta(new PutProducto
                            {
                                Id = productoVendido.IdProducto,
                                Stock = cantidadStock,
                                IdUsuario = cargarVenta.IdUsuario
                            });

                            if (modificarProductoResult)
                            {
                                bool cargarProductoVentaResult = ProductoVendidoHandler.CargarProductoVendido(new PostProductoVendido
                                {
                                    Stock = productoVendido.Stock,
                                    IdProducto = productoVendido.IdProducto,
                                    IdVenta = lastIdVenta
                                });
                                if (cargarProductoVentaResult)
                                {
                                    Console.WriteLine("La venta se ha cargado exitosamente");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se puede cargar la venta del producto " + productoVendido.IdProducto + " porque no hay stock");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [HttpDelete]
        public bool EliminarVenta(int id)
        {
            try
            {
                List <GetProductoVendido> productosVendidos = ProductoVendidoHandler.ObtenerProductoVendidoPorIdVenta(id);
                ProductoVendidoHandler.EliminarProductoVendidoPorIdVenta(id);
                foreach(GetProductoVendido productoVendido in productosVendidos){
                    // Busca el producto, obtiene el stock y le suma el stock del elemento anteriormente vendido
                    int stock = ProductoHandler.CheckStockProductoPostCancelacionVenta(productoVendido.IdProducto, productoVendido.Stock);
                    ProductoHandler.ModificarStockProductoPostCancelacionVenta(new PutProducto
                    {
                        Id = productoVendido.IdProducto,
                        Descripciones = "", // No se utiliza para la query ni nada
                        Costo = 0, // No se utiliza para la query ni nada
                        PrecioVenta = 0, // No se utiliza para la query ni nada
                        Stock = stock,
                        IdUsuario = 0 // No se utiliza para la query ni nada
                    });
                }
                return VentaHandler.EliminarVenta(id);
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        [HttpGet]
        public List<TodasVentas> GetVentas()
        {
            try
            {
                return VentaHandler.GetVentas();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

    }
}
