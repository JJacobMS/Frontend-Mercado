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

    public async Task<IActionResult> Index()
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
        return View(listaproductos);
    }

    public async Task<IActionResult> Eliminar(int itemid, bool? showError = false)
    {
        List<CarritoProducto>? itemToDelete = null;
        CarritoProducto? itemCarrito = null;
        ViewBag.Url = configuration["UrlWebAPI"];
        try
        {
            itemToDelete = await carritos.GetProductoCarritoAsync(itemid);
            if (itemToDelete == null || itemToDelete.Count == 0 || itemToDelete.Count > 1)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
            else
            {
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
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }
        }
        return View(itemCarrito);
    }


    [HttpPost]
    public async Task<IActionResult> ProductoCarrito(int id)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
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

    [HttpPost]
    public async Task<IActionResult> ProductoAgregarAsync(int id, int? cantidadDisponible, int cantidad)
    {
        ViewBag.Url = configuration["UrlWebAPI"];
        if (ModelState.IsValid)
        {
            try
            {
                if (cantidad > cantidadDisponible)
                {
                    TempData["ErrorCantidad"] = "No hay suficientes productos disponibles para la compra.";
                    TempData["IdError"] = id;
                    return RedirectToAction("Index");
                }
                await carritos.PutAsync(id, cantidad);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Salir", "Auth");
                }
            }
        }
        TempData["ErrorExterno"] = "Hubo un error, por favor inténtelo más tarde";
        return RedirectToAction("Index");
    }

    public IActionResult Comprar()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RealizarCompra()
    {
        try
        {
            await carritos.PostAsyncCompra();
            return View("ResultadoCompra");
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Salir", "Auth");
            }
            else if (ex.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ViewData["ErrorMessage"] = "Sin stock suficiente para comprar el producto.";
                return View("ResultadoErrorCompra");
            }
            else if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                ViewData["ErrorMessage"] = "El carrito está vacío.";
                return View("ResultadoErrorCompra");
            }
            else
            {
                ViewData["ErrorMessage"] = "Ocurrió un error inesperado, por favor intenta nuevamente.";
                return View("ResultadoErrorCompra");
            }
        }
    }

}


