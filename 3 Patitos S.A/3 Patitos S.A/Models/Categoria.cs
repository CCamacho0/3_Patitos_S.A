using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Categoria
    {
        [Key]
        public int Id_categoria { get; set; }

        [Required(ErrorMessage ="El nombre de la categoria es un campo obligatorio")]
        public string? Nombre_categoria { get; set;}

        [Required(ErrorMessage = "El descuento es un campo obligatorio")]
        public int Descuento { get; set;}
    }
}
