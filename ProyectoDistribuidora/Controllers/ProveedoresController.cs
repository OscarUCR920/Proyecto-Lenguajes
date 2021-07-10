using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDistribuidora.Data;
using ProyectoDistribuidora.Models;

namespace ProyectoDistribuidora.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly ProyectoDistribuidoraContext context;

        public ProveedoresController(ProyectoDistribuidoraContext cnt)
        {
            this.context = cnt;
        }

        // GET: Proveedores
        public async Task<IActionResult> ListaProveedores()
        {
            return View(await context.Proveedores.ToListAsync());
        }

        // GET: Proveedores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // GET: Proveedores/Create
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Retorna la vista para que el usuario puede crear al proveedor.
        /// </summary>
        /// <returns>Vista CrearCuenta</returns>
        [HttpGet]
        public IActionResult CrearProveedor()
        {
            return View();
        }//

        /// <summary>
        /// Método post para cargar los datos del usuario a la base de datos
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearProveedor([Bind] Proveedores proveedores)
        {
            try
            {
                /*Primero se necesita validar si el modelo es valido*/
                if (ModelState.IsValid)
                {
                    
                    proveedores.FechaIngreso = DateTime.Now;
                    /*Agregar el objeto*/
                    this.context.Proveedores.Add(proveedores);
                    /*Guardar cambio*/
                    this.context.SaveChanges();
                    TempData["mensaje"] = "Proveedor creado correctamente.";
                    /*Si todo sale correcto , redirecciona a Crear Proveedor*/
                    return RedirectToAction("CrearProveedor");
                }
                else
                {
                    /*Mensaje que se muestra en la parte superior del form , informando que todo salió correctamente*/
                    TempData["mensaje"] = "No se registró correctamente";
                    return View(proveedores);
                }

            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error.2" + ex.Message;
                return View(proveedores);
            }


        }//Fin metodo crear Cuenta


        // GET: Proveedores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await context.Proveedores.FindAsync(id);
            if (proveedores == null)
            {
                return NotFound();
            }
            return View(proveedores);
        }

        // POST: Proveedores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProveedor,CedulaProveedor,NombreComercial,Telefono,Direccion,Contacto,FechaIngreso,Email")] Proveedores proveedores)
        {
            if (id != proveedores.IdProveedor)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(proveedores);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedoresExists(proveedores.IdProveedor))
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
            return View(proveedores);
        }

        // GET: Proveedores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedores = await context.Proveedores
                .FirstOrDefaultAsync(m => m.IdProveedor == id);
            if (proveedores == null)
            {
                return NotFound();
            }

            return View(proveedores);
        }

        // POST: Proveedores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedores = await context.Proveedores.FindAsync(id);
            context.Proveedores.Remove(proveedores);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedoresExists(int id)
        {
            return context.Proveedores.Any(e => e.IdProveedor == id);
        }
    }
}
