using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class PaquetesUsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaquetesUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 LISTADO DE PAQUETES PARA EL USUARIO
        public async Task<IActionResult> Index()
        {
            var paquetes = await _context.PaquetesTuristicos
                .Where(p => p.Estado == "Activo")
                .OrderByDescending(p => p.FechaCreacion)
                .ToListAsync();

            return View(paquetes);
        }

        // 🔹 DETALLES DE UN PAQUETE
        public async Task<IActionResult> Detalles(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest("El ID del paquete no es válido.");
            }

            var paquete = await _context.PaquetesTuristicos
                .Include(p => p.IdServicios) // 👈 ESTA ES LA CLAVE
                .FirstOrDefaultAsync(p => p.IdPaquete == id);

            if (paquete == null)
            {
                return NotFound("No se encontró el paquete solicitado.");
            }

            return View(paquete);
        }

    }
}
