using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeajLearner.Infrastructure.Persistance.Migrations
{
    public partial class updatingSpeakModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "mediaFile",
                table: "speakActivityQuestions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string[]>(
                name: "mediaFile",
                table: "speakActivityQuestions",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
