using Microsoft.EntityFrameworkCore.Migrations;

namespace OnBoarding.Migrations
{
    public partial class updatedlatest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "EndUser");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Agent");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "EndUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Agent",
                nullable: true);
        }
    }
}
