using Microsoft.EntityFrameworkCore;
using SimpleStocker.Api.Models.Entities;
using SimpleStocker.SaleApi.Models.Entities;

namespace SimpleStocker.SaleApi.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> oprions) : base(oprions) { }
        public DbSet<SaleItemModel> SaleItems { get; set; }
        public DbSet<SaleModel> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SaleItemModel>(entity =>
            {
                entity.ToTable("SaleItems"); // Nome da tabela no banco

                entity.HasKey(e => e.Id); // Define a chave primária

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd(); // ID gerado automaticamente

                entity.Property(e => e.CreatedDate)
                    .IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .IsRequired();

                entity.Property(e => e.ProductId)
                    .IsRequired();

                entity.Property(e => e.Quantity)
                    .IsRequired();

                entity.Property(e => e.SubTotal)
                    .IsRequired();


            });

            modelBuilder.Entity<SaleModel>(entity =>
            {
                entity.ToTable("Sales"); // Nome da tabela no banco

                entity.HasKey(e => e.Id); // Define a chave primária

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd(); // ID gerado automaticamente

                entity.Property(e => e.CreatedDate)
                    .IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .IsRequired();

                entity.Property(e => e.ClientId)
                    .IsRequired();

                entity.Property(e => e.TotalAmount)
                    .IsRequired();

                entity.Property(e => e.Discount)
                    .IsRequired();

                entity.Property(e => e.PaymentMethod);

                entity.Property(e => e.Status)
                    .IsRequired();


                entity.HasMany(c => c.Items)
               .WithOne(p => p.Sale)
               .HasForeignKey(p => p.SaleId)
               .OnDelete(DeleteBehavior.ClientCascade);
            });
        }
    }
}
