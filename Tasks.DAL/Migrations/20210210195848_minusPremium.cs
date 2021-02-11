using Microsoft.EntityFrameworkCore.Migrations;

namespace Tasks.DAL.Migrations
{
    public partial class minusPremium : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Premium",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Premium",
                table: "Employees",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
