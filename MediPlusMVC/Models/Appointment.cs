namespace MediPlusMVC.Models
{
    public class Appointment : BaseAuditableEntity
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
