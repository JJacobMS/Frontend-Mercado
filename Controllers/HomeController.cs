using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace frontendnet;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Error([FromServices] IHostEnvironment hostEnvironment)
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    public IActionResult NotFoundPage()
    {
        return View();
    }
}
