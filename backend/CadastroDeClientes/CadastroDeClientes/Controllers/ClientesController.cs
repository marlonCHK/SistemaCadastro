using Microsoft.AspNetCore.Mvc;
using CadastroDeClientes.Models;
using CadastroDeClientes.Repositories;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _repository;
        public ClientesController()
        {
            _repository = new ClienteRepository();
        }

        [HttpPost("Salvar")]
        public IActionResult Salvar([FromBody] Cliente cadastro )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _repository.Salvar(cadastro);
                return Ok(cadastro);
                
            }
            catch(Exception ex)
            {
                
            }
            return null;
           
        }
    }
}
