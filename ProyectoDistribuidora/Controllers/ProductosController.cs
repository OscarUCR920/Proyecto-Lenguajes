using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDistribuidora.Data;
using ProyectoDistribuidora.Models;

namespace ProyectoDistribuidora.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProyectoDistribuidoraContext _context;

        public ProductosController(ProyectoDistribuidoraContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }// 

        // GET: Productos
        public async Task<IActionResult> ListaProductos()
        {
            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.CodigoBarra == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> files, [Bind("CodigoBarra,Tipo,Descripcion,PrecioCompra,ImpuestoValor,Exento,PrecioTotalProducto,UnidadMedida,Estado,Foto,Fecha,IdProveedor")] Productos productos)
        {
            if (ModelState.IsValid)
            {
                string rutaFisica = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = rutaFisica.Substring(0, rutaFisica.Length - 24);
                filePath += @"wwwroot\css\imagenes\";
                string fileName = "";

                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        fileName = productos.CodigoBarra + "-" + formFile.FileName;
                        fileName = fileName.Replace(" ", "_");
                        filePath += fileName;

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                            productos.Foto = "/css/imagenes/" + fileName;
                        }//fin del using
                    }//fin del if formFile
                }//fin del foreach

                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }//fin del if modelstate
            return View(productos);
        }//fin del método create

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
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

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CodigoBarra,Tipo,Descripcion,PrecioCompra,ImpuestoValor,Exento,PrecioTotalProducto,UnidadMedida,Estado,Foto,Fecha,IdProveedor")] Productos productos)
        {
            if (id != productos.CodigoBarra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.CodigoBarra))
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

        // GET: Productos/Delete/
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.CodigoBarra == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(string id)
        {
            return _context.Productos.Any(e => e.CodigoBarra == id);
        }
    }
}
