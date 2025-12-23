using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;

namespace Turis_Travel2.Controllers
{
    [Authorize] // o [Authorize(Roles = "Usuario")]
    public class PaquetesUsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaquetesUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTADO DE PAQUETES PARA USUARIO
        public async Task<IActionResult> Index()
        {
            var paquetes = await _context.PaquetesTuristicos
                .Where(p => p.Estado == "Activo")
                .ToListAsync();

            return View(paquetes);
        }
    }
}
