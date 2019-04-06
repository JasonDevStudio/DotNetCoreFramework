using System;
namespace EFCore.Entities
{ 
    public class UserInfo : EntityBase
    {
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
        public string CREATEDATE { get; set; } 
    }
}
