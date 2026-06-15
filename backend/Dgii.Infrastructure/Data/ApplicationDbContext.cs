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
                new Taxpayer { RncCedula = "123456789", Nombre = "FARMACIA TU SALUD", Tipo = "PERSONA JURIDICA", Estatus = "inactivo" },
                new Taxpayer { RncCedula = "101010101", Nombre = "SUPERMERCADOS NACIONAL", Tipo = "PERSONA JURIDICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "202020202", Nombre = "CLARO DOMINICANA", Tipo = "PERSONA JURIDICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "303030303", Nombre = "BANCO POPULAR DOMINICANO", Tipo = "PERSONA JURIDICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "404040404", Nombre = "MARIA GOMEZ", Tipo = "PERSONA FISICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "505050505", Nombre = "FERRETERIA AMERICANA", Tipo = "PERSONA JURIDICA", Estatus = "inactivo" },
                new Taxpayer { RncCedula = "606060606", Nombre = "JOSE RODRIGUEZ", Tipo = "PERSONA FISICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "707070707", Nombre = "GRUPO RAMOS", Tipo = "PERSONA JURIDICA", Estatus = "activo" },
                new Taxpayer { RncCedula = "808080808", Nombre = "ANA MARTINEZ", Tipo = "PERSONA FISICA", Estatus = "inactivo" }
            );

            modelBuilder.Entity<TaxReceipt>().HasData(
                new TaxReceipt { RncCedula = "98754321012", NCF = "E310000000001", Monto = 200.00m, Itbis18 = 36.00m },
                new TaxReceipt { RncCedula = "98754321012", NCF = "E310000000002", Monto = 1000.00m, Itbis18 = 180.00m },
                new TaxReceipt { RncCedula = "101010101", NCF = "E310000000003", Monto = 5000.00m, Itbis18 = 900.00m },
                new TaxReceipt { RncCedula = "101010101", NCF = "E310000000004", Monto = 1500.00m, Itbis18 = 270.00m },
                new TaxReceipt { RncCedula = "202020202", NCF = "E310000000005", Monto = 350.00m, Itbis18 = 63.00m },
                new TaxReceipt { RncCedula = "404040404", NCF = "E310000000006", Monto = 1200.00m, Itbis18 = 216.00m },
                new TaxReceipt { RncCedula = "606060606", NCF = "E310000000007", Monto = 400.00m, Itbis18 = 72.00m },
                new TaxReceipt { RncCedula = "707070707", NCF = "E310000000008", Monto = 10000.00m, Itbis18 = 1800.00m },
                new TaxReceipt { RncCedula = "707070707", NCF = "E310000000009", Monto = 2500.00m, Itbis18 = 450.00m }
            );
        }
    }
}
