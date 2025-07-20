using System.ComponentModel.DataAnnotations;

namespace Challenge_ATM.Models
{
    public class WithdrawalModel
    {
        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0.01, 10000000, ErrorMessage = "El monto debe ser mayor a 0 y menor a 10,000,000")]
        public double Amount { get; set; }
    }
}
