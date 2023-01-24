using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeajLearner.Infrastructure.Persistance.Migrations
{
    public partial class addingDocumentFiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Audios",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "image",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "videos",
                table: "Lesson");

            migrationBuilder.CreateTable(
                name: "DocumentFiles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lessonId = table.Column<int>(type: "integer", nullable: false),
                    language = table.Column<string>(type: "text", nullable: true),
                    video = table.Column<string>(type: "text", nullable: true),
                    audio = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<string>(type: "text", nullable: true),
                    mediaType = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_DocumentFiles_Lesson_lessonId",
                        column: x => x.lessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_speakActivityQuestions_lessonId",
                table: "speakActivityQuestions",
                column: "lessonId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFiles_lessonId",
                table: "DocumentFiles",
                column: "lessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_speakActivityQuestions_Lesson_lessonId",
                table: "speakActivityQuestions",
                column: "lessonId",
                principalTable: "Lesson",
                principalColumn: "LessonId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_speakActivityQuestions_Lesson_lessonId",
                table: "speakActivityQuestions");

            migrationBuilder.DropTable(
                name: "DocumentFiles");

            migrationBuilder.DropIndex(
                name: "IX_speakActivityQuestions_lessonId",
                table: "speakActivityQuestions");

            migrationBuilder.AddColumn<string[]>(
                name: "Audios",
                table: "Lesson",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "image",
                table: "Lesson",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "videos",
                table: "Lesson",
                type: "text[]",
                nullable: true);
        }
    }
}
