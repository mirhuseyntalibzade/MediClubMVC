namespace MediPlusMVC.Models
{
    public class HospitalDoctor : BaseAuditableEntity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
    }
}
