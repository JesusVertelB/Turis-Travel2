using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Si el usuario está autenticado → redirige al HomeUsuarioController
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "HomeUsuario");
            }

            // Usuario NO logueado → Home público
            return View("HomePublico");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
