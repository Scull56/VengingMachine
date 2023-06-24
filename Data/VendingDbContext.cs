using Microsoft.EntityFrameworkCore;
using VendingMachine.Models;

namespace VendingMachine.Data
{
    public class VendingDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Availability> Availabilities { get; set; } = null!;

        public VendingDbContext(DbContextOptions<VendingDbContext> options) : base(options) {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Availability>().HasData(
                    new Availability { Id = 1, Denomination = 1, State=true },
                    new Availability { Id = 2, Denomination = 2, State = true },
                    new Availability { Id = 3, Denomination = 5, State = true },
                    new Availability { Id = 4, Denomination = 10, State = true }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Напиток", Price = 50, Count = 5, Image = "1.jpg" }
            );
        }
    }
}
