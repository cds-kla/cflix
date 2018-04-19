using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CFlix.Migrations.CFlixAuth
{
    public partial class _06addEasterEggTypeandIsAvailable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "EasterEggType",
                table: "EasterEggs",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "EasterEggs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EasterEggType",
                table: "EasterEggs");

            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "EasterEggs");
        }
    }
}
