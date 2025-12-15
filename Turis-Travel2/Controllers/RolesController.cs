using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Turis_Travel2.Data;
using Turis_Travel2.Models.Scaffolded;

namespace Turis_Travel2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTAR ROLES
        public IActionResult Index()
        {
            var roles = _context.Roles
                .Where(r => r.Estado_rol == 1)
                .ToList();

            return View(roles);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (!ModelState.IsValid)
                return View(role);

            role.Estado_rol = 1;

            _context.Roles.Add(role);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return NotFound();

            return View(role);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Role role)
        {
            if (id != role.ID_rol)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(role);

            _context.Update(role);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // DELETE LOGICO
        public IActionResult Delete(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return NotFound();

            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var role = _context.Roles.Find(id);
            if (role == null) return NotFound();

            role.Estado_rol = 0;
            _context.Update(role);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
