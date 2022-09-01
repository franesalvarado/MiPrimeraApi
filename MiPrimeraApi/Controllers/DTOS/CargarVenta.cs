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

    public class TodasVentas
    {
        public int IdVenta { get; set; }
        public string VentaComentario { get; set; }
        public int ProductoVendidoId { get; set; }
        public int ProductoVendidoStock { get; set; }
        public int ProductoId { get; set; }
        public string ProductoDescripciones { get; set; }
        public double ProductoCosto { get; set; }
        public double PrecioVenta { get; set; }
        public int ProductoStock { get; set; }

    }
}
