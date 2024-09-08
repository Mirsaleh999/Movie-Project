using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public  DbSet<MovieC> Movies { get; set; }
        public  DbSet<LanguageCategory> LanguageCategories { get; set; }
        public  DbSet<CountryCategory> CountryCategories { get; set; }
        public  DbSet<GenresCategory> GenresCategories { get; set; }
        public  DbSet<About> Abouts { get; set; }
        public  DbSet<Contact> Contacts { get; set; }
        public  DbSet<MoviesDocument> MoviesDocuments { get; set; }
        public  DbSet<Trends> Trends { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }

    }
}
