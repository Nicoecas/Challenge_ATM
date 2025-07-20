using System.ComponentModel.DataAnnotations;

namespace Challenge_ATM.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El número debe tener 16 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de tarjeta solo debe contener dígitos.")]
        public string Number1 { get; set; } = "";

        [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El número debe tener 16 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de tarjeta solo debe contener dígitos.")]
        public string Number2 { get; set; } = "";
        [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El número debe tener 16 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de tarjeta solo debe contener dígitos.")]
        public string Number3 { get; set; } = "";
        [Required(ErrorMessage = "El número de tarjeta es obligatorio.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "El número debe tener 16 dígitos.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El número de tarjeta solo debe contener dígitos.")]
        public string Number4 { get; set; } = "";


        [Required(ErrorMessage = "El PIN es obligario")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Error en el PIN")]
        [DataType(DataType.Password)]
        public required string PIN { get; set; }
    }
}
