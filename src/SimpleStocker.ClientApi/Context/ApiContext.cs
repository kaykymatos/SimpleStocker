using Microsoft.EntityFrameworkCore;
using SimpleStocker.ClientApi.Models;

namespace SimpleStocker.ClientApi.Context
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> oprions) : base(oprions) { }

        public DbSet<ClientModel> Clients { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClientModel>(entity =>
            {
                entity.ToTable("Clients"); // Nome da tabela no banco

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

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.PhoneNumer)
                    .HasMaxLength(20);

                entity.Property(e => e.Address)
                    .HasMaxLength(150);

                entity.Property(e => e.AddressNumber)
                    .HasMaxLength(10);

                entity.Property(e => e.Active)
                    .IsRequired();

                entity.Property(e => e.BirthDate)
                    .IsRequired();
            });
        }

    }
}
