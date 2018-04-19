using CFlix.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CFlix.Data
{
    public class CFlixAuthContext : IdentityDbContext<CFlixUser>
    {
        public CFlixAuthContext()
        {
        }

        public CFlixAuthContext(DbContextOptions<CFlixAuthContext> options)
            : base(options)
        {
        }

        public DbSet<EasterEgg> EasterEggs { get; set; }

        public DbSet<CFlixUserEasterEgg> UserEasterEggs { get; set; }

        public DbSet<NewsRelease> NewsReleases { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("host=localhost;userid=postgres;password=postgres_password;database=cflix");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CFlixUserEasterEgg>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("current_timestamp");

            builder.Entity<CFlixUserEasterEgg>()
                .HasKey(ueg => new { ueg.EasterEggId, ueg.CFlixUserId });

            builder.Entity<CFlixUserEasterEgg>()
                .HasOne(ueg => ueg.CFlixUser)
                .WithMany(u => u.EasterEggs)
                .HasForeignKey(ueg => ueg.CFlixUserId);

            builder.Entity<CFlixUserEasterEgg>()
                .HasOne(ueg => ueg.EasterEgg)
                .WithMany()
                .HasForeignKey(ueg => ueg.EasterEggId);

            builder.Entity<NewsRelease>()
                .Property(b => b.CreationDate)
                .HasDefaultValueSql("current_timestamp");

        }
    }
}
