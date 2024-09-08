using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.DAL.DbModel;
using Movies.DAL.Dtos;
using Movies.DAL.Repostory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Movies.DAL.Repostory
{
    public class MoviesRepository : GenericRepository<MovieC>, IMoviesRepository
    {
        public MoviesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<MovieC>> GetByCategoryIdAsync(int id)
        {
            IQueryable<MovieC> movie = _dbContext.Movies.Where(p => p.CountryCategoryId == id &&
            p.LanguageCategoryId == id && p.GenresCategoryId == id && p.TrendId == id);

            return movie.ToList();

        }
  
        public async Task< MovieCDto> GetDetailByIdAsync(int id)
        {
            var movieCDto = await (from m in _dbContext.Movies
                             join gc in _dbContext.GenresCategories on m.GenresCategoryId equals gc.Id
                             join c in _dbContext.CountryCategories on m.CountryCategoryId equals c.Id
                             join l in _dbContext.LanguageCategories on m.LanguageCategoryId equals l.Id
                             where m.Id == id
                             select new MovieCDto
                             {
                                 Id = m.Id,
                                 Name = m.Name,
                                 Img = m.Img,
                                 Age = m.Age,
                                 Year = m.Year,
                                 Director = m.Director,
                                 MovieTime = m.MovieTime,
                                 Description = m.Description,
                                 GName = gc.Name,
                                 CName = c.Name,
                                 LName = l.Name
                                

                             }).FirstOrDefaultAsync();

            return movieCDto;
        }


        public MovieC UpdateMovie(MovieC item)
        {
            var dbEntity = _entities.Find(item.Id);
            item.InsertDate = dbEntity.InsertDate;
            item.UpdateDate = DateTime.Now;
            if (string.IsNullOrEmpty(item.Img))
            {
                item.Img = dbEntity.Img;
            }
            if (string.IsNullOrEmpty(item.MovieVideo))
            {
                item.MovieVideo = dbEntity.MovieVideo;
            }
            if (string.IsNullOrEmpty(item.Trailer))
            {
                item.Trailer = dbEntity.Trailer;
            }
            _entities.Update(item);
            _dbContext.SaveChanges();
            return item;
        }
    }
}
