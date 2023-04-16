using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Estado_pedido
    {
        [Key]
        public int ID_Estado_Pedido { get; set; }

        [Required(ErrorMessage = "El nombre del estado es un campo obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ\\s]+$", ErrorMessage = "No se aceptan valores numericos en este campo")]
        public string? Nombre_Estado_Pedido { get; set;}
    }
}
