using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeajLearner.Infrastructure.Persistance.Migrations
{
    public partial class addingMediaFileSpeakActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "mediaFile",
                table: "speakActivityQuestions",
                type: "text[]",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mediaFile",
                table: "speakActivityQuestions");
        }
    }
}
