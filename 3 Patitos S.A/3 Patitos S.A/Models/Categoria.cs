using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        [Required(ErrorMessage ="El nombre de la categoria es un campo obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ\\s]+$", ErrorMessage = "No se aceptan valores numericos en este campo")]
        public string? Nombre_categoria { get; set;}

        [Required(ErrorMessage = "El descuento es un campo obligatorio")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Solo se aceptan valores numericos en este campo")]
        public decimal Descuento { get; set;}
    }
}
