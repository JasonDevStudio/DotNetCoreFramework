using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.DataAccess
{
    /// <summary>
    /// DbContextBase
    /// </summary>
    public class DbContextBase : DbContext
    {
        /// <summary>
        /// DbContext
        /// </summary>
        public DbContext DBCont { get; private set; } 

        /// <summary>
        /// DbContextBase
        /// </summary>
        public DbContextBase() : base() { }

        /// <summary>
        /// DbContextBase
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        public DbContextBase(DbContextOptions options) : base(options) { }
    }
}
