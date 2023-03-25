using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Productos
    {
        [Key]
        public int ID_Producto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es un campo obligatorio")]
        public string? Nombre_producto { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es un campo obligatorio")]
        public DateTime? Fecha_ingreso { get; set; }

        [Required(ErrorMessage = "La fecha de egreso es un campo obligatorio")]
        public DateTime? Fecha_egreso { get; set; }

        [Required(ErrorMessage = "La cantidad es un campo obligatorio")]
        public int Cantidad { get; set; }

        public int ID_Estado { get; set; }

        [Required(ErrorMessage = "El precio es un campo obligatorio")]
        public decimal Precio { get; set; }

        public int Id_Ubicacion { get; set; }
    }
}
