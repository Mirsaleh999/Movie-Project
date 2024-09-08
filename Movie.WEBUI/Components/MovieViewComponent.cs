using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;

namespace Movie.WEBUI.Components
{
    public class MovieViewComponent : ViewComponent
    {
        readonly AppDbContext db;
      
        public MovieViewComponent(AppDbContext db)
        {
         this.db = db;
        }


        public Task<IViewComponentResult> InvokeAsync(int trendId, int categoryId, bool top =false,bool isneww=false)
        {
            var movies = db.Movies.Include(p=>p.GenresCategory)
                .Include(p => p.CountryCategory)
                .Include(p=>p.Trend)
                .Include(p => p.LanguageCategory).AsNoTracking().ToList();

            if(trendId > 0)
            {
                movies = movies.Where(p=>p.TrendId== trendId).ToList();
            }
            if (isneww == true)
            {
                movies = movies.Where(p => p.isneww == true).ToList();
                ViewBag.isneww = "true";
            }

            if (categoryId > 0)
            {
                movies = movies.Where(p => p.GenresCategoryId == categoryId).Take(5).ToList();
            }
            if(top == true)
            {
               movies = movies.Where(p => p.Top == true).ToList();
            }

            return Task.FromResult<IViewComponentResult>(View(movies));

        }
    }
}
