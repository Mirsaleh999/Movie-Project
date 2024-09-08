using Movies.DAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Movies.DAL.Dtos namespace contains the data transfer object classes related to movies.
/// </summary>
namespace Movies.DAL.Dtos
{
    /// <summary>
    /// The MovieCDto class represents the data transfer object for movie information.
    /// Inherits from BaseDto.
    /// </summary>
    public class MovieCDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the name of the movie.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the age rating of the movie.
        /// </summary>
        public byte Age { get; set; }

        /// <summary>
        /// Gets or sets the duration of the movie.
        /// </summary>
        public TimeSpan MovieTime { get; set; }

        /// <summary>
        /// Gets or sets the release year of the movie.
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// Gets or sets the description of the movie.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the optional country category ID for the movie.
        /// </summary>
        public string? CountryCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the optional country category name.
        /// </summary>
        public string? CName { get; set; }

        /// <summary>
        /// Gets or sets the optional genre category ID for the movie.
        /// </summary>
        public string? GenresCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the optional genre category name.
        /// </summary>
        public string? GName { get; set; }

        /// <summary>
        /// Gets or sets the optional language category ID for the movie.
        /// </summary>
        public string? LanguageCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the optional language category name.
        /// </summary>
        public string? LName { get; set; }

        /// <summary>
        /// Gets or sets the optional trend ID for the movie.
        /// </summary>
        public string? TrandsId { get; set; }

        /// <summary>
        /// Gets or sets the optional director of the movie.
        /// </summary>
        public string? Director { get; set; }

        /// <summary>
        /// Gets or sets the optional image URL for the movie.
        /// </summary>
        public string? Img { get; set; }

        /// <summary>
        /// Gets or sets the optional movie video URL.
        /// </summary>
        public string? MovieVideo { get; set; }

        /// <summary>
        /// Gets or sets the optional trailer URL for the movie.
        /// </summary>
        public string? Trailer { get; set; }

        /// <summary>
        /// Gets or sets whether the movie is a top-rated movie.
        /// </summary>
        public bool Top { get; set; }

        /// <summary>
        /// Gets or sets whether the movie is new.
        /// </summary>
        public bool isneww { get; set; }
        public int ViewsCount { get; set; }

        /// <summary>
        /// Gets or sets the optional list of movie document DTOs associated with the movie.
        /// </summary>
        public List<MoviesDocumentDto>? MoviesDocumentDtos { get; set; }

        /// <summary>
        /// Gets or sets the optional list of trend category DTOs associated with the movie.
        /// </summary>
        public List<TrandCategoryDto>? TrandCategoryDtos { get; set; }

        /// <summary>
        /// Gets or sets the optional list of genre category DTOs associated with the movie.
        /// </summary>
        public List<GenresCategoryDto>? GenresCategoryDtos { get; set; }

        /// <summary>
        /// Gets or sets the optional list of language category DTOs associated with the movie.
        /// </summary>
        public List<LanguageCategoryDto>? LanguageCategoryDtos { get; set; }
        public List<CountryCategoryDto> CountryCategoryDtos { get; set; }
    }
}
