using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Tipo_Entrega
    {
        [Key]
        public int ID_Entrega { get; set; }

        [Required(ErrorMessage = "El nombre del estado es un campo obligatorio")]
        public string? Nombre_Entrega { get; set; }
    }
}