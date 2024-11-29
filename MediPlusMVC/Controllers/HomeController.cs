using MediPlusMVC.DAL.Contexts;
using MediPlusMVC.Models;
using MediPlusMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediPlusMVC.Controllers
{
    public class HomeController : Controller
    {
        readonly AppDBContext _context;
        public HomeController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<SliderItem> sliderItems = await _context.SliderItems.ToListAsync();
            IEnumerable<HospitalFact> hospitalFacts = await _context.HospitalFacts.ToListAsync();
            IEnumerable<Service> services = await _context.Services.ToListAsync();
            IEnumerable<Blog> blogs = await _context.Blogs.ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                SliderItems = sliderItems,
                HospitalFacts = hospitalFacts,
                Services = services,
                Blogs = blogs
            };
            return View(homeVM);
        }
    }
}