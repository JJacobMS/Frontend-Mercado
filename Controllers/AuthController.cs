using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;

namespace frontendnet;

public class AuthController(AuthClientService auth) : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IndexAsync(Login model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var token = await auth.ObtenTokenAsync(model.Email, model.Password);
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, token.Email),
                    new(ClaimTypes.GivenName, token.Nombre),
                    new("jwt", token.Jwt),
                    new(ClaimTypes.Role, token.Rol),
                };
                auth.IniciaSesionAsync(claims);

                if (token.Rol == "Administrador")
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (HttpRequestException ex)
            {

                if (ex.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                {
                    ModelState.AddModelError("Email", "Credenciales no válidas. Inténtelo nuevamente.");
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("Email", "No se ha encontrado un usuario con dicho correo.");
                }
                else
                {
                    ModelState.AddModelError("Email", "No se pudo conectar al servidor. Intente nuevamente.");
                }
            }
        }
        return View(model);
    }

    [Authorize(Roles = "Administrador, Usuario")]
    public async Task<IActionResult> SalirAsync()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Auth");
    }

    public IActionResult CrearCuenta()
    {
        return RedirectToAction("CrearUsuario", "Usuarios");
    }
}