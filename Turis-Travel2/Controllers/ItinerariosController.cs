using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [Route("admin/itinerarios")]
    public class ItinerariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItinerariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================== LISTADO ==================
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var itinerarios = await _context.Itinerarios
                .Include(i => i.IdPaqueteNavigation)
                .ToListAsync();

            return View(itinerarios);
        }

        // ================== CREAR ==================
        [HttpGet("crear")]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Paquetes = await _context.PaquetesTuristicos
                .Select(p => new SelectListItem
                {
                    Value = p.IdPaquete.ToString(),
                    Text = p.NombrePaquete
                })
                .ToListAsync();

            return View();
        }

        [HttpPost("crear")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Itinerario itinerario)
        {
            if (ModelState.IsValid)
            {
                itinerario.Estado = "Activo";
                _context.Itinerarios.Add(itinerario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Paquetes = await _context.PaquetesTuristicos
                .Select(p => new SelectListItem
                {
                    Value = p.IdPaquete.ToString(),
                    Text = p.NombrePaquete
                })
                .ToListAsync();

            return View(itinerario);
        }

        // ================== EDITAR ==================
        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Editar(int id)
        {
            var itinerario = await _context.Itinerarios
                .Include(i => i.IdPaqueteNavigation)
                .FirstOrDefaultAsync(i => i.IdItinerario == id);

            if (itinerario == null) return NotFound();

            ViewBag.Paquetes = await _context.PaquetesTuristicos
                .Select(p => new SelectListItem
                {
                    Value = p.IdPaquete.ToString(),
                    Text = p.NombrePaquete
                })
                .ToListAsync();

            return View(itinerario);
        }

        [HttpPost("editar/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Itinerario itinerario)
        {
            if (id != itinerario.IdItinerario) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(itinerario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(itinerario);
        }

        // ================== ACTIVAR / DESACTIVAR ==================
        [HttpGet("estado/{id}")]
        public async Task<IActionResult> CambiarEstado(int id)
        {
            var itin = await _context.Itinerarios.FindAsync(id);
            if (itin == null) return NotFound();

            itin.Estado = itin.Estado == "Activo" ? "Inactivo" : "Activo";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================== ELIMINAR ==================
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
