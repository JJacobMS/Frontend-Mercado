using frontendnet.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace frontendnet.Services;

public class AuthClientService(HttpClient client, IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    public async Task<AuthUser> ObtenTokenAsync(string email, string password)
    {
        Login usuario = new() { Email = email, Password = password };
        var response = await client.PostAsJsonAsync("api/auth", usuario);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadFromJsonAsync<AuthUser>();
        if (token == null || string.IsNullOrEmpty(token.Email) || string.IsNullOrEmpty(token.Nombre) || string.IsNullOrEmpty(token.Jwt) || string.IsNullOrEmpty(token.Rol))
        {
            throw new InvalidOperationException();
        }
        return token!;
    }
    public async void IniciaSesionAsync(List<Claim> claims)
    {
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties();
        await httpContextAccessor.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties)!;
    }
}