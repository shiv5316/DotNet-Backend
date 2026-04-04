namespace DeliveryService.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using DeliveryService.API.Entities;

    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Delivery> Deliveries { get; set; }
    }
}
