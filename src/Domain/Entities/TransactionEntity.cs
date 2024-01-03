namespace Domain.Entities
{
    public class TransactionEntity : BaseEntity
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal? Fees { get; set; }

    }
}
