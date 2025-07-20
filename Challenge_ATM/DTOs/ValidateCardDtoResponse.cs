namespace Challenge_ATM.DTOs
{
    public class ValidateCardDtoResponse
    {
        public bool IsVerify { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public int? Attempts { get; set; }
    }
}
