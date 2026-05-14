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
            //Lê txt dos clientes
            string ClienteJson = File.ReadAllText(CaminhoArquivo);
            //converte
            List<Cliente> clientes = JsonSerializer.Deserialize<List<Cliente>>(ClienteJson);

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

    }
}
