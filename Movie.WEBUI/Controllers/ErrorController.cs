using Microsoft.AspNetCore.Mvc;

namespace Movie.WEBUI.Controllers
{
	public class ErrorController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
