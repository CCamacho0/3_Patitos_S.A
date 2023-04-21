using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3_Patitos_S.A.Models
{
    public class Productos
    {
        [Key]
        [Required(ErrorMessage = "El codigo del producto es un campo obligatorio")]
        public int ID_Producto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es un campo obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ\\s]+$", ErrorMessage = "No se aceptan valores numericos en este campo")]
        public string? Nombre_producto { get; set; }

        [Required(ErrorMessage = "La cantidad es un campo obligatorio")]
        [RegularExpression("^[1-9][0-9]*$+", ErrorMessage = "La cantidad debe ser mayor de 0")]
        public int Cantidad { get; set; }

        public int ID_Estado { get; set; }

        [Required(ErrorMessage = "El precio es un campo obligatorio")] 
        [RegularExpression("^[0-9]+(\\.[0-9]+)?$", ErrorMessage = "El precio debe ser positivo y tener decimales")]
        public decimal Precio { get; set; }
        public byte[]? Imagen { get; set; }

        [Required(ErrorMessage = "La descripción es un campo obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ\\s]+$", ErrorMessage = "No se aceptan valores numericos en este campo")]
        public string? Detalles { get; set; }
        public int Id_Ubicacion { get; set; }

        [NotMapped]
        public IFormFile? Img { get; set; }
    }
}
