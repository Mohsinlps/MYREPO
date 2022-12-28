using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeajLearner.Infrastructure.Persistance.Migrations
{
    public partial class AddstatusInCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "subscribed",
                table: "Courses",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "subscribed",
                table: "Courses");
        }
    }
}
