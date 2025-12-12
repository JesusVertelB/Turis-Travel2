using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Turis_Travel2.Data;
using Turis_Travel2.Models;
using Turis_Travel2.Models.Scaffolded;

namespace Turis_Travel2.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

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

            // Buscar usuario por correo
            var usuario = await _context.Usuarios
                .Include(u => u.ID_rolNavigation)
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Estado == 1);

            if (usuario == null)
            {
                ViewBag.Error = "Credenciales inválidas.";
                return View();
            }

            // ✔ VERIFICAR CONTRASEÑA CON BCRYPT
            bool passwordValida = BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena);

            if (!passwordValida)
            {
                ViewBag.Error = "Credenciales inválidas.";
                return View();
            }

            // Crear claims
            var claims = new List<Claim>
    {
        new Claim("IdUsuario", usuario.ID_usuario.ToString()),
        new Claim(ClaimTypes.Name, usuario.Nombre_usuario),
        new Claim(ClaimTypes.Email, usuario.Correo),
        new Claim(ClaimTypes.Role, usuario.ID_rolNavigation?.Nombre_rol ?? "Cliente")
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                }
            );

            // Redirecciones según rol
            return usuario.ID_rolNavigation?.Nombre_rol == "Admin"
                ? RedirectToAction("Index", "Dashboard")
                : RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            usuario.ID_rol = 2; // Asignar rol de Cliente
            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            usuario.Estado = 1;

            _context.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

    }
}
