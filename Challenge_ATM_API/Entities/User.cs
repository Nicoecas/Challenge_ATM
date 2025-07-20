namespace Challenge_ATM_API.Entities
{
    public class User : BaseEntity
    {
        public Bank Bank { get; set; } = default!;
        public int BankId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public required string DNI { get; set; }
        public required string CUIT { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}
