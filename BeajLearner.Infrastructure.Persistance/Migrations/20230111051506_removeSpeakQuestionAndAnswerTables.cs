using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BeajLearner.Infrastructure.Persistance.Migrations
{
    public partial class removeSpeakQuestionAndAnswerTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "speakAnswer");

            migrationBuilder.DropTable(
                name: "speakQuestion");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "speakQuestion",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lessonId = table.Column<int>(type: "integer", nullable: false),
                    question = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_speakQuestion", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "speakAnswer",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    speakQuestionid = table.Column<int>(type: "integer", nullable: true),
                    answer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_speakAnswer", x => x.id);
                    table.ForeignKey(
                        name: "FK_speakAnswer_speakQuestion_speakQuestionid",
                        column: x => x.speakQuestionid,
                        principalTable: "speakQuestion",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_speakAnswer_speakQuestionid",
                table: "speakAnswer",
                column: "speakQuestionid");
        }
    }
}
