using Microsoft.EntityFrameworkCore;
using NearbyLocationSearch.Data.Entities;

namespace NearbyLocationSearch.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Location>()
                         .ToTable("Location");
        }

        public DbSet<Location> Locations { get; set; }
    }
}