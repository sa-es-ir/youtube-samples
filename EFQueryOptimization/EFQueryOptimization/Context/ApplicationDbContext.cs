using EFQueryOptimization.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFQueryOptimization.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {

    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<PayRoll> PayRolls { get; set; }

    public DbSet<Department> Departments { get; set; }
}
