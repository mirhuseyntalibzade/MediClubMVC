using Microsoft.AspNetCore.Mvc;

namespace MediPlusMVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class SliderItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
