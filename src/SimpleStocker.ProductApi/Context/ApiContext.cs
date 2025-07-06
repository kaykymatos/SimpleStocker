using Microsoft.EntityFrameworkCore;
using SimpleStocker.ProductApi.Models;

namespace SimpleStocker.ProductApi.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> oprions) : base(oprions) { }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CategoryModel>(entity =>
            {
                entity.ToTable("Categories"); // Nome da tabela no banco

                entity.HasKey(e => e.Id); // Define a chave primária

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd(); // ID gerado automaticamente

                entity.Property(e => e.CreatedDate)
                    .IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.HasMany(c => c.Products)
                  .WithOne(p => p.Category)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ProductModel>(entity =>
            {
                entity.ToTable("Products"); // Nome da tabela no banco

                entity.HasKey(e => e.Id); // Define a chave primária

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd(); // ID gerado automaticamente

                entity.Property(e => e.CreatedDate)
                    .IsRequired();

                entity.Property(e => e.UpdatedDate)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.UnityOfMeasurement)
                    .HasMaxLength(20);

                entity.Property(e => e.Price)
                    .IsRequired();

                entity.Property(e => e.CategoryId)
                    .IsRequired();

            });
        }
    }
}
