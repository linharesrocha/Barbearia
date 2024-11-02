using Microsoft.EntityFrameworkCore;

namespace Barbearia.Infrastructure.Context
{
    public class BarbeariaDbContext : DbContext
    {
        public BarbeariaDbContext(DbContextOptions<BarbeariaDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
