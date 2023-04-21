using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Ubicacion
    {
        [Key]
        public int Id_Ubicacion { get; set; }

        [Required(ErrorMessage = "El nombre de la ubicación es un campo obligatorio")]
        [RegularExpression("^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "No se aceptan unicamente valores numericos en este campo")]
        public string? Nombre_Ubicacion { get; set; }
    }
}
