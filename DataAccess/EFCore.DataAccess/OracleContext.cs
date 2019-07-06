using System;
using System.Data.Common;
using System.IO;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DataAccess
{
    public class OracleContext : DbContextBase
    {
        public OracleContext() : base() { }

        public OracleContext(DbContextOptions<OracleContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.2)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCLPDB1)));User Id=USERTEST;Password=123456");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("USERINFO", "USERTEST").HasKey(m => m.ACCOUNT);
        }
    }
}
