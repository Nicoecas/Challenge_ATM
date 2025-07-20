using System.ComponentModel.DataAnnotations;

namespace Challenge_ATM.Models
{
    public class DepositModel
    {
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0.01, 999999999.99, ErrorMessage = "El monto debe ser mayor a 0 y menor o igual a 999.999.999,99")]
        public double Amount { get; set; }
    }
}
