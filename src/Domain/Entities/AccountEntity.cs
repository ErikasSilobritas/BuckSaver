namespace Domain.Entities
{
    public class AccountEntity : BaseEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; }
        public decimal? Balance { get; set; }

    }
}
