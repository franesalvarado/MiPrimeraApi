namespace MiPrimeraApi.Controllers.DTOS
{
    public class PostProducto
    {
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }
    public class PutProducto
    {
        public int Id { get; set; }
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }

    public class PutStockProducto
    {
        public int Id { get; set; }
        public int Stock { get; set; }
    }

}
