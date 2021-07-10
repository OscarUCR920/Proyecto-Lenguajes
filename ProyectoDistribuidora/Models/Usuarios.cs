using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDistribuidora.Models
{
    public class Usuarios
    {
        [Key]
        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Tipo Cédula")]
        public string TipoCedula { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Nombre")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [DataType(DataType.EmailAddress)]
        [DisplayFormat(DataFormatString = "{0:nom@gmail.com}", ApplyFormatInEditMode = true)]
        public string Email { get; set; }

        [Display(Name = "Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Display(Name = "Contraseña")]
        public string Contrasena { get; set; }

        [Display(Name = "Rol")]
        public int IdRol { get; set; }
    }
}
