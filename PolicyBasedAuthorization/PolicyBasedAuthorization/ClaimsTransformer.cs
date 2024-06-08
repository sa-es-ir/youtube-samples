using Microsoft.AspNetCore.Authentication;
using PolicyBasedAuthorization;
using System.Security.Claims;

internal class ClaimsTransformer(UserService userService) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        return Task.FromResult(principal);
    }
}