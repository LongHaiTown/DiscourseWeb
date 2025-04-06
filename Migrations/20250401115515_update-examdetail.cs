using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduquizSuper.Migrations
{
    /// <inheritdoc />
    public partial class updateexamdetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "ExamDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q001" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q002" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q003" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q004" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q005" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q006" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q007" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q008" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q009" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q010" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q011" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q012" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q013" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q014" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q015" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q016" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q017" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q018" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q019" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q020" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q021" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q022" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q023" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q024" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q025" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q026" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q027" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q028" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q029" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q030" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q031" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q032" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q033" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q034" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q035" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q036" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q037" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q038" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q039" },
                column: "IsCorrect",
                value: false);

            migrationBuilder.UpdateData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q040" },
                column: "IsCorrect",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "ExamDetails");
        }
    }
}
