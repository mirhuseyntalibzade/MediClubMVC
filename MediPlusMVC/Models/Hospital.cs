namespace MediPlusMVC.Models
{
    public class Hospital : BaseAuditableEntity
    {
        public string HospitalName { get; set; }
        public ICollection<HospitalDoctor> HospitalDoctors { get; set; }

    }
}
