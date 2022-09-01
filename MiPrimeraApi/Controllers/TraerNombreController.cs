using Microsoft.AspNetCore.Mvc;

namespace MiPrimeraApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TraerNombreController : ControllerBase
    {
        [HttpGet]
        public string TraerNombre()
        {
            return "Desafio final";
        }
    }
}
