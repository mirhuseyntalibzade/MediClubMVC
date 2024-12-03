namespace MediPlusMVC.DTO.DoctorDTO
{
    public class DoctorDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fincode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; }
    }
}
