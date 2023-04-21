using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Persona
    { 
        [Key]
        [Required(ErrorMessage ="El numero de identificacion es un campo obligatorio")]
        [RegularExpression("^[1-9][0-9]{0,7}$", ErrorMessage = "El numero de cedula es invalido")]
        public int Id_Persona{ get; set;}

        public int Id_Rol { get; set; }

        [Required(ErrorMessage = "El nombre de la persona es un campo obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ\\s]+$", ErrorMessage = "No se aceptan valores numericos en este campo")]
        public string? Nombre_Persona { get; set; }

        [Required(ErrorMessage = "La direccion es un campo obligatorio")]
        [RegularExpression("^[a-zA-ZáéíóúüÁÉÍÓÚÜ\\s]+$", ErrorMessage = "No se aceptan valores numericos en este campo")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "La fecha de naciemtno es un campo obligatorio")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha_Nacimiento { get; set; }

        [Required(ErrorMessage = "El numero de telefono es un campo obligatorio")]
        [RegularExpression("^(0|[1-9][0-9]{0,7})$", ErrorMessage = "El numero de telefono es invalido")]
        public int Telefono { get; set; }

        [Required(ErrorMessage = "El correo electronico es un campo obligatorio")]
        [RegularExpression(@"^[^\s@]+@[^\s@]+\.[^\s@]+$", ErrorMessage = "El correo electrónico no es válido")]
        public string? Correo { get; set; } 

        [Required(ErrorMessage = "La contraseña es un campo obligatorio")]
        public string? Contrasena { get; set; }
        public int Id_Estado_Usuario { get; set; }
        public int Id_Categoria { get; set; }

    }
}
