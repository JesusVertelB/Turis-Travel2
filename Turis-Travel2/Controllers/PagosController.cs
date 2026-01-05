using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class PagosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PagosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // FORMULARIO DE PAGO
        // URL: /Pagos/Crear?idReserva=5
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Crear(int idReserva)
        {
            if (idReserva <= 0)
                return NotFound();

            var reserva = await _context.Reservas
                .Include(r => r.IdPaqueteNavigation)
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null)
                return NotFound();

            ViewBag.ReservaId = reserva.IdReserva;
            ViewBag.Total = reserva.PrecioTotal;
            ViewBag.Paquete = reserva.IdPaqueteNavigation?.NombrePaquete ?? "Paquete";

            return View(); // 👉 Views/Pagos/Crear.cshtml
        }

        // ===============================
        // PROCESAR PAGO
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(int idReserva, string metodoPago)
        {
            if (string.IsNullOrEmpty(metodoPago))
            {
                ModelState.AddModelError("", "Debe seleccionar un método de pago.");
                return View();
            }

            var reserva = await _context.Reservas
                .FirstOrDefaultAsync(r => r.IdReserva == idReserva);

            if (reserva == null)
                return NotFound();

            // 💳 Crear pago
            var pago = new Pago
            {
                IdReserva = reserva.IdReserva,
                MetodoPago = metodoPago,
                Monto = reserva.PrecioTotal ?? 0,
                Estado = "Pagado",
                FechaPago = DateTime.Now
            };

            // ✅ Confirmar reserva
            reserva.Estado = "Confirmado";

            _context.Pagos.Add(pago);
            _context.Reservas.Update(reserva);
            await _context.SaveChangesAsync();

            // 👉 Volver a mis reservas
            return RedirectToAction("Index", "ReservasUsuario");
        }
    }
}
