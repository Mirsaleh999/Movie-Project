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
    /// The LanguageCategoryDto class represents the data transfer object for a country category in movies.
    /// Inherits from BaseDto.
    /// </summary>
    public class LanguageCategoryDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the optional name of the Language category.
        /// </summary>
        public string Name { get; set; }
    }
}
