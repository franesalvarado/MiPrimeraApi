namespace MiPrimeraApi.Controllers.DTOS
{

    public class CargarVentaArray
    {
        public int Stock { get; set; }
        public int IdProducto { get; set; }
    }

    public class CargarVenta
    {
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public List<CargarVentaArray> ProductosVendidos { get; set; } 
    }
}
