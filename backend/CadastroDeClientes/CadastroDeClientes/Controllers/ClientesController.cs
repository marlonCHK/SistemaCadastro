using Microsoft.AspNetCore.Mvc;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {
            return Ok("API funcionando");
        }
    }
}
