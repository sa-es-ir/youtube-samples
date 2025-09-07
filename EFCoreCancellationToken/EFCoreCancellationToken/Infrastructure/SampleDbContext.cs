using Microsoft.EntityFrameworkCore;

namespace EFCoreCancellationToken.Infrastructure;

public class SampleDbContext(DbContextOptions<SampleDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; }
}
