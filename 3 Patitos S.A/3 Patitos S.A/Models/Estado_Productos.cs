using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Estado_Productos
    {
        [Key]
        public int ID_Estado { get; set; }

        [Required(ErrorMessage = "El nombre del estado es un campo obligatorio")]
        public string? Nombre_estado { get; set; }
    }
}
