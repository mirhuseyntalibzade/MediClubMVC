namespace MediPlusMVC.Models
{
	public class Doctor : BaseEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Profession { get; set; }
        public int ExperienceYear { get; set; }
        public string Description { get; set; }
    }
}
