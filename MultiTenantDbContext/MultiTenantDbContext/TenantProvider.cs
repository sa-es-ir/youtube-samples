namespace MultiTenantDbContext;

public sealed class TenantProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public string? TenantId { get; set; }

    public void GetTenant()
    {
        TenantId = httpContextAccessor
            .HttpContext
            .Request
            .Query[""];
    }
}
