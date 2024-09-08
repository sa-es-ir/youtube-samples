using EFCoreLargeData.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLargeData;

public class TestDbContext : DbContext
{
    private string _connectionString;
    public TestDbContext(string? connectionString = null)
    {
        _connectionString = connectionString
            ?? "Server=localhost;Database=EFCoreLargeData;user id=sa;password=P@ssw0rd.123!;TrustServerCertificate=True";
    }
    public DbSet<TextTable1MB> TextTable1MBs { get; set; }
    public DbSet<TextTable2MB> TextTable2MBs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }
}

