using Microsoft.EntityFrameworkCore;
using SimpleStocker.InventoryApi.Models;

namespace SimpleStocker.InventoryApi.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> oprions) : base(oprions) { }

        public DbSet<InventoryModel> InventoryModel { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InventoryModel>(entity =>
            {
                entity.ToTable("Inventory"); // Nome da tabela no banco

                entity.HasKey(e => e.Id); // Define a chave primária

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd(); // ID gerado automaticamente

                entity.Property(e => e.ProductId)
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired();
            });
        }

    }
}
