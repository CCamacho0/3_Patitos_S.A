using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Persona
    {
        [Key]
        [Required(ErrorMessage ="El numero de identificacion es un campo obligatorio")]
        public int Id_Persona{ get; set; }

        public int Rol { get; set; }

        [Required(ErrorMessage = "El nombre de la persona es un campo obligatorio")]
        public string? Nombre_Persona { get; set; }

        [Required(ErrorMessage = "La direccion es un campo obligatorio")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El numero de telefono es un campo obligatorio")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El correo electronico es un campo obligatorio")]
        public string? Correo { get; set; } 

        [Required(ErrorMessage = "La contraseña es un campo obligatorio")]
        public string? Contrasena { get; set; }
        public int Id_Estado_Usuario { get; set; }
        public int Id_Categoria { get; set; }
    }
}
