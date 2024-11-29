using Microsoft.AspNetCore.Mvc;

namespace MediPlusMVC.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
