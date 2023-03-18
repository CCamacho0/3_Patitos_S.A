using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Rol
    {
        [Key]
        public int Id_Rol { get; set; }

        [Required(ErrorMessage ="El nombre del rol es un campo obligatorio")]
        public string? Nombre_Rol { get; set; }
    }
}
