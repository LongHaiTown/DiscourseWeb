using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisCourse.Migrations
{
    /// <inheritdoc />
    public partial class fixDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OwnerID",
                table: "Courses",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_OwnerID",
                table: "Courses",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_AspNetUsers_OwnerID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_OwnerID",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerID",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_AuthorId",
                table: "Courses",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_AspNetUsers_AuthorId",
                table: "Courses",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
