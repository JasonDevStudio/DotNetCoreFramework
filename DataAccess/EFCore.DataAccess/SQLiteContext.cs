using System;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DataAccess
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext() : base() { }

        public SQLiteContext(DbContextOptions<SQLiteContext> options) : base(options) { }


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
