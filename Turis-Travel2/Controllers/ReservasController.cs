using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models.Scaffolded;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============================================================
        // INDEX
        // ============================================================
        public async Task<IActionResult> Index()
        {
            var reservas = _context.Reservas
                .Include(r => r.ID_clienteNavigation)
                .Include(r => r.ID_paqueteNavigation)
                .Include(r => r.ID_itinerarioNavigation)
                .Include(r => r.ID_transporteNavigation);

            return View(await reservas.ToListAsync());
        }

        // ============================================================
        // CREATE (GET)
        // ============================================================
        public IActionResult Create()
        {
            CargarCombos();
            return View();
        }

        // ============================================================
        // CREATE (POST)
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                reserva.Fecha_solicitud = DateTime.Now;
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CargarCombos(); // <- IMPORTANTE
            return View(reserva);
        }

        // ============================================================
        // EDIT (GET)
        // ============================================================
        public async Task<IActionResult> Edit(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.ID_clienteNavigation)
                .Include(r => r.ID_paqueteNavigation)
                .Include(r => r.ID_itinerarioNavigation)
                .Include(r => r.ID_transporteNavigation)
                .FirstOrDefaultAsync(r => r.ID_reserva == id);

            if (reserva == null)
                return NotFound();

            CargarCombos(reserva);
            return View(reserva);
        }

        // ============================================================
        // EDIT (POST)
        // ============================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reserva reserva)
        {
            if (id != reserva.ID_reserva)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            CargarCombos(reserva);
            return View(reserva);
        }

        // ============================================================
        // DELETE (GET)
        // ============================================================
        public async Task<IActionResult> Delete(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.ID_clienteNavigation)
                .Include(r => r.ID_paqueteNavigation)
                .FirstOrDefaultAsync(r => r.ID_reserva == id);

            if (reserva == null)
                return NotFound();

            return View(reserva);
        }

        // ============================================================
        // DELETE (POST)
        // ============================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                _context.Reservas.Remove(reserva);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // MÉTODO PARA CARGAR COMBOS
        // ============================================================
        private void CargarCombos(Reserva? reserva = null)
        {
            ViewData["ID_cliente"] = new SelectList(_context.Clientes, "ID_cliente", "Nombre", reserva?.ID_cliente);
            ViewData["ID_paquete"] = new SelectList(_context.Paquetes_Turisticos, "ID_paquete", "Nombre_paquete", reserva?.ID_paquete);
            ViewData["ID_itinerario"] = new SelectList(_context.Itinerarios, "ID_itinerario", "Descripcion", reserva?.ID_itinerario);
            ViewData["ID_transporte"] = new SelectList(_context.Transportes, "ID_transporte", "Tipo_transporte", reserva?.ID_transporte);
        }
    }
}
