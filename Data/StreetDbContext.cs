using Microsoft.EntityFrameworkCore;
using StreetService.Domain;
using System.Reflection;

namespace StreetService.Data
{
    public class StreetDbContext : DbContext
    {
        public DbSet<Street> Street { get; set; }

        public StreetDbContext(DbContextOptions<StreetDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply configurations for Street Table
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
