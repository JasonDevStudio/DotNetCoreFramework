using System;
using System.Data.Common;
using System.IO;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCore.DataAccess
{
    public class OracleContext : DbContext
    {
        public OracleContext() : base() { }

        public OracleContext(DbContextOptions<OracleContext> options) : base(options) { }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle($"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.2)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCLPDB1)));User Id=USERTEST;Password=123456");

            //var csb = new Devart.Common.DbConnectionStringBuilder();
            //csb["Direct"] = true;
            //csb["Server"] = "127.0.0.1";
            //csb["Port"] = 1521;
            //csb["Sid"] = "xe";
            //csb["User Id"] = "UTEST";
            //csb["Password"] = "123456";
            //csb["Max Pool Size"] = 15000;
            //csb["Connection Timeout"] = 30;
            //csb["License Key"] = $"trial:{Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Devart.Data.Oracle.key")}";
            //optionsBuilder.UseOracle(csb.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>().ToTable("USERINFO", "USERTEST").HasKey(m => m.ID);
            //modelBuilder.HasSequence<int>("USERINFO_SEQ", "UTEST").StartsAt(2).IncrementsBy(1);
            //modelBuilder.Entity<UserInfo>().Property(m => m.ID).HasDefaultValueSql(" USERINFO_SEQ.NETXVAL ");
        }
    }
}
