using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Turis_Travel2.Data;

namespace Turis_Travel2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //   ===== CARDS PRINCIPALES =====
            ViewBag.TotalUsuarios = _context.Usuarios.Count();
            ViewBag.TotalClientes = _context.Clientes.Count();
            ViewBag.ReservasActivas = _context.Reservas.Count(r => r.Estado == "activa" || r.Estado == "pendiente" || r.Estado == "confirmada");
            ViewBag.TotalPaquetes = _context.Paquetes_Turisticos.Count();

            //   ===== DESTINOS POPULARES =====
            var destinos = _context.Reservas
                .Where(r => r.ID_paquete != null)
                .GroupBy(r => r.ID_paqueteNavigation.Nombre_paquete)
                .Select(g => new
                {
                    Destino = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToList();

            ViewBag.DestinosLabels = destinos.Select(x => x.Destino).ToList();
            ViewBag.DestinosData = destinos.Select(x => x.Cantidad).ToList();


            //   ===== NUEVOS CLIENTES (últimas 4 semanas) =====
            var hoy = DateTime.Now;

            var clientesPorSemana = new int[4];

            for (int i = 0; i < 4; i++)
            {
                var inicio = hoy.AddDays(-(7 * (i + 1)));
                var fin = hoy.AddDays(-(7 * i));

                clientesPorSemana[3 - i] = _context.Clientes
                    .Count(c => c.Fecha_registro >= inicio && c.Fecha_registro < fin);
            }

            ViewBag.NuevosClientesLabels = new[] { "Semana 1", "Semana 2", "Semana 3", "Semana 4" };
            ViewBag.NuevosClientesData = clientesPorSemana;

            return View();
        }
    }
}
