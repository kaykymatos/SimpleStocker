using Microsoft.EntityFrameworkCore;
using SimpleStocker.StockConsumer.Models;

namespace SimpleStocker.StockConsumer.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> oprions) : base(oprions) { }

        public DbSet<InventoryEventModel> Inventory { get; set; }
    }
}
