using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize]
    public class HomeUsuarioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeUsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var nombre = User.FindFirstValue(ClaimTypes.Name) ?? "Usuario";
            var email = User.FindFirstValue(ClaimTypes.Email);

            var model = new HomeUsuarioViewModel
            {
                Nombre = nombre,
                Membresia = "Platinum",
                Puntos = 2450,
                DestinosGuardados = 0,
                AlertasPrecio = 0
            };

            var cliente = await _context.Clientes
                .FirstOrDefaultAsync(c => c.Email == email);

            if (cliente != null)
            {
                model.DestinosGuardados = await _context.Reservas
                    .Where(r => r.IdCliente == cliente.IdCliente)
                    .Select(r => r.IdPaquete)
                    .Distinct()
                    .CountAsync();

                model.AlertasPrecio = await _context.Notificaciones
                    .Where(n => n.Destinatario == email)
                    .CountAsync();

                var reservaActiva = await _context.Reservas
                    .Include(r => r.IdPaqueteNavigation)
                    .Include(r => r.IdItinerarioNavigation)
                    .Where(r => r.IdCliente == cliente.IdCliente && r.Estado == "confirmado")
                    .OrderByDescending(r => r.FechaSolicitud)
                    .FirstOrDefaultAsync();

                if (reservaActiva != null)
                {
                    model.DestinoActual =
                        reservaActiva.IdPaqueteNavigation?.NombrePaquete ?? "Destino";

                    model.FechasViaje =
                        $"{reservaActiva.IdItinerarioNavigation?.FechaInicio:dd MMM} - " +
                        $"{reservaActiva.IdItinerarioNavigation?.FechaFin:dd MMM yyyy}";

                    model.Viajeros =
                        $"{reservaActiva.NumeroPasajeros} pasajeros";

                    model.EstadoViaje =
                        reservaActiva.Estado;
                }
            }

            // 👇 ESTA PARTE ES LA QUE FALTABA
            model.OfertasDestacadas = await _context.Destinos
                .Where(d => d.Estado == 1)
                .Take(6)
                .ToListAsync();

            return View(model);
        }

        public async Task<IActionResult> Destinos()
        {
            var destinos = await _context.Destinos
                .Where(d => d.Estado == 1)
                .AsNoTracking()
                .ToListAsync();

            return View(destinos);
        }


    }
}
