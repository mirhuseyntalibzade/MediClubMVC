using System.ComponentModel;

namespace MediPlusMVC.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
