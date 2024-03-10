using System.Globalization;

namespace MultiTenantDbContext;

public class TenantIdentifierMiddleware
{
    private readonly RequestDelegate next;

    public TenantIdentifierMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context, TenantProvider tenantProvider)
    {
        tenantProvider.TenantId = context.Request.Query["tenantId"];

        await next(context);
    }
}
