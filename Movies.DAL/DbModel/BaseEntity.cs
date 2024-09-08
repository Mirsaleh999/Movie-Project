using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.DAL.DbModel
{
    /// <summary>
    /// The BaseEntity class represents a base class for entities in the system.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date and time the entity was inserted. Defaults to the current date and time.
        /// </summary>
        public DateTime InsertDate { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets or sets the optional date and time the entity was last updated.
        /// </summary>
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// Gets or sets the optional date and time the entity was deleted.
        /// </summary>
        public DateTime? DeletedDate { get; set; }
    }

}
