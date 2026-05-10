using System.ComponentModel.DataAnnotations;

namespace CadastroDeClientes.Models
{
    public class Cliente
    {
        public int id { get; set; }

        [Required]
        public int documento { get; set; }

        [Required]
        public string nome { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string uf { get; set; }

    }
}
