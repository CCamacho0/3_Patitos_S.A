using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Estado_Usuario
    {
        [Key]
        public int Id_estado_usuario { get; set; }

        [Required(ErrorMessage ="E: nombre del estado es un campo obligatorio")]
        public string? Nombre_estado {get; set; }

    }
}
