using Microsoft.AspNetCore.Mvc;

namespace MediPlusMVC.Controllers
{
	public class ServicesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
