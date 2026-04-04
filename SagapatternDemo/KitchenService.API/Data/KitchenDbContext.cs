namespace KitchenService.API.Data
{
    using Microsoft.EntityFrameworkCore;
    using KitchenService.API.Entities;

    public class KitchenDbContext : DbContext
    {
        public KitchenDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<KitchenOrder> KitchenOrders { get; set; }
    }
}
