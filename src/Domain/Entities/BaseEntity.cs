namespace Domain.Entities
{
    public class BaseEntity
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; } = "Erikas";
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
