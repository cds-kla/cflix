using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CFlix.Migrations.CFlixAuth
{
    public partial class _03addEasterEgg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasterEggs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Description = table.Column<string>(nullable: true),
                    Hash = table.Column<string>(maxLength: 64, nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasterEggs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserEasterEggs",
                columns: table => new
                {
                    EasterEggId = table.Column<int>(nullable: false),
                    CFlixUserId = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "current_timestamp")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEasterEggs", x => new { x.EasterEggId, x.CFlixUserId });
                    table.ForeignKey(
                        name: "FK_UserEasterEggs_AspNetUsers_CFlixUserId",
                        column: x => x.CFlixUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEasterEggs_EasterEggs_EasterEggId",
                        column: x => x.EasterEggId,
                        principalTable: "EasterEggs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEasterEggs_CFlixUserId",
                table: "UserEasterEggs",
                column: "CFlixUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EasterEggs_Hash",
                table: "EasterEggs",
                column: "Hash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEasterEggs");

            migrationBuilder.DropTable(
                name: "EasterEggs");
        }
    }
}
