using Dgii.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dgii.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Taxpayer> Taxpayers { get; set; }
        public DbSet<TaxReceipt> TaxReceipts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Taxpayer>(entity =>
            {
                entity.HasKey(e => e.RncCedula);
                entity.Property(e => e.RncCedula).HasMaxLength(11);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Tipo).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Estatus).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<TaxReceipt>(entity =>
            {
                entity.HasKey(e => e.NCF);
                entity.Property(e => e.NCF).HasMaxLength(13);
                entity.Property(e => e.Monto).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Itbis18).HasColumnType("decimal(18,2)");

                entity.HasOne(e => e.Taxpayer)
                    .WithMany(t => t.TaxReceipts)
                    .HasForeignKey(e => e.RncCedula)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            // Seed Data
            modelBuilder.Entity<Taxpayer>().HasData(
                new Taxpayer { RncCedula = "98754321012", Nombre = "JUAN PEREZ", Tipo = "PERSONA FISICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "123456789", Nombre = "FARMACIA TU SALUD", Tipo = "PERSONA JURIDICA", Estatus = "inactivo" }
            );

            modelBuilder.Entity<TaxReceipt>().HasData(
                new TaxReceipt { RncCedula = "98754321012", NCF = "E310000000001", Monto = 200.00m, Itbis18 = 36.00m },
                new TaxReceipt { RncCedula = "98754321012", NCF = "E310000000002", Monto = 1000.00m, Itbis18 = 180.00m }
            );
        }
    }
}
