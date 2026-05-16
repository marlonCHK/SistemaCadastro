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
                return BadRequest(ex.Message);
            }
            
           
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            List<Cliente> ClientesLista = null;
 
            try
            {
                ClientesLista = _repository.Listar();
                
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(ClientesLista);
           
        }

        [HttpDelete("Deletar")]
        public IActionResult Deletar(string documento)
        {

            try
            {
                _repository.Deletar(documento);


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return NoContent();

        }

        [HttpPut("Atualizar")]
        public IActionResult Atualizar([FromBody] Cliente clienteAtualizado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _repository.Atualizar(clienteAtualizado);
                return NoContent();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /*[HttpGet("Buscar")]
        public IActionResult Atualizar(string documento)
        {
            try
            {
                var ClienteAtualizar = _repository.Buscar(documento);

                return Ok(ClienteAtualizar);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }*/
    }
}
