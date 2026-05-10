using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Models
{
    public class Clientes
    {
        public int Id { get; set; }

        [Required]
        public int Documento { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Uf { get; set; }

    }
}
