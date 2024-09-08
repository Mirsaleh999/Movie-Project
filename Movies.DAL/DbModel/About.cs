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
    /// The About class represents information about a specific entity or person.
    /// Inherits from BaseEntity.
    /// </summary>
    public class About : BaseEntity
    {
        /// <summary>
        /// Gets or sets the description of the entity or person.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the optional image URL related to the entity or person.
        /// </summary>
        public string? Img { get; set; }

        /// <summary>
        /// Gets or sets the position or role of the entity or person.
        /// </summary>
        public string Position { get; set; }
    }
}
