using MediPlusMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.DAL.Contexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<HospitalFact> HospitalFacts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SliderItem> SliderItems { get; set; }
    }
}
