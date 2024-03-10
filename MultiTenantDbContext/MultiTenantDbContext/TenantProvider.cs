namespace MultiTenantDbContext;

public sealed class TenantProvider
{
    public string TenantKeyName = "TenantId";

    public string? TenantId { get; set; }
}
