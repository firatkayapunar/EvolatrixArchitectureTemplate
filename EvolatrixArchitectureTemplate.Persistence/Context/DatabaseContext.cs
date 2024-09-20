using EvolatrixArchitectureTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvolatrixArchitectureTemplate.Persistence.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
       : base(options)
        { }
        DbSet<City> Cities { get; set; }
    }
}
