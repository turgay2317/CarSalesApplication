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

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Here is for Migrations.
        optionsBuilder.UseMySql(
            "Server=localhost;Port=3306;Database=CarSalesProject;Uid=root;Pwd=200034755;",
            new MySqlServerVersion("8.0"), options => options.MigrationsAssembly("CarSalesApplication.API"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
    }
}