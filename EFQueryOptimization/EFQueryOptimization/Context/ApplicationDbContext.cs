﻿using EFQueryOptimization.Entities;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=127.0.0.1;Database=EFOptimize;Uid=sa;Password=123456;TrustServerCertificate=True",
            opt => opt.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
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
