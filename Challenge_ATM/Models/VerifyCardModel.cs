using System.ComponentModel.DataAnnotations;

namespace Challenge_ATM.Models
{
    public class VerifyCardModel
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

    }
}
