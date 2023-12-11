
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Security.Claims;

#pragma warning disable CS0618

namespace OpenIdConnectAuthentication
{
    public class CustomAuthenticationHandler : AuthenticationHandler<JwtBearerOptions>
    {
        private readonly IConfiguration _configuration;

        public CustomAuthenticationHandler(IOptionsMonitor<JwtBearerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _configuration = configuration;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var token = GetAccessTokenFromHeader();

            var oidc = _configuration.GetValue<string>("IdentityOIDC");

            var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(oidc,
                                   new OpenIdConnectConfigurationRetriever());

            var openIdConfig = await configurationManager.GetConfigurationAsync(Request.HttpContext.RequestAborted);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
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

                claims.Add(new Claim(ClaimTypes.Role.ToString(), "user"));

                var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(new ClaimsIdentity(claims)), Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail($"token validation failed: {ex.Message}");
            }

        }

        private string GetAccessTokenFromHeader()
        {
            var scheme = "Bearer";

            var tokenWithScheme = Request.Headers[HeaderNames.Authorization].ToString();

            return tokenWithScheme.Substring($"{scheme} ".Length).Trim();
        }
    }
}
