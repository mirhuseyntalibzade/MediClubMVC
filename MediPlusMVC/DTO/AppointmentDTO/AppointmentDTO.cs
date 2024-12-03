namespace MediPlusMVC.DTO.AppointmentDTO
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public bool isActive { get; set; }
    }
}
