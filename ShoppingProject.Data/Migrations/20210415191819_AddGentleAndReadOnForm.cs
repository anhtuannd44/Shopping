using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingProject.Data.Migrations
{
    public partial class AddGentleAndReadOnForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "FormCSKH",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Gentle",
                table: "FormCSKH",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "FormCSKH",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "FormCSKH");

            migrationBuilder.DropColumn(
                name: "Gentle",
                table: "FormCSKH");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "FormCSKH");
        }
    }
}
