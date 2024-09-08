using Movie.WEBUI.wwwroot.ViewModels;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;

namespace Movie.WEBUI.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Trends>? Trends { get; set; }
        public IEnumerable<GenresCategory>? GenresCategories { get; set; }
        public IEnumerable<CountryCategory>? CountryCategories { get; set; }
        public IEnumerable<LanguageCategory>? LanguageCategories { get; set; }
        public SignInViewModel SignInViewModel { get; set; }
        public MovieC MovieC { get; set; }
        public SignUpViewModel? SignUpViewModel { get; set; }
        public ProfileViewModel? ProfileViewModel { get; set; }
        public PagedViewModel<MovieC> PagedViewModel { get; set; }
        public IEnumerable<AboutDto>? Abouts { get; set; }

        public FilterFormModel FilterFormModel { get; set; }
    }
}
