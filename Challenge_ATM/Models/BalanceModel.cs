namespace Challenge_ATM.Models
{
    public class BalanceModel
    {
        public string Number { get; set; } = string.Empty;
        public string FormattedNumber
        {
            get
            {
                if (string.IsNullOrEmpty(Number)) return "";

                var chunks = new List<string>();
                for (int i = 0; i < Number.Length; i += 4)
                {
                    int length = Math.Min(4, Number.Length - i);
                    chunks.Add(Number.Substring(i, length));
                }
                return string.Join("-", chunks);
            }
        }
        public double Balance { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
