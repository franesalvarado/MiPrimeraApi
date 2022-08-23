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
                        int cantidadStock = ProductoHandler.CheckStockProducto(productoVendido.IdProducto, productoVendido.Stock);
                        if (cantidadStock != -1)
                        {
                            bool modificarProductoResult = ProductoHandler.ModificarStockProducto(new PutProducto
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

    }
}
