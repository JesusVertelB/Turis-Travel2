using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // LISTADO
        public IActionResult Index()
        {
            var reservas = _context.Reservas
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdPaqueteNavigation)
                .OrderByDescending(r => r.FechaSolicitud)
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
            ViewBag.Clientes = _context.Usuarios.Where(u => u.IdRol == 2).ToList();
            ViewBag.Paquetes = _context.PaquetesTuristicos.ToList();
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = _context.Usuarios.Where(u => u.IdRol == 2).ToList();
                ViewBag.Paquetes = _context.PaquetesTuristicos.ToList();
                return View(reserva);
            }

            reserva.Estado = "pendiente";
            reserva.FechaSolicitud = DateTime.Now;

            _context.Reservas.Add(reserva);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var reserva = _context.Reservas.Find(id);
            if (reserva == null) return NotFound();

            ViewBag.Clientes = _context.Usuarios.Where(u => u.IdRol == 2).ToList();
            ViewBag.Paquetes = _context.PaquetesTuristicos.ToList();

            return View(reserva);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reserva reserva)
        {
            if (id != reserva.IdReserva) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.Clientes = _context.Usuarios.Where(u => u.IdRol == 2).ToList();
                ViewBag.Paquetes = _context.PaquetesTuristicos.ToList();
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
                .Include(r => r.IdClienteNavigation)
                .Include(r => r.IdPaqueteNavigation)
                .FirstOrDefault(r => r.IdReserva == id);

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
