using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduquizSuper.Migrations
{
    /// <inheritdoc />
    public partial class AddExamDetailsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ExamDetails",
                columns: new[] { "ExamId", "QuestionId" },
                values: new object[,]
                {
                    { "EXAM001", "Q001" },
                    { "EXAM001", "Q002" },
                    { "EXAM001", "Q003" },
                    { "EXAM001", "Q004" },
                    { "EXAM001", "Q005" },
                    { "EXAM001", "Q006" },
                    { "EXAM001", "Q007" },
                    { "EXAM001", "Q008" },
                    { "EXAM001", "Q009" },
                    { "EXAM001", "Q010" },
                    { "EXAM001", "Q011" },
                    { "EXAM001", "Q012" },
                    { "EXAM001", "Q013" },
                    { "EXAM001", "Q014" },
                    { "EXAM001", "Q015" },
                    { "EXAM001", "Q016" },
                    { "EXAM001", "Q017" },
                    { "EXAM001", "Q018" },
                    { "EXAM001", "Q019" },
                    { "EXAM001", "Q020" },
                    { "EXAM001", "Q021" },
                    { "EXAM001", "Q022" },
                    { "EXAM001", "Q023" },
                    { "EXAM001", "Q024" },
                    { "EXAM001", "Q025" },
                    { "EXAM001", "Q026" },
                    { "EXAM001", "Q027" },
                    { "EXAM001", "Q028" },
                    { "EXAM001", "Q029" },
                    { "EXAM001", "Q030" },
                    { "EXAM001", "Q031" },
                    { "EXAM001", "Q032" },
                    { "EXAM001", "Q033" },
                    { "EXAM001", "Q034" },
                    { "EXAM001", "Q035" },
                    { "EXAM001", "Q036" },
                    { "EXAM001", "Q037" },
                    { "EXAM001", "Q038" },
                    { "EXAM001", "Q039" },
                    { "EXAM001", "Q040" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q001" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q002" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q003" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q004" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q005" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q006" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q007" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q008" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q009" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q010" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q011" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q012" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q013" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q014" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q015" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q016" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q017" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q018" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q019" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q020" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q021" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q022" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q023" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q024" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q025" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q026" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q027" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q028" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q029" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q030" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q031" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q032" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q033" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q034" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q035" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q036" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q037" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q038" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q039" });

            migrationBuilder.DeleteData(
                table: "ExamDetails",
                keyColumns: new[] { "ExamId", "QuestionId" },
                keyValues: new object[] { "EXAM001", "Q040" });
        }
    }
}
