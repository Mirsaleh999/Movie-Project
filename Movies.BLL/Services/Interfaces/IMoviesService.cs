using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.BLL.Services.Interfaces
{
    public interface IMoviesService: IGenericService<MovieCDto, MovieC>
    {
        public Task<List<CountryCategoryDto>> GetCountryCategoriesAsync();
        public Task<List<GenresCategoryDto>> GetGenresCategoriesAsync();
        public Task<List<LanguageCategoryDto>> GetLangaugeCategoriesAsync();
        public Task<List<TrandCategoryDto>> GeTTrandsCategoriesAsync();

        public Task<List<MovieCDto>> GetMoviesByCategoryIdAsync(int id);
        public Task<MovieCDto> GetMoviesDetailByIdAsync(int id);
        public Task<MovieCDto> GetDetailByIdAsync(int id);

        public MovieCDto UpdateMovie(MovieCDto item);
    }
}
