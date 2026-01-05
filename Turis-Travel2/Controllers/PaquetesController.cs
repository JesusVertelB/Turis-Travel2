using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    public class PaquetesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaquetesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 LISTADO ADMIN
        public async Task<IActionResult> Index()
        {
            var paquetes = await _context.PaquetesTuristicos
                .AsNoTracking()
                .ToListAsync();

            return View(paquetes);
        }

        // 🔹 CREATE GET
        public IActionResult Create()
        {
            CargarEstados();
            return View();
        }

        // 🔹 CREATE POST
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(
    PaquetesTuristico paquete,
    IFormFile? ImagenArchivo)
{
    if (!ModelState.IsValid)
    {
        CargarEstados();
        return View(paquete);
    }

    // 🔒 Estado por defecto
    if (string.IsNullOrEmpty(paquete.Estado))
        paquete.Estado = "Activo";

    // 📅 Fechas automáticas
    paquete.FechaCreacion = DateTime.Now;
    paquete.FechaActualizacion = DateTime.Now;

    // 📸 PRIORIDAD 1: archivo subido
    if (ImagenArchivo != null && ImagenArchivo.Length > 0)
    {
        var carpeta = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot/imagenes/paquetes"
        );

        if (!Directory.Exists(carpeta))
            Directory.CreateDirectory(carpeta);

        var nombreArchivo = Guid.NewGuid() + Path.GetExtension(ImagenArchivo.FileName);
        var ruta = Path.Combine(carpeta, nombreArchivo);

        using var stream = new FileStream(ruta, FileMode.Create);
        await ImagenArchivo.CopyToAsync(stream);

        paquete.ImagenUrl = "/imagenes/paquetes/" + nombreArchivo;
    }
    // 🌐 PRIORIDAD 2: URL escrita
    else if (!string.IsNullOrWhiteSpace(paquete.ImagenUrl))
    {
        // se deja tal cual viene del formulario
        paquete.ImagenUrl = paquete.ImagenUrl.Trim();
    }
    // ❌ PRIORIDAD 3: nada
    else
    {
        paquete.ImagenUrl = null;
    }

    _context.PaquetesTuristicos.Add(paquete);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}


        // 🔹 EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var paquete = await _context.PaquetesTuristicos.FindAsync(id);
            if (paquete == null) return NotFound();

            // Creamos strings para el formulario
            ViewBag.FechaInicioString = paquete.FechaInicio?.ToString("yyyy-MM-dd");
            ViewBag.FechaFinString = paquete.FechaFin?.ToString("yyyy-MM-dd");

            CargarEstados();
            return View(paquete);
        }


        // 🔹 EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
    int id,
    PaquetesTuristico paquete,
    IFormFile? ImagenArchivo,
    string? FechaInicioString,
    string? FechaFinString)
        {
            if (id != paquete.IdPaquete)
                return NotFound();

            if (!ModelState.IsValid)
            {
                CargarEstados();
                return View(paquete);
            }

            // Convertimos los strings a DateOnly
            if (DateOnly.TryParse(FechaInicioString, out var inicio))
                paquete.FechaInicio = inicio;

            if (DateOnly.TryParse(FechaFinString, out var fin))
                paquete.FechaFin = fin;

            paquete.FechaActualizacion = DateTime.Now;

            // Manejo de imagen
            if (ImagenArchivo != null && ImagenArchivo.Length > 0)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes/paquetes");
                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var nombreArchivo = Guid.NewGuid() + Path.GetExtension(ImagenArchivo.FileName);
                var ruta = Path.Combine(carpeta, nombreArchivo);

                using var stream = new FileStream(ruta, FileMode.Create);
                await ImagenArchivo.CopyToAsync(stream);

                paquete.ImagenUrl = "/imagenes/paquetes/" + nombreArchivo;
            }

            _context.Update(paquete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        // 🔹 DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var paquete = await _context.PaquetesTuristicos.FindAsync(id);
            if (paquete != null)
            {
                _context.PaquetesTuristicos.Remove(paquete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 DROPDOWN ESTADOS
        private void CargarEstados()
        {
            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Value = "Activo", Text = "Activo" },
                new SelectListItem { Value = "Inactivo", Text = "Inactivo" },
                new SelectListItem { Value = "Borrador", Text = "Borrador" }
            };
        }
    }
}
