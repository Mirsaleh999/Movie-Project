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
    /// The ContactDto class represents the data transfer object for contact information of a person.
    /// Inherits from BaseDto.
    /// </summary>
    public class ContactDto : BaseDto
    {
        /// <summary>
        /// Gets or sets the full name of the person.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the person.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the message or comment provided by the person.
        /// </summary>
        public string Message { get; set; }
    }
}

