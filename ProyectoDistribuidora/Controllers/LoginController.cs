using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoDistribuidora.Data;
using ProyectoDistribuidora.Models;
/*Librerias para la autentificación de formularios*/
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ProyectoDistribuidora.Controllers
{
    public class LoginController : Controller
    {
        /*Variable para manejar todas las variables del contexto*/
        private ProyectoDistribuidoraContext context;

        /// <summary>
        /// Contructor con Parametors
        /// </summary>
        /// <param name="cnt">Context</param>
        public LoginController(ProyectoDistribuidoraContext cnt)
        {
            this.context = cnt;
        }

        /// <summary>
        /// Muestra la vista de una lista con todos los usuarios registrados
        /// </summary>El administrador y gerente solo podrá ver esta lista
        /// <returns>Retorna la lista de Usuarios en una tabla</returns>
        [HttpGet]
        public async Task<IActionResult> ListaUsuarios()
        {
            return View(await context.Usuarios.ToListAsync());
        }


        /// <summary>
        /// Retorna la vista del index, este Index contiene los componentes Form, para loguear
        /// </summary>
        /// <returns>Index</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }// 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([Bind] Usuarios user)
        {
            try
            {
                var temp = this.ValidarUsuarios(user);
                /*Se pregunta si el usuario se validó correctamente*/
                if (temp != null)
                {
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,temp.Email),
                        new Claim(ClaimTypes.Name,temp.Email),
                        new Claim(ClaimTypes.Email, temp.Email),
                        new Claim(ClaimTypes.Role,temp.IdRol.ToString()),
                    };

                    var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });

                    HttpContext.SignInAsync(userPrincipal);

                    return RedirectToAction("Index","Home");
                }

                TempData["mensaje"] = "Error usuario o contraseña incorrecta";
                return View(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Retorna la vista para que el usuario puede crear su cuenta.
        /// </summary>Este método Get se utiliza en 2 ocasiones, el Adinisstrador puede crear un usuario o el mismo usuario puede llenar sus datos
        /// <returns>Vista CrearCuenta</returns>
        [HttpGet]
        public IActionResult CrearCuenta()
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
        public IActionResult CrearCuenta([Bind] Usuarios usuario)
        {
            try
            {
                /*Primero se necesita validar si el modelo es valido*/
                if (ModelState.IsValid)
                {
                    /*Si fuera valido el id rol siempre será 3 cuando se hace por logueo,genera la contraseña*/
                    usuario.IdRol = 3;
                    usuario.FechaIngreso = DateTime.Now;
                    usuario.Contrasena = this.generarClave();
                    /*Agregar el objeto*/
                    this.context.Usuarios.Add(usuario);
                    /*Guardar cambio*/
                    this.context.SaveChanges();
                    /**/
                    Email email = new Email();
                    /*Generando el cuerpo de los datos del email*/
                    string html = "Bienvenidos a la Distribuidora UCR gracias por ser parte de nuestra plataforma web";
                    html += "<br A continuación detallamos los datos registrados en nuestra plataforma web>";
                    html += "<br> <b>Cédula: </b> " + usuario.Cedula;
                    html += "<br> <b>Tipo Cédula: </b> " + usuario.TipoCedula;
                    html += "<br> <b>Nombre Completo: </b> " + usuario.NombreCompleto;
                    html += "<br> <b>Teléfono: </b> " + usuario.Telefono;
                    html += "<br> <b>Dirección: </b> " + usuario.Direccion;
                    html += "<br> <b>Email: </b>" + usuario.Email;
                    html += "<br> <b>Contraseña: </b>" + usuario.Contrasena;
                    /*Datos que se envian al método enviar de la clase Email*/
                    email.enviar(usuario.Email, @"wwwroot/css/imagenes/FirmaDistribuidora.jpg",html, "Bienvenidos a la Distribuidora UCR gracias por ser parte de nuestra plataforma web");
                   /*Mensaje que se muestra en la parte superior del form , informando que todo salió correctamente*/
                    TempData["mensaje"] = "Usuario creado correctamente. Sú contraseña se envió al correo";
                    /*Si todo sale correcto , redirecciona a Crear Cuenta*/
                    return RedirectToAction("CrearCuenta");
                }
                else
                {
                    /*Mensaje que se muestra en la parte superior del form , informando que todo salió correctamente*/
                    TempData["mensaje"] = "No se registró correctamente";
                    return View(usuario);
                }

            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error.2" + ex.Message;
                return View(usuario);
            }
         
            
        }//Fin metodo crear Cuenta

        /// <summary>
        /// Método que genera una clave de forma al azar
        /// </summary>
        /// <returns>Contrasañe</returns>
        private string generarClave()
        {
            try
            {
                Random random = new Random();
                string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";
                return new string(Enumerable.Repeat(chars, 12).Select(s => s[random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

         /*     
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await context.Usuarios
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }
        */

        /// <summary>
        /// Para editar los datos del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Cedula,TipoCedula,NombreCompleto,Telefono,Direccion,Email,FechaIngreso,Contrasena,IdRol")] Usuarios usuarios)
        {
            if (id != usuarios.Cedula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(usuarios);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.Cedula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ListaUsuarios));
            }
            return View(usuarios);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await context.Usuarios
                .FirstOrDefaultAsync(m => m.Cedula == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuarios = await context.Usuarios.FindAsync(id);
            context.Usuarios.Remove(usuarios);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(ListaUsuarios));
        }

        private bool UsuariosExists(string id)
        {
            return context.Usuarios.Any(e => e.Cedula == id);
        }

        /// <summary>
        /// Va a devolver los datos de un usuairo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       private Usuarios ValidarUsuarios(Usuarios temp)
       {
        Usuarios autorizado = null;
        /*Se valida el login*/
        var user = this.context.Usuarios.FirstOrDefault(u => u.Email == temp.Email);
            /*Se pregunta si existe el usuario*/
            if (user != null)
             {
                /*Se confirma la contraeña*/
                if (user.Contrasena.Equals(temp.Contrasena))
                {
                     autorizado = user;
                }

             }
        return autorizado;
        }//Fin método Vaidar Usuario

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

        

    }
}
