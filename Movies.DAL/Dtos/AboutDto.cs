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
    /// The AboutDto class represents the data transfer object for information about a specific entity or person.
    /// Inherits from BaseDto.
    /// </summary>
    public class AboutDto : BaseDto
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

