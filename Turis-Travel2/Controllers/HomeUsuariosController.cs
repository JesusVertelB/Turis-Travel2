using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turis_Travel2.Models;
using System.Security.Claims;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class HomeUsuarioController : Controller
    {
        public IActionResult Index()
        {
            var nombre = User.FindFirstValue(ClaimTypes.Name) ?? "Usuario";

            var model = new HomeUsuarioViewModel
            {
                Nombre = nombre,
                Membresia = "Platinum",
                Puntos = 2450,
                DestinosGuardados = 12,
                AlertasPrecio = 3,
                DestinoActual = "Cancún, México",
                FechasViaje = "15 Oct - 22 Oct, 2025",
                Viajeros = "2 Adultos",
                EstadoViaje = "Confirmado"
            };

            return View(model);
        }
    }
}



