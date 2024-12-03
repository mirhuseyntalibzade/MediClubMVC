namespace MediPlusMVC.Models
{
    public class BaseAuditableEntity : BaseEntity
    {
        public bool isActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
