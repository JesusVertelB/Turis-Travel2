using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PermisosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermisosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR PERMISOS
        public IActionResult Index()
        {
            var permisos = _context.Permisos
                .Include(p => p.IdRolNavigation)
                .Include(p => p.IdModuloNavigation)
                .Where(p => p.EstadoPermiso == 1)  // Solo activos
                .ToList();

            return View(permisos);
        }

        // CREATE GET
        public IActionResult Create()
        {
            ViewBag.Modulos = _context.Modulos.ToList();
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Permiso permiso)
        {
            if (!ModelState.IsValid)
                return View(permiso);

            permiso.EstadoPermiso = 1;  // Activo por defecto

            _context.Permisos.Add(permiso);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var permiso = _context.Permisos
                .Include(p => p.IdRolNavigation)
                .Include(p => p.IdModuloNavigation)
                .FirstOrDefault(p => p.IdPermiso == id);

            if (permiso == null)
                return NotFound();

            ViewBag.Modulos = _context.Modulos.ToList();
            ViewBag.Roles = _context.Roles.ToList();

            return View(permiso);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Permiso permiso)
        {
            if (id != permiso.IdPermiso)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(permiso);

            _context.Update(permiso);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var permiso = _context.Permisos.Find(id);
            if (permiso == null)
                return NotFound();

            return View(permiso);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var permiso = _context.Permisos.Find(id);
            if (permiso == null)
                return NotFound();

            permiso.EstadoPermiso = 0;  // Desactivar el permiso
            _context.Update(permiso);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        // Guardar Permisos POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult GuardarPermisos(List<int> permisosSeleccionados)
        {
            var permisos = _context.Permisos.ToList();

            foreach (var permiso in permisos)
            {
                if (permisosSeleccionados != null &&
                    permisosSeleccionados.Contains(permiso.IdPermiso))
                {
                    permiso.EstadoPermiso = 1;
                }
                else
                {
                    permiso.EstadoPermiso = 0;
                }
            }

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
