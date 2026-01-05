using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    public class DestinoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int PageSize = 6;

        public DestinoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Destino/Destinos
        public async Task<IActionResult> Destinos(int page = 1)
        {
            var totalDestinos = await _context.Destinos.CountAsync();

            // 👉 Si no hay datos reales, usar datos de prueba
            if (totalDestinos == 0)
            {
                var prueba = GetDestinosDePrueba(page);
                return View("~/Views/HomeUsuario/Destinos.cshtml", prueba);
            }

            var destinosQuery = _context.Destinos
                .Where(d => d.Estado == 1 || d.Estado == null)
                .OrderBy(d => d.Nombre);

            var totalPages = (int)Math.Ceiling(totalDestinos / (double)PageSize);
            page = Math.Clamp(page, 1, totalPages);

            var destinos = await destinosQuery
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalDestinos = totalDestinos;

            // 🔥 AQUÍ LE DECIMOS EXACTAMENTE QUÉ VISTA USAR
            return View("~/Views/HomeUsuario/Destinos.cshtml", destinos);
        }

        // GET: /Destino/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var destino = await _context.Destinos
                .FirstOrDefaultAsync(d => d.Id == id);

            if (destino == null)
                return NotFound();

            return View(destino);
        }

        // ===============================
        // DATOS DE PRUEBA
        // ===============================
        private List<Destino> GetDestinosDePrueba(int page)
        {
            var destinos = new List<Destino>
            {
                new() {
                    Id = 1,
                    Nombre = "Santorini, Greece",
                    Pais = "Grecia",
                    Descripcion = "Atardeceres icónicos.",
                    Precio = 1800,
                    Categoria = "Relajación",
                    ImagenUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff",
                    Estado = 1
                },
                new() {
                    Id = 2,
                    Nombre = "Kyoto, Japan",
                    Pais = "Japón",
                    Descripcion = "Templos antiguos.",
                    Precio = 2200,
                    Categoria = "Cultural",
                    ImagenUrl = "https://images.unsplash.com/photo-1493976040374-85c8e12f0c0e",
                    Estado = 1
                }
            };

            var totalPages = (int)Math.Ceiling(destinos.Count / (double)PageSize);
            page = Math.Clamp(page, 1, totalPages);

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalDestinos = destinos.Count;

            return destinos
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }
}
