namespace MiPrimeraApi.Controllers.DTOS
{
    public class PostUsuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Mail { get; set; }
    }
    public class PutNombreUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    public class PutUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Contraseña { get; set; }
        public string NombreUsuario { get; set; }
        public string Mail { get; set; }
    }
}
