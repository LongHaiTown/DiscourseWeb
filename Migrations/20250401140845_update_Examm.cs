using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduquizSuper.Migrations
{
    /// <inheritdoc />
    public partial class update_Examm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "Exams",
                newName: "ScorePerQuestion");

            migrationBuilder.RenameColumn(
                name: "NumberOfQuestions",
                table: "Exams",
                newName: "MediumQuestions");

            migrationBuilder.AddColumn<int>(
                name: "EasyQuestions",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HardQuestions",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EasyQuestions",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "HardQuestions",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "ScorePerQuestion",
                table: "Exams",
                newName: "TotalScore");

            migrationBuilder.RenameColumn(
                name: "MediumQuestions",
                table: "Exams",
                newName: "NumberOfQuestions");
        }
    }
}
