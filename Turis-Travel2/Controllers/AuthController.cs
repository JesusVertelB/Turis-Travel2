using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Turis_Travel2.Data;
using Turis_Travel2.Models;

namespace Turis_Travel2.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // LOGIN
        // =========================
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contrasena)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Error = "Por favor ingresa todos los campos.";
                return View();
            }

<<<<<<< HEAD
=======
            // Buscar usuario por correo y estado activo
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Estado == 1);

            if (usuario == null ||
                !BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena))
            {
                ViewBag.Error = "Credenciales inválidas.";
                return View();
            }

<<<<<<< HEAD
            string rolNombre = usuario.IdRol == 1 ? "Admin" : "Cliente";

=======
            // Verificar contraseña
            bool passwordValida = BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena);

            if (!passwordValida)
            {
                ViewBag.Error = "Credenciales inválidas.";
                return View();
            }

            // Determinar rol según IdRol
            string rolNombre = usuario.IdRol == 1 ? "Admin" : "Cliente";

            // Crear claims
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, rolNombre)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

<<<<<<< HEAD
=======
            // Redirección según rol
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
            return rolNombre == "Admin"
                ? RedirectToAction("Index", "Dashboard")
                : RedirectToAction("Index", "Home");
        }

<<<<<<< HEAD
        // =========================
        // LOGOUT
        // =========================
=======
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login", "Auth");
        }

        // =========================
        // REGISTER
        // =========================
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario usuario)
        {
<<<<<<< HEAD
=======
            // Validar correo duplicado
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
            bool existeCorreo = await _context.Usuarios
                .AnyAsync(u => u.Correo == usuario.Correo);

            if (existeCorreo)
            {
                ViewBag.Error = "Este correo ya está registrado.";
                return View(usuario);
            }

            // 1️⃣ CREAR USUARIO
            usuario.IdRol = 2; // Cliente
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            usuario.Estado = 1;
            usuario.FechaCreacion = DateTime.Now;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

<<<<<<< HEAD
            // 2️⃣ CREAR CLIENTE AUTOMÁTICO
            var cliente = new Cliente
            {
                Nombre = usuario.NombreUsuario,
                Email = usuario.Correo,
                Estado = "Activo",
                FechaRegistro = DateTime.Now
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // 3️⃣ AUTO LOGIN
=======
            // Auto login
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim("IdUsuario", usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, "Cliente")
            };

<<<<<<< HEAD
            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );
=======
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                });

            return RedirectToAction("Index", "Home");
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> Filtros, paginacion y mejoras en destinos, falta estilizar el apartado de configurar el viaje
