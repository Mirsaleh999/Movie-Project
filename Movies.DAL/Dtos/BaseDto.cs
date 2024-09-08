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
    /// The BaseDto class represents a base class for data transfer objects in the system.
    /// </summary>
    public class BaseDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the data transfer object.
        /// </summary>
        public int Id { get; set; }
    }
}

