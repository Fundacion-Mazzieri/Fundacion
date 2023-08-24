using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fundacion.Data;
using Fundacion.Models;
using Microsoft.AspNetCore.Authorization;

namespace Fundacion.Controllers
{
    [Authorize(Roles = "Super Admin, Admin")]
    public class UsuariosController : Controller
    {
        private readonly FundacionContext _context;

        public UsuariosController(FundacionContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        [Authorize(Roles = "Super Admin, Admin, Usuario")]
        public async Task<IActionResult> Index()
        {
            var fundacionContext = _context.Usuarios.Include(u => u.Ro);
            return View(await fundacionContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "Super Admin, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Ro)
                .FirstOrDefaultAsync(m => m.UsId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Super Admin, Admin")]
        public IActionResult Create()
        {
            ViewData["RoId"] = new SelectList(_context.Roles, "RoId", "RoDenominacion");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Super Admin, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsId,UsDni,UsApellido,UsNombre,UsDireccion,UsLocalidad,UsProvincia,UsEmail,UsTelefono,UsContrasena,RoId,UsActivo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe este usuario
                if (_context.Usuarios.Any(c => c.UsDni == usuario.UsDni))
                {
                    ModelState.AddModelError("UsDni", "Ya existe este usuario.");
                    return View(usuario);
                }
                // Encriptar la contraseña antes de guardarla
                usuario.UsContrasena = Encrypt.GetMD5(usuario.UsContrasena);

                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoId"] = new SelectList(_context.Roles, "RoId", "RoDenominacion", usuario.RoId);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Super Admin, Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["RoId"] = new SelectList(_context.Roles, "RoId", "RoDenominacion", usuario.RoId);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Super Admin, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsId,UsDni,UsApellido,UsNombre,UsDireccion,UsLocalidad,UsProvincia,UsEmail,UsTelefono,UsContrasena,RoId,UsActivo")] Usuario usuario)
        {
            if (id != usuario.UsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Encriptar la contraseña antes de guardarla
                usuario.UsContrasena = Encrypt.GetMD5(usuario.UsContrasena);
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.UsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoId"] = new SelectList(_context.Roles, "RoId", "RoDenominacion", usuario.RoId);
            return View(usuario);
        }


        // GET: Usuarios/Delete/5
        [Authorize(Roles ="Super Admin,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.Ro)
                .FirstOrDefaultAsync(m => m.UsId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Super Admin, Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'FundacionContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.UsId == id)).GetValueOrDefault();
        }
    }
}
