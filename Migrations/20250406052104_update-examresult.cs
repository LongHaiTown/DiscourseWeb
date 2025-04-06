using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduquizSuper.Migrations
{
    /// <inheritdoc />
    public partial class updateexamresult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectCount",
                table: "ExamResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IncorrectCount",
                table: "ExamResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnansweredCount",
                table: "ExamResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectCount",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "IncorrectCount",
                table: "ExamResults");

            migrationBuilder.DropColumn(
                name: "UnansweredCount",
                table: "ExamResults");
        }
    }
}
