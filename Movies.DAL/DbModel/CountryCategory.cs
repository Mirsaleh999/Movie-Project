using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Movies.DAL.DbModel namespace contains the data access layer classes related to movies.
/// </summary>
namespace Movies.DAL.DbModel
{
    /// <summary>
    /// The CountryCategory class represents a category of countries for movies.
    /// Inherits from BaseEntity.
    /// </summary>
    public class CountryCategory : BaseEntity
    {
        /// <summary>
        /// Gets or sets the optional name of the country category.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the optional collection of movies associated with the country category.
        /// </summary>
        public virtual ICollection<MovieC>? Movies { get; set; }
    }
}

