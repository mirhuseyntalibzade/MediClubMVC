using Microsoft.AspNetCore.Mvc;

namespace MediPlusMVC.Controllers
{
	public class BlogsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Details()
		{
			return View();
		}
	}
}
