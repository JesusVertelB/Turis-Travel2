using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turis_Travel2.Data;
using Turis_Travel2.Models;


namespace Turis_Travel2.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ NUEVA ACCIÓN PARA LA VISTA Inicio.cshtml
        public IActionResult Inicio()
        {
            return View();
        }

        // GET: UsuariosController
        public async Task<IActionResult> Index(int? roleId = null, int? Id = null, string? q = null)
        {
            if (User.IsInRole("Usuario") && !Id.HasValue)
            {
                var ClaimId = User.FindFirstValue("IdUsuario");
                if (ClaimId != null) Id = int.Parse(ClaimId);
            }

            var RoleList = await _context.Roles.OrderBy(r => r.NombreRol).ToListAsync();
            ViewData["Roles"] = new SelectList(_context.Roles, "IdRol", "NombreRol");
            ViewData["SelectedRole"] = roleId;

            var query = _context.Usuarios.Include(u => u.IdRolNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(u => u.NombreUsuario.Contains(q) || u.Correo.Contains(q));
            }

            if (roleId.HasValue)
            {
                query = query.Where(u => u.IdRol == roleId.Value);
            }

            if (Id.HasValue)
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.IdRolNavigation)
                    .FirstOrDefaultAsync(u => u.IdUsuario == Id.Value);

                if (usuario == null) return NotFound();

                var reservas = await _context.Reservas
                    .Include(r => r.IdPaqueteNavigation)
                    .Where(r => r.IdCliente == Id.Value)
                    .ToListAsync();

                var vmDetalle = new UsuariosViewModel
                {
                    Usuario = new List<Usuario> { usuario },
                    Reserva = reservas
                };

                return View(vmDetalle);
            }

            var usuariosList = await query.ToListAsync();

            var vmLista = new UsuariosViewModel
            {
                Usuario = usuariosList,
                Reserva = new List<Reserva>()
            };

            return View(vmLista);
        }

        // GET: UsuariosController/Details/5
        public async Task<IActionResult> ReservasUsuarios(string? q = null, int? roleId = null, int? Id = null)
        {
            if (User.IsInRole("Cliente") && !Id.HasValue)
            {
                var ClaimId = User.FindFirstValue("IdUsuario");
                if (ClaimId != null) Id = int.Parse(ClaimId);
            }

            var rolelist = await _context.Roles.OrderBy(r => r.NombreRol).ToListAsync();
            ViewData["Roles"] = new SelectList(_context.Roles, "IdRol", "NombreRol");
            ViewData["SelectedRole"] = roleId;

            var query = _context.Usuarios.Include(u => u.IdRolNavigation).AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(u => u.NombreUsuario.Contains(q) || u.Correo.Contains(q));
            }

            if (roleId.HasValue)
            {
                query = query.Where(u => u.IdRol == roleId.Value);
            }

            if (Id.HasValue)
            {
                var usuario = await _context.Usuarios
                    .Include(u => u.IdRolNavigation)
                    .FirstOrDefaultAsync(u => u.IdUsuario == Id.Value);

                if (usuario == null) return NotFound();

                var reservas = await _context.Reservas
                    .Include(r => r.IdPaqueteNavigation)
                    .Where(r => r.IdCliente == Id.Value)
                    .ToListAsync();

                var vmDetalle = new UsuariosViewModel
                {
                    Usuario = new List<Usuario> { usuario },
                    Reserva = reservas
                };

                return View(vmDetalle);
            }

            var usuariosList = await query.ToListAsync();

            var vmLista = new UsuariosViewModel
            {
                Usuario = usuariosList,
                Reserva = new List<Reserva>()
            };

            return View(vmLista);
        }

        

        public async Task<IActionResult> Miperfil()
        {
            var idClaim = User.FindFirstValue("IdUsuario");

            if (idClaim == null)
                return RedirectToAction("Login", "Auth");

            int idUsuario = int.Parse(idClaim);

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.IdUsuario == idUsuario);

            if (usuario == null)
                return NotFound();

            var reservas = await _context.Reservas
                .Include(r => r.IdPaqueteNavigation)
                .Where(r => r.IdCliente == idUsuario)
                .ToListAsync();

            var vm = new UsuariosViewModel
            {
                Usuario = new List<Usuario> { usuario },
                Reserva = reservas
            };

            return View(vm);  // <<<<< ESTO ENVÍA EL MODELO CORRECTO A Miperfil.cshtml
        }


    }
}
