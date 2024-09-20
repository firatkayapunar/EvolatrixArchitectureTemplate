using EvolatrixArchitectureTemplate.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EvolatrixArchitectureTemplate.Persistence
{
    public class ContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            var connectionString = "Data Source=localhost;Database=EvolatrixArchitectureTemplateDb;Integrated Security=True;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
 