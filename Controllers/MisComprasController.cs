using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    [Authorize(Roles = "Usuario")]
    public class MisComprasController(MisComprasClientService compras) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<MisCompra>? lista = [];
            try
            {
                lista = await compras.GetAsync();
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

        public async Task<IActionResult> Detalle(int id, string fecha)
        {
            List<MisCompraDetalle>? misCompraDetalle = null;
            try
            {
                misCompraDetalle = await compras.GetAsync(id);
                if (misCompraDetalle == null)
                {
                    return RedirectToAction("NotFoundPage", "Home");
                }
                else
                {
                    return View("Detalle", new Tuple<List<MisCompraDetalle>, string>(misCompraDetalle, fecha));
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
                misCompraDetalle = misCompraDetalle ?? new List<MisCompraDetalle>(); 
                return View("Detalle", new Tuple<List<MisCompraDetalle>, string>(misCompraDetalle, fecha));
            }
        }

    }
}