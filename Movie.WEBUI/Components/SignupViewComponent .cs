using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Data;

namespace Movie.WEBUI.Components
{
    public class SignupViewComponent : ViewComponent
    {
        readonly AppDbContext db;

        public SignupViewComponent(AppDbContext db)
        {
            this.db = db;
        }


        public Task<IViewComponentResult> InvokeAsync()
        {

            return Task.FromResult<IViewComponentResult>(View());

        }
    }
}
