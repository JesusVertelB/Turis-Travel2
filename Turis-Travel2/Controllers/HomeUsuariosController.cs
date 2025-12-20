using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turis_Travel2.Models;
using System.Security.Claims;
using Turis_Travel2.Data;
using Microsoft.EntityFrameworkCore;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class HomeUsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }
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

        public async Task<IActionResult> Destinos(int page = 1, int pageSize= 10)
        {

            var total = _context.Destinos.Count();

            var destinos = await _context.Destinos
                .OrderByDescending(d => d.Id)
                .Skip((page - 1)* pageSize )
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);

            return View(destinos);
        }
    }
}



