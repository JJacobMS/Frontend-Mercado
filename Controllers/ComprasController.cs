using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet
{
    [Authorize(Roles = "Administrador")]
    public class ComprasController(ComprasClientService compras) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Compra>? lista = [];
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
        public async Task<IActionResult> Detalle(int id)
        {
            Compra? item = null;
            try
            {
                item = await compras.GetAsync(id);
                if (item == null)
                {
                    return RedirectToAction("NotFoundPage", "Home");
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
            return View(item);
        }
    }
}