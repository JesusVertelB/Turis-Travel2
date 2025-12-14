using Microsoft.AspNetCore.Mvc;

namespace Turis_Travel2.Controllers
{
    public class InicioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

