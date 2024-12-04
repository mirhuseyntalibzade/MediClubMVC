using MediPlusMVC.Models;

namespace MediPlusMVC.DTO.HospitalDTO
{
    public class HospitalDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string HospitalName { get; set; }
        public bool isActive { get; set; }
        public List<int> DoctorIds { get; set; }
    }
}
