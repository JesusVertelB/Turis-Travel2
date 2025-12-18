using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models.Scaffolded;

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

        // LISTADO
        public IActionResult Index()
        {
            var reservas = _context.Reservas
                .Include(r => r.ID_clienteNavigation)
                .Include(r => r.ID_paqueteNavigation)
                .OrderByDescending(r => r.Fecha_solicitud)
                .ToList();

            return View(reservas);
        }

        // CONFIRMAR
        [HttpPost]
        public IActionResult Confirmar(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "confirmada";
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // CANCELAR
        [HttpPost]
        public IActionResult Cancelar(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            reserva.Estado = "cancelada";
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            ViewBag.Clientes = _context.Usuarios.Where(u => u.ID_rol == 2).ToList();
            ViewBag.Paquetes = _context.Paquetes_Turisticos.ToList();
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = _context.Usuarios.Where(u => u.ID_rol == 2).ToList();
                ViewBag.Paquetes = _context.Paquetes_Turisticos.ToList();
                return View(reserva);
            }

            reserva.Estado = "pendiente";
            reserva.Fecha_solicitud = DateTime.Now;

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            ViewBag.Clientes = _context.Usuarios.Where(u => u.ID_rol == 2).ToList();
            ViewBag.Paquetes = _context.Paquetes_Turisticos.ToList();

            return View(reserva);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reserva reserva)
        {
            if (id != reserva.ID_reserva) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = _context.Usuarios.Where(u => u.ID_rol == 2).ToList();
                ViewBag.Paquetes = _context.Paquetes_Turisticos.ToList();
                return View(reserva);
            }

            _context.Update(reserva);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var reserva = _context.Reservas
                .Include(r => r.ID_clienteNavigation)
                .Include(r => r.ID_paqueteNavigation)
                .FirstOrDefault(r => r.ID_reserva == id);

            if (reserva == null) return NotFound();

            return View(reserva);
        }

        // DELETE (POST)
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

    }
}
