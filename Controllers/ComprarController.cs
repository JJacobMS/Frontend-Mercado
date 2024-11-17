using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet;

[Authorize(Roles = "Usuario")]
public class ComprarController(ProductosClientService productos, IConfiguration configuration) : Controller
{
    public async Task<IActionResult> Index(string? s)
    {
        List<Producto>? lista = [];
        try
        {
            lista = await productos.GetAsync(s);
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }

        ViewBag.Url = configuration["UrlWebAPI"];
        ViewBag.search = s;
        return View(lista);
    }
    public async Task<IActionResult> Detalle(int id)
    {
        Producto? item = null;
        List<CarritoProducto>? productosCarrito = null;
        ViewBag.Url = configuration["UrlWebAPI"];
        ViewBag.EnCarrito = false;
        try
        {
            item = await productos.GetAsync(id);
            if (item == null) return NotFound();

            productosCarrito = await productos.GetProductoCarritoAsync(id);
            if(productosCarrito != null && productosCarrito.Count > 0){
                ViewBag.EnCarrito = true;
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

    [HttpPost]
    public async Task<IActionResult> CarritoAsync(int id, int cantidad)
    {
        try
        {
            await productos.PostProductoCarritoAsync(id, cantidad);
            return View("AgregadoCarrito");
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }
        return RedirectToAction("Error", "Home");
    }

}
