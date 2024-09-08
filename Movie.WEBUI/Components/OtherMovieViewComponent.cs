using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie.WEBUI.ViewModels;
using Movies.DAL.Data;
using Movies.DAL.DbModel;

namespace Movie.WEBUI.Components
{
    public class OtherMovieViewComponent : ViewComponent
    {
        readonly AppDbContext db;

        public OtherMovieViewComponent(AppDbContext db)
        {
            this.db = db;
        }


        public Task<IViewComponentResult> InvokeAsync(int PageIndex = 1, int pagSize = 12)
        {
            var vm = new HomeViewModel();

          vm.PagedViewModel  = new PagedViewModel<MovieC>(db.Movies.Include(p => p.GenresCategory).Include(p => p.CountryCategory).Include(p => p.Trend).Include(p => p.LanguageCategory), PageIndex, pagSize);
            return Task.FromResult<IViewComponentResult>(View(vm));

        }
    }
}
