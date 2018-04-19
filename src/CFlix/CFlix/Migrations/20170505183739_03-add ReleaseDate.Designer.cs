using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CFlix.Data;
using CFlix.Models;

namespace CFlix.Migrations
{
    [DbContext(typeof(CFlixContext))]
    [Migration("20170505183739_03-add ReleaseDate")]
    partial class _03addReleaseDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("CFlix.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImageUri");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TIMESTAMP");

                    b.Property<string>("Title");

                    b.Property<short>("Type");

                    b.Property<string>("YouTubeId");

                    b.HasKey("Id");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("CFlix.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<bool>("IsHidden");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TIMESTAMP");

                    b.Property<int>("MediaId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("MediaId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("CFlix.Models.Review", b =>
                {
                    b.HasOne("CFlix.Models.Media", "Media")
                        .WithMany("Reviews")
                        .HasForeignKey("MediaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
