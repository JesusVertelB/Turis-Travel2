using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize] // 🔐 SOLO USUARIOS LOGUEADOS
    public class ReservasUsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservasUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // LISTADO DE RESERVAS DEL USUARIO
        // ===============================
        public async Task<IActionResult> Index()
        {
            var emailUsuario = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailUsuario))
            {
                return RedirectToAction("Login", "Auth");
            }

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == emailUsuario);

            if (cliente == null)
            {
                return View("SinCliente"); // vista opcional
            }

            var reservasDb = await _context.Reservas
                .Include(r => r.IdPaqueteNavigation)
                .Where(r => r.IdCliente == cliente.IdCliente)
                .OrderByDescending(r => r.FechaSolicitud)
                .ToListAsync();

            var model = new ReservasUsuarioViewModel
            {
                Nombre = User.Identity?.Name ?? "Usuario",
                ViajesProximos = reservasDb.Count(r => r.Estado == "Confirmado"),
                ViajesCompletados = reservasDb.Count(r => r.Estado == "Completado"),
                MillasTotales = reservasDb.Count * 500,

                Reservas = reservasDb.Select(r => new ReservaItemViewModel
                {
                    IdReserva = r.IdReserva, // ✅ CLAVE
                    Destino = r.IdPaqueteNavigation?.NombrePaquete ?? "Paquete",
                    Fecha = r.IdPaqueteNavigation != null
                     ? $"{r.IdPaqueteNavigation.FechaInicio:dd MMM} - {r.IdPaqueteNavigation.FechaFin:dd MMM yyyy}"
                    : "Sin fecha",
                    Precio = r.PrecioTotal ?? 0,
                    Estado = r.Estado ?? "Pendiente"
                }).ToList()

            };

            return View(model);
        }

        // ===============================
        // CREAR RESERVA (BOTÓN RESERVAR)
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(int PaqueteId, int NumeroPersonas)
        {
            var emailUsuario = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(emailUsuario))
            {
                return RedirectToAction("Login", "Auth");
            }

            // 🔎 Buscar cliente REAL
            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == emailUsuario);

            if (cliente == null)
            {
                return BadRequest("El cliente no existe");
            }

            // 🔎 Buscar paquete
            var paquete = await _context.PaquetesTuristicos
                .FirstOrDefaultAsync(p => p.IdPaquete == PaqueteId);

            if (paquete == null)
            {
                return NotFound();
            }

            // 🧾 Crear reserva
            var reserva = new Reserva
            {
                IdCliente = cliente.IdCliente,   // ✅ EXISTE EN CLIENTES
                IdPaquete = paquete.IdPaquete,
                NumeroPasajeros = NumeroPersonas,
                PrecioTotal = paquete.PrecioBase * NumeroPersonas,
                Estado = "Pendiente",
                FechaSolicitud = DateTime.Now
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            // 👉 luego puede redirigir a pagos
            return RedirectToAction("Index");
        }
    }
}
