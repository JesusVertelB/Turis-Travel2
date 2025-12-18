using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models.Scaffolded;

namespace Turis_Travel2.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ItinerariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItinerariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: admin/itinerarios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var itinerarios = await _context.Itinerarios
                .Include(i => i.ID_paqueteNavigation) // Incluye el paquete relacionado
                .ToListAsync();

            return View(itinerarios);
        }

        // GET: admin/itinerarios/detalle/5
        [HttpGet("detalle/{id}")]
        public async Task<IActionResult> Detalle(int id)
        {
            var itinerario = await _context.Itinerarios
                .Include(i => i.ID_paqueteNavigation)
                .FirstOrDefaultAsync(i => i.ID_itinerario == id);

            if (itinerario == null) return NotFound();
            return View(itinerario);
        }

        // GET: admin/itinerarios/crear
        [HttpGet("crear")]
        public IActionResult Crear()
        {
            return View();
        }

        // POST: admin/itinerarios/crear
        [HttpPost("crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Itinerario itinerario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itinerario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itinerario);
        }

        // GET: admin/itinerarios/editar/5
        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var itinerario = await _context.Itinerarios.FindAsync(id);
            if (itinerario == null) return NotFound();
            return View(itinerario);
        }

        // POST: admin/itinerarios/editar/5
        // En tu ItinerariosController, modifica el método Editar GET:

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var itinerario = await _context.Itinerarios
                .Include(i => i.ID_paqueteNavigation)
                .FirstOrDefaultAsync(i => i.ID_itinerario == id);

            if (itinerario == null)
            {
                TempData["ErrorMessage"] = "Itinerario no encontrado";
                return RedirectToAction(nameof(Index));
            }

            // Cargar lista de paquetes para el dropdown
            ViewBag.Paquetes = await _context.Paquetes_Turisticos
                .Where(p => p.Estado == "activo" || p.Estado == "borrador")
                .Select(p => new SelectListItem
                {
                    Value = p.ID_paquete.ToString(),
                    Text = p.Nombre_paquete
                })
                .ToListAsync();

            return View(itinerario);
        }

        // GET: admin/itinerarios/eliminar/5
        [HttpGet("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var itinerario = await _context.Itinerarios.FindAsync(id);
            if (itinerario == null) return NotFound();

            _context.Itinerarios.Remove(itinerario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}