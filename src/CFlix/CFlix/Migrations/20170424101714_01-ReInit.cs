using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CFlix.Migrations
{
    public partial class _01ReInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ImageUri = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Type = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Content = table.Column<string>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    MediaId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_MediaId",
                table: "Reviews",
                column: "MediaId");

            migrationBuilder.Sql(@"
CREATE USER IF NOT EXISTS 'cflix-ro'@'%' IDENTIFIED BY 'mysql_password_ro';
GRANT SELECT, REFERENCES ON cflixdb.* TO 'cflix-ro'@'%';
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.Sql(@"
REVOKE ALL privileges ON cflixdb.* FROM 'cflix-ro'@'%';
DROP USER 'cflix-ro'@'%';
");
        }
    }
}
