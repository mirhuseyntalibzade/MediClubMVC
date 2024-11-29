namespace MediPlusMVC.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Description { get; set; }
    }
}
