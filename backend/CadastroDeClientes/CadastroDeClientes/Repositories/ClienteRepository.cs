using CadastroDeClientes.Controllers;
using CadastroDeClientes.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace CadastroDeClientes.Repositories
{
    public class ClienteRepository
    {
        public void Salvar(Cliente cliente)
        {
            
        }
        
        public List<Cliente> Listar()
        {
            //var cliente = File.ReadAllText(CaminhoArquivo);
            //List<Cliente> ListaClientes = JsonSerializer.Deserialize<List<Cliente>>(cliente);
            //return ListaClientes.OrderByDescending(c => c.nome).ToList();
            return null;
        }

        public void Deletar(string documento)
        {
            //retorna o banco
            var clientes = Listar();

            //procura cliente para remover
            var clienteRemover = clientes.FirstOrDefault(c => 
            c.documento.ToString() == documento);

            if (clienteRemover != null)
            {
                clientes.Remove(clienteRemover);
            }
            //salva o banco
            string ClienteNovoJson =
                JsonSerializer.Serialize(clientes,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
            //File.WriteAllText(CaminhoArquivo, ClienteNovoJson);

        }

        public void Atualizar(Cliente ClienteAtualizar)
        {
            var Clientes = Listar();
            var ClienteAntigo = Clientes.FirstOrDefault(c =>
            c.documento == ClienteAtualizar.documento);
            if (ClienteAntigo != null)
            {
                Salvar(ClienteAtualizar);
            }
        }

        /*public Cliente Buscar(string documento)
        {
            
            //retorna o banco
            var clientes = Listar();
            //procura o cliente para atualizar
            var clienteBusca = clientes.FirstOrDefault(c =>
            c.documento.ToString() == documento);
            return clienteBusca;
            

        }*/

    }
}
