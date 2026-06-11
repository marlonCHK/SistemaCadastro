using Microsoft.AspNetCore.Mvc;
using CadastroDeClientes.Models;
using CadastroDeClientes.Repositories;
using CadastroDeClientes.Data;

namespace CadastroDeClientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
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
                _context.Clientes.Add(cadastro);
                _context.SaveChanges();
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        
        }

        [HttpGet("Listar")]
        public IActionResult Listar()
        {
            List<Cliente> ClientesLista = null;
 
            try
            {
                //ClientesLista = _repository.Listar();
                
                
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
               // _repository.Deletar(documento);


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
               // _repository.Atualizar(clienteAtualizado);
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
