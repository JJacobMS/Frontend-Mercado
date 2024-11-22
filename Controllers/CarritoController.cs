using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontendnet;

[Authorize(Roles = "Usuario")]
public class CarritoController(CarritosClientService carritos, IConfiguration configuration) : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> CarritoComprasPartial()
    {
        List<Carrito>? lista = [];
        List<CarritoProducto> listaproductos = [];
        ViewBag.Url = configuration["UrlWebAPI"];
        try
        {
            lista = await carritos.GetAsync();
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }

        if (lista != null && lista.Count > 0)
        {
            foreach (var item in lista)
            {
                if (item.CarritoProductos != null)
                {
                    listaproductos = item.CarritoProductos;
                }
            }
        }
        return PartialView("_PartialCardCarrito", listaproductos);
    }

    public async Task<IActionResult> Eliminar(int itemid, bool? showError = false)
    {
        List<CarritoProducto>? itemToDelete = null;
        CarritoProducto? itemCarrito = null;
        try
        {
            itemToDelete = await carritos.GetProductoCarritoAsync(itemid);
            if (itemToDelete == null || itemToDelete.Count == 0 || itemToDelete.Count > 1)
            {
                return NotFound();
            }
            else{
                itemCarrito = itemToDelete[0];
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
        return View(itemCarrito);
    }


    [HttpPost]
    public async Task<IActionResult> ProductoCarrito(int id)
    {
        try
        {
            await carritos.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
        }
        return RedirectToAction(nameof(Eliminar), new { itemid = id, showerror = true });
    }

    [HttpPut]
    public async Task<IActionResult> Producto(int id, int cantidad)
    {
        try
        {
            await carritos.PutAsync(id, cantidad);
            return Json(new { success = true });
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Json(new { success = false, redirectUrl = Url.Action("Salir", "Auth") });
            }
        }
        return BadRequest();
    }

}
