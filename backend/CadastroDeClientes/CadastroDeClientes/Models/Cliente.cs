using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public long documento { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string uf { get; set; }

    }
}
