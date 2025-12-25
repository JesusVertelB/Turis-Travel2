using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Añade este using
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaquetesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PaquetesController
        public async Task<IActionResult> Index()
        {
            var paquetes = await _context.PaquetesTuristicos
                .Include(p => p.IdServicios)
                .AsNoTracking()
                .ToListAsync();

            return View(paquetes);
        }

        // GET: PaquetesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paquete = await _context.PaquetesTuristicos
                .FirstOrDefaultAsync(p => p.IdPaquete == id);

            if (paquete == null)
            {
                return NotFound();
            }

            return View(paquete);
        }

        // GET: PaquetesController/Create
        public IActionResult Create()
        {
            // CORREGIDO: Usar SelectListItem correctamente
            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" },
                new SelectListItem { Value = "Borrador", Text = "Borrador" }
            };

            return View();
        }

        // POST: PaquetesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaquetesTuristico paquete)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(paquete);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    // Si hay error, recargar los estados
                    CargarEstados();
                    return View(paquete);
                }
            }

            // Si el modelo no es válido, recargar los estados
            CargarEstados();
            return View(paquete);
        }

        // GET: PaquetesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var paquete = await _context.PaquetesTuristicos.FindAsync(id);

            if (paquete == null)
            {
                return NotFound();
            }

            CargarEstados();
            return View(paquete);
        }

        // POST: PaquetesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PaquetesTuristico paquete)
        {
            if (id != paquete.IdPaquete)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paquete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaqueteExists(paquete.IdPaquete))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            CargarEstados();
            return View(paquete);
        }

        // POST: PaquetesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paquete = await _context.PaquetesTuristicos.FindAsync(id);
            if (paquete != null)
            {
                _context.PaquetesTuristicos.Remove(paquete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PaqueteExists(int id)
        {
            return _context.PaquetesTuristicos.Any(e => e.IdPaquete == id);
        }

        // Método privado para cargar los estados (reutilizable)
        private void CargarEstados()
        {
            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" },
                new SelectListItem { Value = "Borrador", Text = "Borrador" }
            };
        }
    }
}