namespace Challenge_ATM_API.Entities
{
    public class Transaction : BaseEntity
    {
        public Card Card { get; set; } = default!;
        public int CardId { get; set; }
        public Guid Code { get; set; }
        public TransactionType Type { get; set; }
        public double? Amount { get; set; }
    }
    public enum TransactionType
    {
        Balance = 1,
        Withdrawal = 2,
        Deposit = 3
    }
}
