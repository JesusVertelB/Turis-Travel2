using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;
using Microsoft.AspNetCore.Authorization;

namespace Turis_Travel2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDestinoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminDestinoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ===============================
        // GET: /AdminDestino
        // ===============================
        public async Task<IActionResult> Index()
        {
            var destinos = await _context.Destinos
                .OrderBy(d => d.Nombre)
                .ToListAsync();

            return View(destinos);
        }

        // ===============================
        // GET: /AdminDestino/Create
        // ===============================
        public IActionResult Create()
        {
            return View();
        }

        // ===============================
        // POST: /AdminDestino/Create
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Destino model,
            IFormFile? ImagenArchivo,
            string? ImagenUrl)
        {
            if (!ModelState.IsValid)
                return View(model);

            // 🔑 DESTINO ACTIVO (VISIBLE PARA USUARIOS)
            model.Estado = 1;

            // ---------- Imagen por archivo ----------
            if (ImagenArchivo != null && ImagenArchivo.Length > 0)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/destinos");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var fileName = Guid.NewGuid() + Path.GetExtension(ImagenArchivo.FileName);
                var filePath = Path.Combine(carpeta, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagenArchivo.CopyToAsync(stream);
                }

                model.ImagenUrl = "/images/destinos/" + fileName;
            }
            // ---------- Imagen por URL ----------
            else if (!string.IsNullOrEmpty(ImagenUrl))
            {
                model.ImagenUrl = ImagenUrl;
            }

            _context.Destinos.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // GET: /AdminDestino/Edit/5
        // ===============================
        public async Task<IActionResult> Edit(int id)
        {
            var destino = await _context.Destinos.FindAsync(id);
            if (destino == null)
                return NotFound();

            return View(destino);
        }

        // ===============================
        // POST: /AdminDestino/Edit/5
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            Destino model,
            IFormFile? ImagenArchivo,
            string? ImagenUrl)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            // ---------- Imagen por archivo ----------
            if (ImagenArchivo != null && ImagenArchivo.Length > 0)
            {
                var carpeta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/destinos");

                if (!Directory.Exists(carpeta))
                    Directory.CreateDirectory(carpeta);

                var fileName = Guid.NewGuid() + Path.GetExtension(ImagenArchivo.FileName);
                var filePath = Path.Combine(carpeta, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImagenArchivo.CopyToAsync(stream);
                }

                model.ImagenUrl = "/images/destinos/" + fileName;
            }
            // ---------- Imagen por URL ----------
            else if (!string.IsNullOrEmpty(ImagenUrl))
            {
                model.ImagenUrl = ImagenUrl;
            }

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ===============================
        // POST: /AdminDestino/Delete/5
        // (ELIMINADO LÓGICO)
        // ===============================
        public async Task<IActionResult> Delete(int id)
        {
            var destino = await _context.Destinos.FindAsync(id);
            if (destino == null)
                return NotFound();

            // ❌ NO se elimina de la BD
            destino.Estado = 0;

            _context.Update(destino);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
