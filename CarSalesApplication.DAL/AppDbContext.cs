using CarSalesApplication.Core.Enums;
using CarSalesApplication.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarSalesApplication.DAL;

public class AppDbContext : DbContext
{
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<CarPart> CarParts { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Here is for Migrations.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<CarPart>()
            .Property(cp => cp.Status)
            .HasDefaultValue(PartStatus.Unspecified);
    }
}