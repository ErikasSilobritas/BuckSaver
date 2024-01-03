namespace Domain.DTOs
{
    public class GetTransactions
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal? Fees { get; set; }
    }

    public class TopUp
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
    }

    public class Transfer
    {
        public int FromAccountId { get; set; }
        public int ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
