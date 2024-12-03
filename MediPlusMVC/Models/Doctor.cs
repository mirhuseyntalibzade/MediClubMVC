namespace MediPlusMVC.Models
{
	public class Doctor : BaseAuditableEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fincode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
