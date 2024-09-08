using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.WEBUI.Models;
using Movie.WEBUI.ViewModels;
using Movies.DAL.Data;
using System.Diagnostics;

namespace Movie.WEBUI.Controllers
{
	public class HomeController : Controller
	{
		readonly AppDbContext db;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, AppDbContext db)
		{
			_logger = logger;
			this.db = db;
		}

		public IActionResult Index()
		{
			var vm = new HomeViewModel();
			//vm.Trends = db.Trends.ToList();
			vm.GenresCategories= db.GenresCategories.ToList();	
			return View(vm);
		}

        public IActionResult AccessDenied()
        {

            return View();
        }

        public IActionResult Privacy()
		{
			return View();
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}