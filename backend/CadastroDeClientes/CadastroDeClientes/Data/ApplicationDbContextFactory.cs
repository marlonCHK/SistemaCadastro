using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection.Metadata.Ecma335;

namespace CadastroDeClientes.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder =
                new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-6R727PO;Database=SistemaCadastroDB;Trusted_Connection=True;TrustServerCertificate=True;");
            
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
