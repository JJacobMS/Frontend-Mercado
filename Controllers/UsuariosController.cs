using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontendnet;

public class UsuariosController(UsuariosClientService usuarios) : Controller
{
    public async Task<IActionResult> Index()
    {
        List<Usuario>? lista = [];
        try
        {
            lista = await usuarios.GetAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }
        return View(lista);
    }

    public async Task<IActionResult> Detalle(string id)
    {
        Usuario? item = null;
        try
        {
            item = await usuarios.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }
        return View(item);
    }

    public IActionResult Crear()
    {
        ViewData["PreTitle"] = "Listado";
        return View();
    }

    public IActionResult CrearUsuario()
    {
        ViewData["PreTitle"] = "Inicio Sesion";
        return View("Crear");
    }

    [HttpPost]
    public async Task<IActionResult> CrearAsync(UsuarioPwd itemToCreate)
    {

        if (ModelState.IsValid)
        {
            try
            {
                var response = await usuarios.PostAsync(itemToCreate);
                TempData["SuccessMessage"] = "Cuenta Creada Exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está registrado.");
                    return View(itemToCreate);
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                {
                    ModelState.AddModelError("Email", "Correo o contraseña inválidos.");
                    ModelState.AddModelError("Password", "Correo o contraseña inválidos.");
                    return View(itemToCreate);
                }
                ModelState.AddModelError("Email", "Error inesperado al procesar la solicitud.");
            }
        }
        ModelState.AddModelError("Email", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToCreate);
    }

    [HttpGet("[controller]/[action]/{email?}")]
    public async Task<IActionResult> EditarAsync(string email)
    {
        Usuario? itemToEdit = null;
        try
        {
            itemToEdit = await usuarios.GetAsync(email);
            if (itemToEdit == null)
            {
                return NotFound();
            }
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }
        ViewBag.PuedeEditar = (User.Identity?.Name == email);
        return View(itemToEdit);
    }

    [HttpPost("[controller]/[action]/{email?}")]
    public async Task<IActionResult> EditarAsync(string email, Usuario itemToEdit)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var response = await usuarios.PutAsync(itemToEdit);
                TempData["SuccessMessage"] = "Usuario modificado exitosamente";
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.UnprocessableEntity)
                {
                    ModelState.AddModelError("Nombre", "Correo invalido.");
                    return View(itemToEdit);
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ModelState.AddModelError("Nombre", "El nombre del usuario es obligatorio y debe ser una cadena con al menos un carácter.");
                    return View(itemToEdit);
                }
                ModelState.AddModelError("Nombre", "Error inesperado al procesar la solicitud.");
            }
        }
        ModelState.AddModelError("Nombre", "No ha sido posible realizar la acción. Inténtelo nuevamente.");
        return View(itemToEdit);
    }

    public async Task<IActionResult> Eliminar(string id, bool? showError = false)
    {
        Usuario? itemToDelete = null;
        try
        {
            itemToDelete = await usuarios.GetAsync(id);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            if (showError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "No ha sido posible realizar la acción. Inténtelo nuevamente.";
            }
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }
        ViewBag.PuedeEditar = !(User.Identity?.Name == id);
        return View(itemToDelete);
    }

    [HttpPost]
    public async Task<IActionResult> Eliminar(string id)
    {
        TempData.Remove("Message");
        TempData.Remove("ErrorMessage");
        if (ModelState.IsValid)
        {
            try
            {
                var response = await usuarios.DeleteAsync(id);
                TempData["SuccessMessage"] = "Se ha eliminado el usuario correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {

                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    TempData["ErrorMessage"] = "No se puede eliminar un usuario protegido";
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    TempData["ErrorMessage"] = "No se puede eliminar un usuario con compras.";
                }
                else if (ex.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    TempData["Message"] = "Usuario eliminado correctamente.";
                }
            }
        }
        return RedirectToAction(nameof(Eliminar), new { id, showError = true });
    }
}