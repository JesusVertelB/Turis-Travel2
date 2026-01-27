using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turis_Travel2.Data;
using Turis_Travel2.Models;
using Turis_Travel2.Models.ViewModels;


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

        public async Task<IActionResult> Destinos(
            int page = 1,
            string? categoria = null,
            string? orden = null)
        {
            int pageSize = 6;

            // 🔹 Query base
            var query = _context.Destinos
                .Where(d => d.Estado == 1)
                .AsQueryable();

            // 🔹 Filtro por categoría
            if (!string.IsNullOrEmpty(categoria))
                query = query.Where(d => d.Categoria == categoria);

            // 🔹 Ordenamiento
            query = orden switch
            {
                "precio_asc" => query.OrderBy(d => d.Precio),
                "precio_desc" => query.OrderByDescending(d => d.Precio),
                _ => query.OrderBy(d => d.Id)
            };

            // 🔹 Total con filtros aplicados
            var totalDestinos = await query.CountAsync();

            // 🔹 Paginación
            var destinos = await query
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // 🔹 ViewBag
            ViewBag.Page = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalDestinos / (double)pageSize);
            ViewBag.Categoria = categoria;
            ViewBag.Orden = orden;

            return View(destinos);
        }


        [HttpGet]
        public async Task<IActionResult> ConfigurarViaje(int id)
        {
            var destino = await _context.Destinos
                .FirstOrDefaultAsync(d => d.Id == id);

            if (destino == null)
                return NotFound();

            var vm = new ConfigurarViajeViewModel
            {
                IdDestino = destino.Id,
                NombreDestino = destino.Nombre,
                PrecioBase = destino.Precio,
                FechaSalida = DateTime.Today,
                CantidadAdultos = 1,
                CantidadNinos = 0
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> DetalleDestino(int id)
        {
            var destino = await _context.Destinos
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id && d.Estado == 1);

            if (destino == null)
                return NotFound();

            var paquetes = await _context.PaquetesTuristicos
                .AsNoTracking()
                .Where(p => p.Estado == "Activo" && p.DestinoId == id) // Solo muestra el paquete del destino
                .ToListAsync();

            // Si no hay paquetes para este destino, mostrar los más populares
            if (!paquetes.Any())
            {
                paquetes = await _context.PaquetesTuristicos
                    .AsNoTracking()
                    .Where(p => p.Estado == "Activo")
                    .OrderByDescending(p => p.PrecioBase) // o alguna lógica de popularidad
                    .Take(3)
                    .ToListAsync();
            }

            // ⭐ Calificaciones basadas en reservas de esos paquetes
            var paqueteIds = paquetes.Select(p => p.IdPaquete).ToList();

            var calificaciones = await _context.Retroalimentacions
                .Include(r => r.IdReservaNavigation)
                .Where(r =>
                    r.IdReservaNavigation.IdPaquete != null &&
                    paqueteIds.Contains(r.IdReservaNavigation.IdPaquete.Value))
                .ToListAsync();

            double promedio = calificaciones.Any()
                ? calificaciones.Average(r => (double)r.Puntuacion)
                : 0;

            ViewBag.PromedioCalificacion = Math.Round(promedio, 1);
            ViewBag.TotalOpiniones = calificaciones.Count;

            // ✅ Comentarios filtrados por paquetes de este destino
            var comentarios = await _context.Retroalimentacions
                .Include(r => r.IdReservaNavigation)
                .Where(r => r.IdReservaNavigation.IdPaquete != null && paqueteIds.Contains(r.IdReservaNavigation.IdPaquete.Value))
                .OrderByDescending(r => r.IdRetroalimentacion)
                .ToListAsync();

            var vm = new DestinosPaquetesViewModel
            {
                Destino = destino,
                Paquetes = paquetes,
                Comentarios = comentarios // <-- Aquí los añadimos al ViewModel
            };

            return View(vm);
        }

    }
}
