using BlazorSimpleAuth.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorSimpleAuth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly UserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomAuthStateProvider(UserService userService, IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        //throw new NotImplementedException();

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal()));
    }

    public string Login(string username, string password)
    {
        var user = _userService.GetUser(username);

        if (user != null && user.Password == password)
        {
            var claimsIdentity = new ClaimsIdentity(
                [
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "admin")
                ]);

            // generate a JWT token based on the claims
            var token = new JwtSecurityToken(
                issuer: "https://test-issuer.com",
                audience: Guid.NewGuid().ToString(),
                claims: claimsIdentity.Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())),
                SecurityAlgorithms.HmacSha256)
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        throw new Exception("Invalid username or password");
    }
}
