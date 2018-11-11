using System;
using EFCore.Entities;
using Microsoft.EntityFrameworkCore; 

namespace EFCore.DataAccess
{
    public class XEContext : DbContext
    {
        public XEContext():base(){}

        public XEContext(DbContextOptions<XEContext> options):base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("User ID=utest;Password=123456;Host=localhost;Port=1521;SID=xe;Pooling=true;Min Pool Size=1;Max Pool Size=100;Connection Lifetime=0;Direct=True;"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<UserInfo>().ToTable("USER_INFO", "UTEST").HasKey(m=>m.ID); 
        } 
    }  
}
