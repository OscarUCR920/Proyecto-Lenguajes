using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoDistribuidora.Models;

namespace ProyectoDistribuidora.Data
{
    public class ProyectoDistribuidoraContext : DbContext
    {
        /// <summary>s
        /// Contructor con parámetros , recibe el contecto para interactuar con la base de datos
        /// </summary>
        /// <param name="options"></param>
        public ProyectoDistribuidoraContext (DbContextOptions<ProyectoDistribuidoraContext> options)
            : base(options)
        {
        }

        //Contexto para manejar las referencias de los usuarios
        public DbSet<ProyectoDistribuidora.Models.Usuarios> Usuarios { get; set; }

        public DbSet<ProyectoDistribuidora.Models.Productos> Productos { get; set; }

        public DbSet<ProyectoDistribuidora.Models.Proveedores> Proveedores { get; set; }
    }
}
