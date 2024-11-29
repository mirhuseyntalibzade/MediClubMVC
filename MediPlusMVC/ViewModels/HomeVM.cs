using MediPlusMVC.Models;

namespace MediPlusMVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<SliderItem> SliderItems { get; set; }
        public IEnumerable<HospitalFact> HospitalFacts { get; set; }
        public IEnumerable<Service> Services { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }   
    }
}
