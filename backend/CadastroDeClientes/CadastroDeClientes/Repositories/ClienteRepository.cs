using CadastroDeClientes.Controllers;
using CadastroDeClientes.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace CadastroDeClientes.Repositories
{
    public class ClienteRepository
    {
        private readonly string PastaBanco =
            Path.Combine(Directory.GetParent(
            Directory.GetCurrentDirectory()).FullName, "BancoDados");

        private readonly string CaminhoArquivo;
        public ClienteRepository()
        {
            //Cria pastaBanco se nao existir
            Directory.CreateDirectory(PastaBanco);

            CaminhoArquivo = Path.Combine(PastaBanco, "clientes.txt");

            //Cria o txt se não existir
            if (!File.Exists(CaminhoArquivo))
            {
                File.WriteAllText(CaminhoArquivo, "[]");
            }
        }

        public void Salvar(Cliente cliente)
        {
            //retorna banco atualizado em lista
            List<Cliente> clientes = Listar();

            //procura se documento repetido para atualizar/deletar
            var ClienteExiste = clientes.Where(c => c.documento == cliente.documento).FirstOrDefault();

            if (ClienteExiste != null) 
            {
                Deletar(cliente.documento.ToString());
                clientes = Listar();
            }
            //adiciona o cliente que foi passado
            clientes.Add(cliente);

            //converte para json
            string ClienteNovoJson =
                JsonSerializer.Serialize(clientes,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });
            //salva no banco .txt
            File.WriteAllText(CaminhoArquivo, ClienteNovoJson);
        }
        
        public List<Cliente> Listar()
        {
            var cliente = File.ReadAllText(CaminhoArquivo);
            List<Cliente> ListaClientes = JsonSerializer.Deserialize<List<Cliente>>(cliente);
            return ListaClientes.OrderByDescending(c => c.nome).ToList();
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
            File.WriteAllText(CaminhoArquivo, ClienteNovoJson);

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
