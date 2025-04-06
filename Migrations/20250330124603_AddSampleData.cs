using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EduquizSuper.Migrations
{
    /// <inheritdoc />
    public partial class AddSampleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "AnswersJson", "Difficulty", "QuestionContent", "SubjectId" },
                values: new object[,]
                {
                    { "Q001", "[{\"id\":\"1\",\"content\":\"3\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"4\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"6\",\"isCorrect\":false}]", "Easy", "What is 2 + 2?", "SUB001" },
                    { "Q002", "[{\"id\":\"1\",\"content\":\"2\",\"isCorrect\":true},{\"id\":\"2\",\"content\":\"3\",\"isCorrect\":false},{\"id\":\"3\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"1\",\"isCorrect\":false}]", "Easy", "What is 5 - 3?", "SUB001" },
                    { "Q003", "[{\"id\":\"1\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"8\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"10\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"12\",\"isCorrect\":false}]", "Easy", "What is 4 × 2?", "SUB001" },
                    { "Q004", "[{\"id\":\"1\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"2\",\"isCorrect\":false}]", "Easy", "What is 10 ÷ 2?", "SUB001" },
                    { "Q005", "[{\"id\":\"1\",\"content\":\"11\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"12\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"13\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"10\",\"isCorrect\":false}]", "Easy", "What is 7 + 5?", "SUB001" },
                    { "Q006", "[{\"id\":\"1\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"2\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"3\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"3\",\"isCorrect\":false}]", "Easy", "What is 9 - 4?", "SUB001" },
                    { "Q007", "[{\"id\":\"1\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"9\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"15\",\"isCorrect\":false}]", "Easy", "What is 3 × 3?", "SUB001" },
                    { "Q008", "[{\"id\":\"1\",\"content\":\"3\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"4\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"6\",\"isCorrect\":false}]", "Easy", "What is 12 ÷ 3?", "SUB001" },
                    { "Q009", "[{\"id\":\"1\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"14\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"16\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"18\",\"isCorrect\":false}]", "Easy", "What is 6 + 8?", "SUB001" },
                    { "Q010", "[{\"id\":\"1\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"8\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"9\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"7\",\"isCorrect\":false}]", "Easy", "What is 15 - 7?", "SUB001" },
                    { "Q011", "[{\"id\":\"1\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"10\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"15\",\"isCorrect\":false}]", "Easy", "What is 2 × 5?", "SUB001" },
                    { "Q012", "[{\"id\":\"1\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"7\",\"isCorrect\":false}]", "Easy", "What is 20 ÷ 4?", "SUB001" },
                    { "Q013", "[{\"id\":\"1\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"10\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"11\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"12\",\"isCorrect\":false}]", "Easy", "What is 1 + 9?", "SUB001" },
                    { "Q014", "[{\"id\":\"1\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"6\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"7\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"4\",\"isCorrect\":false}]", "Easy", "What is 8 - 2?", "SUB001" },
                    { "Q015", "[{\"id\":\"1\",\"content\":\"8\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"10\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"12\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"15\",\"isCorrect\":false}]", "Easy", "What is 5 × 2?", "SUB001" },
                    { "Q016", "[{\"id\":\"1\",\"content\":\"40\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"45\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"50\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"55\",\"isCorrect\":false}]", "Medium", "What is 15 × 3?", "SUB001" },
                    { "Q017", "[{\"id\":\"1\",\"content\":\"5\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"6\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"7\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"8\",\"isCorrect\":false}]", "Medium", "What is 36 ÷ 6?", "SUB001" },
                    { "Q018", "[{\"id\":\"1\",\"content\":\"28\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"30\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"32\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"34\",\"isCorrect\":false}]", "Medium", "What is 12 + 18?", "SUB001" },
                    { "Q019", "[{\"id\":\"1\",\"content\":\"25\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"27\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"29\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"31\",\"isCorrect\":false}]", "Medium", "What is 50 - 23?", "SUB001" },
                    { "Q020", "[{\"id\":\"1\",\"content\":\"54\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"56\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"58\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"60\",\"isCorrect\":false}]", "Medium", "What is 7 × 8?", "SUB001" },
                    { "Q021", "[{\"id\":\"1\",\"content\":\"4\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"5\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"6\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"7\",\"isCorrect\":false}]", "Medium", "What is 45 ÷ 9?", "SUB001" },
                    { "Q022", "[{\"id\":\"1\",\"content\":\"43\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"45\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"47\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"49\",\"isCorrect\":false}]", "Medium", "What is 19 + 26?", "SUB001" },
                    { "Q023", "[{\"id\":\"1\",\"content\":\"32\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"34\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"36\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"38\",\"isCorrect\":false}]", "Medium", "What is 72 - 38?", "SUB001" },
                    { "Q024", "[{\"id\":\"1\",\"content\":\"52\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"54\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"56\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"58\",\"isCorrect\":false}]", "Medium", "What is 9 × 6?", "SUB001" },
                    { "Q025", "[{\"id\":\"1\",\"content\":\"14\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"16\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"18\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"20\",\"isCorrect\":false}]", "Medium", "What is 80 ÷ 5?", "SUB001" },
                    { "Q026", "[{\"id\":\"1\",\"content\":\"40\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"42\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"44\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"46\",\"isCorrect\":false}]", "Medium", "What is 13 + 29?", "SUB001" },
                    { "Q027", "[{\"id\":\"1\",\"content\":\"43\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"45\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"47\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"49\",\"isCorrect\":false}]", "Medium", "What is 64 - 19?", "SUB001" },
                    { "Q028", "[{\"id\":\"1\",\"content\":\"42\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"44\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"46\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"48\",\"isCorrect\":false}]", "Medium", "What is 11 × 4?", "SUB001" },
                    { "Q029", "[{\"id\":\"1\",\"content\":\"13\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"15\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"17\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"19\",\"isCorrect\":false}]", "Medium", "What is 90 ÷ 6?", "SUB001" },
                    { "Q030", "[{\"id\":\"1\",\"content\":\"40\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"42\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"44\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"46\",\"isCorrect\":false}]", "Medium", "What is 25 + 17?", "SUB001" },
                    { "Q031", "[{\"id\":\"1\",\"content\":\"211\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"221\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"231\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"241\",\"isCorrect\":false}]", "Hard", "What is 17 × 13?", "SUB001" },
                    { "Q032", "[{\"id\":\"1\",\"content\":\"10\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"12\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"14\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"16\",\"isCorrect\":false}]", "Hard", "What is 144 ÷ 12?", "SUB001" },
                    { "Q033", "[{\"id\":\"1\",\"content\":\"132\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"134\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"136\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"138\",\"isCorrect\":false}]", "Hard", "What is 56 + 78?", "SUB001" },
                    { "Q034", "[{\"id\":\"1\",\"content\":\"34\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"36\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"38\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"40\",\"isCorrect\":false}]", "Hard", "What is 123 - 87?", "SUB001" },
                    { "Q035", "[{\"id\":\"1\",\"content\":\"131\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"133\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"135\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"137\",\"isCorrect\":false}]", "Hard", "What is 19 × 7?", "SUB001" },
                    { "Q036", "[{\"id\":\"1\",\"content\":\"13\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"15\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"17\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"19\",\"isCorrect\":false}]", "Hard", "What is 225 ÷ 15?", "SUB001" },
                    { "Q037", "[{\"id\":\"1\",\"content\":\"154\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"156\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"158\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"160\",\"isCorrect\":false}]", "Hard", "What is 89 + 67?", "SUB001" },
                    { "Q038", "[{\"id\":\"1\",\"content\":\"76\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"78\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"80\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"82\",\"isCorrect\":false}]", "Hard", "What is 176 - 98?", "SUB001" },
                    { "Q039", "[{\"id\":\"1\",\"content\":\"205\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"207\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"209\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"211\",\"isCorrect\":false}]", "Hard", "What is 23 × 9?", "SUB001" },
                    { "Q040", "[{\"id\":\"1\",\"content\":\"23\",\"isCorrect\":false},{\"id\":\"2\",\"content\":\"25\",\"isCorrect\":true},{\"id\":\"3\",\"content\":\"27\",\"isCorrect\":false},{\"id\":\"4\",\"content\":\"29\",\"isCorrect\":false}]", "Hard", "What is 300 ÷ 12?", "SUB001" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q001");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q002");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q003");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q004");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q005");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q006");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q007");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q008");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q009");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q010");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q011");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q012");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q013");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q014");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q015");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q016");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q017");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q018");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q019");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q020");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q021");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q022");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q023");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q024");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q025");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q026");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q027");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q028");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q029");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q030");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q031");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q032");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q033");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q034");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q035");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q036");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q037");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q038");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q039");

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "QuestionId",
                keyValue: "Q040");
        }
    }
}
