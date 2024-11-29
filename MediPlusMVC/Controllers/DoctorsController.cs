using Microsoft.AspNetCore.Mvc;

namespace MediPlusMVC.Controllers
{
	public class DoctorsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
