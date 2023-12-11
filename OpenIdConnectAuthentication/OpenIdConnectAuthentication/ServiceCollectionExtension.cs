using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace OpenIdConnectAuthentication;

public static class ServiceCollectionExtension
{
    public static void AddAuthenticationService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
         {
             options.MetadataAddress = configuration.GetValue<string>("IdentityOIDC")!;

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false,
                 ClockSkew = TimeSpan.Zero,
             };
         });
    }
}
