using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Repostory.Interfaces
{
    public interface IMoviesRepository: IGenericRepository<MovieC>
    {
        public Task<List<MovieC>> GetByCategoryIdAsync(int id);
        public Task<MovieCDto> GetDetailByIdAsync(int id);
       public  MovieC UpdateMovie(MovieC item);
    }
}
