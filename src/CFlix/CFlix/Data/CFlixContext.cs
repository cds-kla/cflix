using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using CFlix.Models;

namespace CFlix.Data
{
    public class CFlixContext : DbContext
    {
        public CFlixContext()
        {
        }

        public CFlixContext(DbContextOptions<CFlixContext> options) : base(options)
        {
        }

        public DbSet<Media> Medias { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("Server=localhost;Uid=root;Pwd=mysql_password;Database=cflixdb;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().Property(r => r.LastUpdated);//.IsRowVersion();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class CFlixROContext : CFlixContext
    {
        public CFlixROContext(DbContextOptions<CFlixContext> options) : base(options)
        {
        }
    }
}
