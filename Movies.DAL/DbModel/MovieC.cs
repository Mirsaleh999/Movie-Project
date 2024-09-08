using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{

    public class MovieC : BaseEntity
    {

        public string Name { get; set; }

        public byte Age { get; set; }

        public TimeSpan MovieTime { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public string? Img { get; set; }

        public string? MovieVideo { get; set; }

        public string? Trailer { get; set; }


        public int GenresCategoryId { get; set; }

        public GenresCategory? GenresCategory { get; set; }

        public int CountryCategoryId { get; set; }

        public CountryCategory? CountryCategory { get; set; }

        public int LanguageCategoryId { get; set; }

        public LanguageCategory? LanguageCategory { get; set; }

        public int? TrendId { get; set; }

        public Trends? Trend { get; set; }

        public virtual ICollection<MoviesDocument>? MoviesDocuments { get; set; }

        public bool Top { get; set; }

        public bool isneww { get; set; }

        public int ViewsCount { get; set; }

        public double RatingSum { get; set; } = 0;
        public int RatingCount { get; set; } = 0;
    }
}
