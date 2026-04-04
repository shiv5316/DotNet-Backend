
    using Microsoft.EntityFrameworkCore;
    using OrderService.API.Entities;

namespace OrderService.API.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
    }
}
