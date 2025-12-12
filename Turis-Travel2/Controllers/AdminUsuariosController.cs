using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Turis_Travel2.Data;
using Turis_Travel2.Models;
using Turis_Travel2.Models.Scaffolded;

namespace Turis_Travel2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminUsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminUsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

public async Task<IActionResult> Index(string search, int? rol, int? estado, int page = 1)
{
    int pageSize = 10;
    var query = _context.Usuarios.Include(u => u.ID_rolNavigation).AsQueryable();

    // FILTRO búsqueda
    if (!string.IsNullOrEmpty(search))
        query = query.Where(u =>
            u.Nombre_usuario.Contains(search) ||
            u.Correo.Contains(search));

    // FILTRO rol
    if (rol.HasValue)
        query = query.Where(u => u.ID_rol == rol.Value);

    // FILTRO estado
    if (estado.HasValue)
        query = query.Where(u => u.Estado == estado.Value);

    // PAGINACIÓN
    int totalUsuarios = await query.CountAsync();
    var usuarios = await query
        .OrderBy(u => u.Nombre_usuario)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    ViewBag.Roles = _context.Roles.ToList();
    ViewBag.EstadoActual = estado;
    ViewBag.RolActual = rol;
    ViewBag.Search = search;
    ViewBag.Page = page;
    ViewBag.TotalPages = (int)Math.Ceiling(totalUsuarios / (double)pageSize);

    return View(usuarios);
}


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
                return NotFound();

            // Llenar dropdown de roles
            ViewBag.Roles = new SelectList(_context.Roles, "ID_rol", "Nombre_rol", usuario.ID_rol);

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
        {
            if (id != usuario.ID_usuario)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = new SelectList(_context.Roles, "ID_rol", "Nombre_rol", usuario.ID_rol);
                return View(usuario);
            }

            // Traemos la entidad desde la base de datos
            var usuarioEnDb = await _context.Usuarios.FindAsync(id);
            if (usuarioEnDb == null)
                return NotFound();

            // Actualizamos solo los campos que queremos permitir
            usuarioEnDb.Nombre_usuario = usuario.Nombre_usuario;
            usuarioEnDb.Correo = usuario.Correo;
            usuarioEnDb.ID_rol = usuario.ID_rol;

            // Guardamos cambios
            await _context.SaveChangesAsync();

            // Redirigimos a la lista de usuarios
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.ID_rolNavigation)
                .FirstOrDefaultAsync(u => u.ID_usuario == id);

            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
