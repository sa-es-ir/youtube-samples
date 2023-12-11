using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace OpenIdConnectAuthentication;

public class CustomAuthMiddleware : IMiddleware
{
    private readonly IConfiguration _configuration;

    public CustomAuthMiddleware(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var token = GetAccessTokenFromHeader(context);

        var oidc = _configuration.GetValue<string>("IdentityOIDC");

        var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(oidc,
                               new OpenIdConnectConfigurationRetriever());

        var openIdConfig = await configurationManager.GetConfigurationAsync(context.RequestAborted);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        tokenValidationParameters.IssuerSigningKeys = openIdConfig.SigningKeys;
        tokenValidationParameters.ValidIssuer = openIdConfig.Issuer;

        var tokenValidator = new JwtSecurityTokenHandler();

        try
        {
            tokenValidator.ValidateToken(
                    token, tokenValidationParameters, out var secToken);

            var securityToken = (secToken as JwtSecurityToken)!;
            var claims = securityToken.Claims.ToList();

            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 401;
            return;
        }
    }

    private string GetAccessTokenFromHeader(HttpContext context)
    {
        var scheme = "Bearer";

        var tokenWithScheme = context.Request.Headers[HeaderNames.Authorization].ToString();

        return tokenWithScheme.Substring($"{scheme} ".Length).Trim();
    }
}