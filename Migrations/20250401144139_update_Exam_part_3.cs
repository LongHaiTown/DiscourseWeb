using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduquizSuper.Migrations
{
    /// <inheritdoc />
    public partial class update_Exam_part_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_ManagerId",
                table: "Exams");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Exams",
                type: "nvarchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfQuestions",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TotalScore",
                table: "Exams",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_ManagerId",
                table: "Exams",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exams_AspNetUsers_ManagerId",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "NumberOfQuestions",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Exams");

            migrationBuilder.AlterColumn<string>(
                name: "ManagerId",
                table: "Exams",
                type: "nvarchar(250)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_AspNetUsers_ManagerId",
                table: "Exams",
                column: "ManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
