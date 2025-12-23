using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Models;
using Turis_Travel2.Data;
using System.Linq;

namespace Turis_Travel2.Controllers
{
    public class DestinoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DestinoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Destino/Destinos
        public async Task<IActionResult> Destinos(int page = 1, int pageSize = 6)
        {
            try
            {
                // Verificar si hay datos en la tabla
                var totalDestinos = await _context.Destinos.CountAsync();

                if (totalDestinos == 0)
                {
                    // Si no hay datos, usar datos de prueba
                    return View(GetDestinosDePrueba(page, pageSize));
                }

                // Obtener todos los destinos activos (si el campo Estado existe y es 1)
                var destinosQuery = _context.Destinos
                    .Where(d => d.Estado == 1 || d.Estado == null) // Incluir también nulos por si acaso
                    .OrderBy(d => d.Nombre);

                var totalPages = (int)Math.Ceiling(totalDestinos / (double)pageSize);

                // Validar página
                page = Math.Max(1, Math.Min(page, totalPages));

                // Paginar resultados
                var destinos = await destinosQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                // Pasar datos a la vista
                ViewBag.Page = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalDestinos = totalDestinos;

                return View(destinos);
            }
            catch (Exception ex)
            {
                // En caso de error, mostrar datos de prueba
                Console.WriteLine($"Error al cargar destinos: {ex.Message}");
                return View(GetDestinosDePrueba(page, pageSize));
            }
        }

        // GET: /Destino/Detalle/5
        public async Task<IActionResult> Detalle(int id)
        {
            var destino = await _context.Destinos
                .FirstOrDefaultAsync(d => d.Id == id);

            if (destino == null)
            {
                return NotFound();
            }

            return View(destino);
        }

        // Método para obtener datos de prueba
        private List<Destino> GetDestinosDePrueba(int page = 1, int pageSize = 6)
        {
            // Datos de prueba en memoria
            var destinos = new List<Destino>
            {
                new Destino {
                    Id = 1,
                    Nombre = "Santorini, Greece",
                    Pais = "Grecia",
                    Descripcion = "Atardeceres icónicos, impresionantes vistas de la caldera y románticos pueblos blancos.",
                    Precio = 1800,
                    Categoria = "Relajación",
                    ImagenUrl = "https://images.unsplash.com/photo-1570077188670-e3a8d69ac5ff?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 2,
                    Nombre = "Kyoto, Japan",
                    Pais = "Japón",
                    Descripcion = "Explora templos antiguos, jardines serenos y distritos tradicionales de geishas.",
                    Precio = 2200,
                    Categoria = "Cultural",
                    ImagenUrl = "https://images.unsplash.com/photo-1493976040374-85c8e12f0c0e?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 3,
                    Nombre = "Machu Picchu",
                    Pais = "Perú",
                    Descripcion = "Recorre el Camino Inca para contemplar la impresionante ciudad antigua de los Andes.",
                    Precio = 2500,
                    Categoria = "Aventura",
                    ImagenUrl = "https://images.unsplash.com/photo-1526392060635-9d6019884377?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 4,
                    Nombre = "Paris, France",
                    Pais = "Francia",
                    Descripcion = "La ciudad del amor, el arte y la cultura. Un destino atemporal para todas las edades.",
                    Precio = 1500,
                    Categoria = "Familiar",
                    ImagenUrl = "https://images.unsplash.com/photo-1502602898534-4a47b6958d0f?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 5,
                    Nombre = "Bora Bora",
                    Pais = "Polinesia Francesa",
                    Descripcion = "Un verdadero paraíso con lujosos bungalows sobre el agua y lagunas de aguas cristalinas.",
                    Precio = 4500,
                    Categoria = "Relajación",
                    ImagenUrl = "https://images.unsplash.com/photo-1544551763-46a013bb70d5?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 6,
                    Nombre = "Amalfi Coast, Italy",
                    Pais = "Italia",
                    Descripcion = "Espectaculares pueblos en acantilados, aguas azules y una gastronomía italiana de primera clase.",
                    Precio = 2100,
                    Categoria = "Cultural",
                    ImagenUrl = "https://images.unsplash.com/photo-1516738901171-8eb4fc13bd20?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 7,
                    Nombre = "New York City",
                    Pais = "USA",
                    Descripcion = "La ciudad que nunca duerme, llena de rascacielos, Broadway y energía inigualable.",
                    Precio = 3200,
                    Categoria = "Cultural",
                    ImagenUrl = "https://images.unsplash.com/photo-1496442226666-8d4d0e62e6e9?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 8,
                    Nombre = "Bali, Indonesia",
                    Pais = "Indonesia",
                    Descripcion = "Playas paradisíacas, templos hindúes y una cultura espiritual única en el mundo.",
                    Precio = 1900,
                    Categoria = "Relajación",
                    ImagenUrl = "https://images.unsplash.com/photo-1537953773345-d172ccf13cf1?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                },
                new Destino {
                    Id = 9,
                    Nombre = "Patagonia, Chile",
                    Pais = "Chile",
                    Descripcion = "Naturaleza salvaje, glaciares imponentes y trekking en paisajes espectaculares.",
                    Precio = 2800,
                    Categoria = "Aventura",
                    ImagenUrl = "https://images.unsplash.com/photo-1513326738677-b964603b136d?ixlib=rb-4.0.3&auto=format&fit=crop&w=800&q=80",
                    Estado = 1
                }
            };

            // Configurar paginación
            var totalDestinos = destinos.Count;
            var totalPages = (int)Math.Ceiling(totalDestinos / (double)pageSize);

            // Validar página
            page = Math.Max(1, Math.Min(page, totalPages));

            // Paginar (simulado)
            var destinosPaginados = destinos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalDestinos = totalDestinos;

            return destinosPaginados;
        }
    }
}