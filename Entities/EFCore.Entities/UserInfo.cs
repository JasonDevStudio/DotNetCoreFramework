using System;
namespace EFCore.Entities
{
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public decimal ID { get; set; }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>The account.</value>
        public string ACCOUNT { get; set; }

        /// <summary>
        /// Gets or sets the NAME.
        /// </summary>
        /// <value>The NAME.</value>
        public string NAME { get; set; }

        /// <summary>
        /// Gets or sets the CREATEDATE.
        /// </summary>
        /// <value>The CREATEDATE.</value>
        public DateTime CREATEDATE { get; set; }

        /// <summary>
        /// Gets or sets the STATUS.
        /// </summary>
        /// <value>The STATUS.</value>
        public string STATUS { get; set; }
    }
}
