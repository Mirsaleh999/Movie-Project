using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// The Movies.DAL.DbModel namespace contains the data access layer classes related to movies.
/// </summary>
namespace Movies.DAL.DbModel
{
    /// <summary>
    /// The Trends class represents a trending category for movies.
    /// Inherits from BaseEntity.
    /// </summary>
    public class Trends : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the trending category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the collection of movies associated with the trending category.
        /// </summary>
        public virtual ICollection<MovieC> Movie { get; set; }
    }
}

