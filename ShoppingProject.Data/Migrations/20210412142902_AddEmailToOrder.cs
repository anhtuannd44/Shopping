using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingProject.Data.Migrations
{
    public partial class AddEmailToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomerEmail",
                table: "Order",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerEmail",
                table: "Order");
        }
    }
}
