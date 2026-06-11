using CadastroDeClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeClientes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
