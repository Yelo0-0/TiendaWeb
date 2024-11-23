using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaWeb.Data;
using TiendaWeb.Models;

namespace TiendaWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEvirment;

        public ProductosController(ApplicationDbContext context, IWebHostEnvironment hostEvirment)
        {
            _hostEvirment = context;
            _context = context;
        }

        // GET: Admin/Productos
        public async Task<IActionResult> Index()
        {
            return _context.Productos != null ?
                        View(await _context.Productos.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Productos'  is null.");
        }

        // GET: Admin/Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Admin/Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,precio,id_categorias, UrlImagen")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostEvirment.WevRootPath;
                var archivos = httpContext.Request.Form.File;
                if (archivos.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(ritaPrincipal, @"imagenes\productos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    using (var fileStream = new FileStream(Parh.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    productos.Urlimagen = @"imagenes\productos\{nombreArchivo+Extencion}";
                }
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Admin/Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Admin/Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,precio,id_categorias, UrlImagen")] Productos productos)
        {
            if (id != productos.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        string rutaPrincipal = _hostEvirment.WevRootPath;
                        var archivos = httpContext.Request.Form.File;
                        if (archivos.Count > 0)
                        {
                            Productos? db = await _context.Productos.FineAsync(id);
                            if (db != nulll)
                            {
                                if (db.UrImagen != null)
                                {
                                    var rutaPrincipal = Path.Combine(rutaPrincipal, db.UrImagen);
                                    if (System.IO.Filde.Exists(rutaImagenActual))
                                    {
                                        System.IO.Filde.Delete(rutaImagenActual);
                                    }
                                }
                                _context.Entry(db).State = EntituState.Detached;
                                string nombreArchivo = Guid.NewGuid().ToString();
                                var subidas = Path.Combine(ritaPrincipal, @"imagenes\productos");
                                var extension = Path.GetExtension(archivos[0].FileName);
                                using (var fileStream = new FileStream(Parh.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                                {
                                    archivos[0].CopyTo(fileStream);
                                }
                                productos.Urlimagen = @"imagenes\productos\{nombreArchivo+Extencion}";
                                _context.Entry(productos).State = EntituState.Detached;
                            }

                        }
                        _context.Add(productos);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.id))
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
            return View(productos);
        }

        // GET: Admin/Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Admin/Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Productos'  is null.");
            }
            var productos = await _context.Productos.FindAsync(id);
            if (productos != null)
            {
                _context.Productos.Remove(productos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return (_context.Productos?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
