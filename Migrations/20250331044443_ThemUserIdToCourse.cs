using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisCourse.Migrations
{
    /// <inheritdoc />
    public partial class ThemUserIdToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Courses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Courses");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "Thumbnail" },
                values: new object[] { 1, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Các bài viết không thuộc chủ đề cụ thể", "Không chủ đề", null });
        }
    }
}
