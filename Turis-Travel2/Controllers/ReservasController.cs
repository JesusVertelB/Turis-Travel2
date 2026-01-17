using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =====================================================
        // LISTADO
        // =====================================================
        public IActionResult Index()
        {
            var reservas = _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdPaqueteNavigation)
                .Include(r => r.IdItinerarioNavigation)
                .Include(r => r.IdTransporteNavigation)
                .OrderByDescending(r => r.FechaSolicitud)
                .ToList();

            return View(reservas);
        }

        // =====================================================
        // CREATE (GET)
        // =====================================================
        public IActionResult Create()
        {
            CargarCombos();
            return View();
        }

        // =====================================================
        // CREATE (POST)  ✅ ARREGLADO
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            // 🔥 CLAVE ABSOLUTA: limpiar ModelState de navegations
            ModelState.Remove("IdClienteNavigation");
            ModelState.Remove("IdPaqueteNavigation");
            ModelState.Remove("IdItinerarioNavigation");
            ModelState.Remove("IdTransporteNavigation");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Retroalimentacions");

            if (!ModelState.IsValid)
            {
                CargarCombos(reserva);
                return View(reserva);
            }

            reserva.Estado = "pendiente";
            reserva.FechaSolicitud = DateTime.Now;

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT (GET)
        // =========================
        public IActionResult Edit(int id)
        {
            var reserva = _context.Reservas
                .AsNoTracking()
                .FirstOrDefault(r => r.IdReserva == id);

            if (reserva == null)
                return NotFound();

            CargarCombos(reserva);
            return View(reserva);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reserva reserva)
        {
            if (id != reserva.IdReserva)
                return BadRequest();

            // 🔥 QUITAR VALIDACIÓN DE NAVIGATIONS
            ModelState.Remove("IdClienteNavigation");
            ModelState.Remove("IdPaqueteNavigation");
            ModelState.Remove("IdItinerarioNavigation");
            ModelState.Remove("IdTransporteNavigation");
            ModelState.Remove("Notificaciones");
            ModelState.Remove("Retroalimentacions");

            if (!ModelState.IsValid)
            {
                CargarCombos(reserva);
                return View(reserva);
            }

            try
            {
                _context.Reservas.Update(reserva);
                _context.SaveChanges();
            }
            catch
            {
                ModelState.AddModelError("", "Error al actualizar la reserva.");
                CargarCombos(reserva);
                return View(reserva);
            }

            return RedirectToAction(nameof(Index));
        }



        // =====================================================
        // CONFIRMAR
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Confirmar(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "confirmada";
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // CANCELAR
        // =====================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cancelar(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "cancelada";
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // DELETE (GET)
        // =====================================================
        public IActionResult Delete(int id)
        {
            var reserva = _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdPaqueteNavigation)
                .Include(r => r.IdItinerarioNavigation)
                .Include(r => r.IdTransporteNavigation)
                .FirstOrDefault(r => r.IdReserva == id);

            if (reserva == null) return NotFound();

            return View(reserva);
        }

        // =====================================================
        // DELETE (POST)
        // =====================================================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            _context.Reservas.Remove(reserva);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // =====================================================
        // COMBOS
        // =====================================================
        private void CargarCombos(Reserva? reserva = null)
        {
            ViewData["IdCliente"] = new SelectList(
                _context.Clientes,
                "IdCliente",
                "Nombre",
                reserva?.IdCliente
            );

            ViewData["IdPaquete"] = new SelectList(
                _context.PaquetesTuristicos,
                "IdPaquete",
                "NombrePaquete",
                reserva?.IdPaquete
            );

            ViewData["IdItinerario"] = new SelectList(
                _context.Itinerarios,
                "IdItinerario",
                "Nombre",
                reserva?.IdItinerario
            );

            ViewData["IdTransporte"] = new SelectList(
                _context.Transportes,
                "IdTransporte",
                "Tipo",
                reserva?.IdTransporte
            );
        }
    }
}
