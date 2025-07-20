
namespace Challenge_ATM.DTOs
{
    public class CardValidationDtoRequest
    {
        public string Number { get; set; } = "";
        public required string PIN { get; set; }
    }
}
