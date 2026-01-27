using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class RetroalimentacionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RetroalimentacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // FORMULARIO CALIFICAR
        // ===============================
        public async Task<IActionResult> Crear(int idReserva)
        {
            var reserva = await _context.Reservas
                .Include(r => r.IdPaqueteNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null)
                return NotFound();

            ViewBag.Reserva = reserva;
            return View();
        }

        // ===============================
        // GUARDAR CALIFICACIÓN
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(int idReserva, int puntuacion, string comentario)
        {
            var reserva = await _context.Reservas
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null)
                return NotFound();

            var retro = new Retroalimentacion
            {
                IdReserva = idReserva,
                Puntuacion = puntuacion,
                Comentario = comentario,
                Fecha = DateTime.Now
            };

            _context.Retroalimentacions.Add(retro);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "ReservasUsuario");
        }
    }
}
