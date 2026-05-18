using Microsoft.EntityFrameworkCore;
using Tutorial7.Models;

namespace Tutorial7.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PC> PCs { get; set; }
    public DbSet<Component> Components { get; set; }
    public DbSet<PCComponent> PCComponents { get; set; }
    public DbSet<ComponentType> ComponentTypes { get; set; }
    public DbSet<ComponentManufacturer> ComponentManufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PC>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(50).IsRequired();
            e.Property(x => x.Weight).HasColumnType("float(5)");
            e.Property(x => x.CreatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<ComponentType>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Abbreviation).HasMaxLength(30).IsRequired();
            e.Property(x => x.Name).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<ComponentManufacturer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Abbreviation).HasMaxLength(30).IsRequired();
            e.Property(x => x.FullName).HasMaxLength(300).IsRequired();
            e.Property(x => x.FoundationDate).HasColumnType("date");
        });

        modelBuilder.Entity<Component>(e =>
        {
            e.HasKey(x => x.Code);
            e.Property(x => x.Code).HasColumnType("char(10)");
            e.Property(x => x.Name).HasMaxLength(300).IsRequired();
            e.Property(x => x.Description).HasColumnType("nvarchar(max)");
            e.HasOne(x => x.Manufacturer)
                .WithMany(x => x.Components)
                .HasForeignKey(x => x.ComponentManufacturersId);
            e.HasOne(x => x.ComponentType)
                .WithMany(x => x.Components)
                .HasForeignKey(x => x.ComponentTypesId);
        });

        modelBuilder.Entity<PCComponent>(e =>
        {
            e.HasKey(x => new { x.PCId, x.ComponentCode });
            e.Property(x => x.ComponentCode).HasColumnType("char(10)");
            e.HasOne(x => x.PC)
                .WithMany(x => x.PCComponents)
                .HasForeignKey(x => x.PCId);
            e.HasOne(x => x.Component)
                .WithMany(x => x.PCComponents)
                .HasForeignKey(x => x.ComponentCode);
        });

        modelBuilder.Entity<ComponentType>().HasData(
            new ComponentType { Id = 1, Abbreviation = "CPU", Name = "Processor" },
            new ComponentType { Id = 2, Abbreviation = "RAM", Name = "Memory" },
            new ComponentType { Id = 3, Abbreviation = "GPU", Name = "Graphics Card" }
        );

        modelBuilder.Entity<ComponentManufacturer>().HasData(
            new ComponentManufacturer { Id = 1, Abbreviation = "Intel", FullName = "Intel Corporation", FoundationDate = new DateOnly(1968, 7, 18) },
            new ComponentManufacturer { Id = 2, Abbreviation = "AMD", FullName = "Advanced Micro Devices", FoundationDate = new DateOnly(1969, 5, 1) },
            new ComponentManufacturer { Id = 3, Abbreviation = "NVIDIA", FullName = "NVIDIA Corporation", FoundationDate = new DateOnly(1993, 4, 5) }
        );

        modelBuilder.Entity<Component>().HasData(
            new Component { Code = "CPU0000001", Name = "Intel Core i9-13900K", Description = "High-end desktop processor", ComponentManufacturersId = 1, ComponentTypesId = 1 },
            new Component { Code = "RAM0000001", Name = "Kingston DDR5 32GB", Description = "DDR5 RAM module", ComponentManufacturersId = 2, ComponentTypesId = 2 },
            new Component { Code = "GPU0000001", Name = "NVIDIA RTX 4090", Description = "Top-tier graphics card", ComponentManufacturersId = 3, ComponentTypesId = 3 }
        );

        modelBuilder.Entity<PC>().HasData(
            new PC { Id = 1, Name = "Gaming Beast X", Weight = 12.5, Warranty = 36, CreatedAt = new DateTime(2026, 5, 8, 9, 0, 0), Stock = 5 },
            new PC { Id = 2, Name = "Office Mini Pro", Weight = 4.2, Warranty = 24, CreatedAt = new DateTime(2026, 4, 15, 13, 30, 0), Stock = 12 },
            new PC { Id = 3, Name = "Workstation Z", Weight = 18.0, Warranty = 48, CreatedAt = new DateTime(2026, 3, 1, 10, 0, 0), Stock = 3 }
        );

        modelBuilder.Entity<PCComponent>().HasData(
            new PCComponent { PCId = 1, ComponentCode = "CPU0000001", Amount = 1 },
            new PCComponent { PCId = 1, ComponentCode = "GPU0000001", Amount = 2 },
            new PCComponent { PCId = 2, ComponentCode = "RAM0000001", Amount = 2 },
            new PCComponent { PCId = 3, ComponentCode = "CPU0000001", Amount = 2 },
            new PCComponent { PCId = 3, ComponentCode = "RAM0000001", Amount = 4 }
        );
    }
}
