using System;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DataAccess
{
    public class XEContext : DbContext
    {
        public XEContext() : base() { }

        public XEContext(DbContextOptions<XEContext> options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=/Volumes/MAC_VM/00_GitHub/DotNetCoreFramework/DataBase/UTEST.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("USER_INFO").HasKey(m => m.ID);
        }
    }
}
