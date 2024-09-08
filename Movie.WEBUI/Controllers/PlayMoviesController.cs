using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.WEBUI.ViewModels;
using Movies.DAL.Data;
using Movies.DAL.DbModel;

namespace Movie.WEBUI.Controllers
{
    public class PlayMoviesController : Controller
	{
        private readonly AppDbContext _context;

        public PlayMoviesController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int id)
        {
            var movie = await _context.Movies.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (movie != null)
            {
                _context.Entry(movie).State = EntityState.Modified;
                movie.ViewsCount += 1;
                await _context.SaveChangesAsync();
            }

            var vm = new HomeViewModel
            {
                MovieC = movie
            };

            return View(vm);
        }



    }
}
