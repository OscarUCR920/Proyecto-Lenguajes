using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProyectoDistribuidora.Models
{
    public class Proveedores
    {
        [Key]
        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Id Proveedor")]
        public int IdProveedor { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Cédula")]
        public string CedulaProveedor { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Nombre Comercial")]
        public string NombreComercial { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Contacto")]
        public string Contacto { get; set; }

        [Display(Name = "Fecha Ingreso")]
        public DateTime FechaIngreso { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [DataType(DataType.EmailAddress)]
        [DisplayFormat(DataFormatString = "{0:nom@gmail.com}", ApplyFormatInEditMode = true)]
        public string Email { get; set; }

    }
}
