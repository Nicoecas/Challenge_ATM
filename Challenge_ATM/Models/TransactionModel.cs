namespace Challenge_ATM.Models
{
    public class TransactionModel
    {
        public int CardId { get; set; }
        public Guid Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
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
