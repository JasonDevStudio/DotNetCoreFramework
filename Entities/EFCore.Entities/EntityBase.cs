using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Entities
{
    [NotMapped]
    public class EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int ID { get; set; } 

        /// <summary>
        /// Gets or sets the STATUS.
        /// </summary>
        /// <value>The STATUS.</value>
        public string STATUS { get; set; }
    }
}
