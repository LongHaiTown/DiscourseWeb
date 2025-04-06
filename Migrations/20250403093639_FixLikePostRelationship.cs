using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DisCourse.Migrations
{
    /// <inheritdoc />
    public partial class FixLikePostRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts");

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts");

            migrationBuilder.AddForeignKey(
                name: "FK_LikePosts_Posts_PostId",
                table: "LikePosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
