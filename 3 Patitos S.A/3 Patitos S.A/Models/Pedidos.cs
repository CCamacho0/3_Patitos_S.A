using System.ComponentModel.DataAnnotations;

namespace _3_Patitos_S.A.Models
{
    public class Pedidos
    {
        [Key]
        public int Id_pedido { get; set; }

		public int ID_Producto { get; set; }

        public int ID_Entrega { get; set; }

        public int ID_Persona { get; set; }

        [Required(ErrorMessage = "La cantidad es un campo obligatorio")]
        [RegularExpression("^[1-9][0-9]*$+", ErrorMessage = "La cantidad debe ser mayor de 0")]
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public int ID_Estado_Pedido { get; set; }
    }
}
