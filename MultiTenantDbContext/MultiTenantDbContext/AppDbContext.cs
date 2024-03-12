using Microsoft.EntityFrameworkCore;

namespace MultiTenantDbContext;

public class AppDbContext : DbContext
{
    private readonly string? _tenantId;

    public AppDbContext(DbContextOptions<AppDbContext> options,
        TenantProvider tenantProvider) : base(options)
    {
        _tenantId = tenantProvider.TenantId;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Student>()
            .HasQueryFilter(x => x.TenantId == _tenantId);
    }

    public Task<List<Student>> GetStudentsAsync(CancellationToken cancellationToken)
    {
        return Students.ToListAsync(cancellationToken);
    }

    public Task<List<Student>> GetAllStudentsAsync(CancellationToken cancellationToken)
    {
        return Students
            .IgnoreQueryFilters()
            .ToListAsync(cancellationToken);
    }

    public DbSet<Student> Students { get; set; }
}


public class Student
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? TenantId { get; set; }
}