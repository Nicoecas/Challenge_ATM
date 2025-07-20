namespace Challenge_ATM_API.Entities
{
    public class Card : BaseEntity
    {
        public User User { get; set; } = default!;
        public int UserId { get; set; }
        public required string Number { get; set; }
        public bool IsLocked { get; set; }
        public required string PIN { get; set; }
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Attempts { get; set; } = 0;
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
