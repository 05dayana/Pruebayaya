using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pruebayaya.Models;

namespace Pruebayaya.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly dbcontex _context;

        public CategoriasController(dbcontex context)
        {
            _context = context;
        }

        // GET: Categorias
        public async Task<IActionResult> Index()
        {
              return _context.Categorias != null ? 
                          View(await _context.Categorias.ToListAsync()) :
                          Problem("Entity set 'dbcontex.Categorias'  is null.");
        }

        // GET: Categorias/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(m => m.id == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        // GET: Categorias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categorias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nombre,descripcion,id,activo,creadoDate")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                // Ejecutar el procedimiento almacenado directamente
                var nombreParam = new SqlParameter("@nombre", categorias.nombre);
                var descripcionParam = new SqlParameter("@descripcion", categorias.descripcion);
                var activoParam = new SqlParameter("@activo", categorias.activo);
                var creadoDateParam = new SqlParameter("@creado", DateTime.Now);

                // Ejecutar el procedimiento almacenado con los parámetros
                await _context.Database.ExecuteSqlRawAsync("EXEC insertar @nombre, @descripcion, @activo, @creado",
                    nombreParam, descripcionParam, activoParam, creadoDateParam);

                return RedirectToAction(nameof(Index));
                }
                return View(categorias);
        }

        // GET: Categorias/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias == null)
            {
                return NotFound();
            }
            return View(categorias);
        }

        // POST: Categorias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("nombre,descripcion,id,activo,creadoDate")] Categorias categorias)
        {
            if (id != categorias.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var idc = new SqlParameter("@id", categorias.id);
                    var nombreParam = new SqlParameter("@nombre", categorias.nombre);
                    var descripcionParam = new SqlParameter("@descripcion", categorias.descripcion);
                    var activoParam = new SqlParameter("activo", categorias.activo);
                    var creado = new SqlParameter("creado", DateTime.Now);

                    // Ejecutar el procedimiento almacenado con los parámetros
                    await _context.Database.ExecuteSqlRawAsync("EXEC editar  @id,@nombre, @descripcion, @activo, @creado",
                       idc, nombreParam, descripcionParam, activoParam, creado);
                    //////_context.Update(categorias);
                    //////await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriasExists(categorias.id))
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
            return View(categorias);
        }

        // GET: Categorias/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Categorias == null)
            {
                return NotFound();
            }

            var categorias = await _context.Categorias
                .FirstOrDefaultAsync(m => m.id == id);
            if (categorias == null)
            {
                return NotFound();
            }

            return View(categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Categorias == null)
            {
                return Problem("Entity set 'dbcontex.Categorias'  is null.");
            }
            var categorias = await _context.Categorias.FindAsync(id);
            if (categorias != null)
            {
                _context.Categorias.Remove(categorias);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriasExists(long id)
        {
          return (_context.Categorias?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
