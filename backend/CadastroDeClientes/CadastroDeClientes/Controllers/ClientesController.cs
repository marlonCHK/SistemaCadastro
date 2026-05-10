using Microsoft.AspNetCore.Mvc;
using CadastroDeClientes.Models;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        [HttpPost("Salvar")]
        public IActionResult Salvar([FromBody] Clientes cliente )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok("cliente");
        }
    }
}
