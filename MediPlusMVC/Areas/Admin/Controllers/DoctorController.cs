using Microsoft.AspNetCore.Mvc;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class DoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
