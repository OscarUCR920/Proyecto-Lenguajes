using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoDistribuidora.Models
{
    public class Productos
    {
        [Key]
        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Código Barra")]
        public string CodigoBarra { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Precio Compra")]
        public int PrecioCompra { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Impuesto")]
        public double ImpuestoValor { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Exento")]
        public double Exento { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Precio Total")]
        public double PrecioTotalProducto { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Unidad Medida")]
        public string UnidadMedida { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        //comento la siguiente linea de código, si se deja no permite agregar las fotos
        //[Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Display(Name = "Fecha Ingreso")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "No se permite campos vacíos")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

    }
}
