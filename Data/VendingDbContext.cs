using Microsoft.EntityFrameworkCore;
using VendingMachine.Models;

namespace VendingMachine.Data
{
    public class VendingDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Availability> Availabilities { get; set; } = null!;

        public VendingDbContext(DbContextOptions<VendingDbContext> options) : base(options) {}
    }
}
