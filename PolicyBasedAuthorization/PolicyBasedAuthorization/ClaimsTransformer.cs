using Microsoft.AspNetCore.Authentication;
using PolicyBasedAuthorization;
using System.Security.Claims;

internal class ClaimsTransformer(UserService userService) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var userClaims = userService.GetUserClaims(principal.Identity!.Name!);

        var identity = new ClaimsIdentity(userClaims.Select(claim => new Claim("user-group", claim)));

        principal.AddIdentity(identity);

        return Task.FromResult(principal);
    }
}