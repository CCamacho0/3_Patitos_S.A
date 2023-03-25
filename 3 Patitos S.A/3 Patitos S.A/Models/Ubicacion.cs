using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Ubicacion
    {
        [Key]
        public int Id_Ubicacion { get; set; }

        [Required(ErrorMessage = "El nombre de la ubicación es un campo obligatorio")]
        public string? Nombre_Ubicacion { get; set; }
    }
}
