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
    /// The Contact class represents contact information of a person.
    /// Inherits from BaseEntity.
    /// </summary>
    public class Contact : BaseEntity
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

