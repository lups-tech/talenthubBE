using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class AmendedCommentColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Developers_DeveloperId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Comments",
                newName: "user_email");

            migrationBuilder.RenameColumn(
                name: "DeveloperId",
                table: "Comments",
                newName: "developer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Comments",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CommentText",
                table: "Comments",
                newName: "comment_text");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_DeveloperId",
                table: "Comments",
                newName: "IX_Comments_developer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Developers_developer_id",
                table: "Comments",
                column: "developer_id",
                principalTable: "Developers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_user_id",
                table: "Comments",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Developers_developer_id",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_user_id",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Comments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "user_email",
                table: "Comments",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "developer_id",
                table: "Comments",
                newName: "DeveloperId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Comments",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "comment_text",
                table: "Comments",
                newName: "CommentText");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_user_id",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_developer_id",
                table: "Comments",
                newName: "IX_Comments_DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Developers_DeveloperId",
                table: "Comments",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id");
        }
    }
}
