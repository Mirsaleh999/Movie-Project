using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Movie.WEBUI.ViewModels;
using Movies.BLL.Services;
using Movies.BLL.Services.Interfaces;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movie.WEBUI.Controllers
{
	public class MoviesController : Controller
	{
        private readonly AppDbContext _context;
        public MoviesController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index(int PageIndex=1, int pagSize=12, HomeViewModel vmm = null)
        {
            var vm = new HomeViewModel();

            vm.FilterFormModel = vmm.FilterFormModel;

            var movies =  _context.Movies.Include(p => p.GenresCategory).Include(p => p.CountryCategory)
                .Include(p => p.Trend).Include(p => p.LanguageCategory);
            if (vm.FilterFormModel != null)
            {
                
                movies = _context.Movies.Where(p => p.GenresCategoryId == vm.FilterFormModel.GenresCategoryId && p.CountryCategoryId == vm.FilterFormModel.CountryCategoryId && p.LanguageCategoryId == vm.FilterFormModel.LanguageCategoryId).Include(p => p.GenresCategory).Include(p => p.CountryCategory)
                .Include(p => p.Trend).Include(p => p.LanguageCategory);


            }
            vm.PagedViewModel = new PagedViewModel<MovieC>(movies, PageIndex, pagSize);

            vm.GenresCategories = _context.GenresCategories.ToList();
            vm.CountryCategories = _context.CountryCategories.ToList();
            vm.LanguageCategories = _context.LanguageCategories.ToList();

            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> RateMovie(int movieId, double rating)
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            string userIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var existingRating = await _context.MovieRatings
                .FirstOrDefaultAsync(mr => mr.MovieId == movieId && mr.UserIp == userIp);

            if (existingRating != null)
            {
                _context.MovieRatings.Update(existingRating);
                existingRating.Rating = rating;
            }
            else
            {
                var movieRating = new MovieRating { MovieId = movieId, UserIp = userIp, Rating = rating };
                _context.MovieRatings.Add(movieRating);
            }

            await _context.SaveChangesAsync();

            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == movieId);
            if (movie != null)
            {
                var ratings = await _context.MovieRatings.Where(mr => mr.MovieId == movieId).ToListAsync();
                movie.RatingSum = ratings.Sum(r => r.Rating);
                movie.RatingCount = ratings.Count;
                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();
                var newAverageRating = movie.RatingCount > 0 ? movie.RatingSum / movie.RatingCount : 0;
                return Json(new { newAverageRating = newAverageRating.ToString("0.0") });
            }

            return NotFound();
        }
    }
}
