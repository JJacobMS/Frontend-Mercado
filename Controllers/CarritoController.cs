using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace frontendnet;

//[Authorize(Roles = "Usuario")]
public class CarritoController(CarritosClientService carritos) : Controller
{
    
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> CarritoComprasPartial(){
        List<Carrito>? lista = [];
        List<CarritoProducto> listaproductos = [];
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

    [HttpDelete]
    public async Task<IActionResult> EliminarProducto(int id)
    {
        try
        {
            await carritos.DeleteAsync(id);
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

    [HttpPut]
    public async Task<IActionResult> ModificarProducto(int id, int cantidad)
    {
        try
        {
            Console.WriteLine(id);
            await carritos.PutAsync(id, cantidad);
            return Json(new { success = true });
        }
        catch (HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return Json(new { success = false, redirectUrl = Url.Action("Salir", "Auth") });
            }
            Console.WriteLine(ex.Message);
        }
        return BadRequest();
    }

}
