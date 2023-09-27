using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class NewUserObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUser_Users_UsersId",
                table: "DeveloperUser");

            migrationBuilder.DropForeignKey(
                name: "FK_JobUser_Users_UsersId",
                table: "JobUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobUser",
                table: "JobUser");

            migrationBuilder.DropIndex(
                name: "IX_JobUser_UsersId",
                table: "JobUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeveloperUser",
                table: "DeveloperUser");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperUser_UsersId",
                table: "DeveloperUser");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "role",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "JobUser");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "DeveloperUser");

            migrationBuilder.AddColumn<string>(
                name: "UsersAuth0Id",
                table: "JobUser",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UsersAuth0Id",
                table: "DeveloperUser",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "auth0Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobUser",
                table: "JobUser",
                columns: new[] { "JobsId", "UsersAuth0Id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeveloperUser",
                table: "DeveloperUser",
                columns: new[] { "DevelopersId", "UsersAuth0Id" });

            migrationBuilder.CreateIndex(
                name: "IX_JobUser_UsersAuth0Id",
                table: "JobUser",
                column: "UsersAuth0Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperUser_UsersAuth0Id",
                table: "DeveloperUser",
                column: "UsersAuth0Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUser_Users_UsersAuth0Id",
                table: "DeveloperUser",
                column: "UsersAuth0Id",
                principalTable: "Users",
                principalColumn: "auth0Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobUser_Users_UsersAuth0Id",
                table: "JobUser",
                column: "UsersAuth0Id",
                principalTable: "Users",
                principalColumn: "auth0Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUser_Users_UsersAuth0Id",
                table: "DeveloperUser");

            migrationBuilder.DropForeignKey(
                name: "FK_JobUser_Users_UsersAuth0Id",
                table: "JobUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobUser",
                table: "JobUser");

            migrationBuilder.DropIndex(
                name: "IX_JobUser_UsersAuth0Id",
                table: "JobUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeveloperUser",
                table: "DeveloperUser");

            migrationBuilder.DropIndex(
                name: "IX_DeveloperUser_UsersAuth0Id",
                table: "DeveloperUser");

            migrationBuilder.DropColumn(
                name: "UsersAuth0Id",
                table: "JobUser");

            migrationBuilder.DropColumn(
                name: "UsersAuth0Id",
                table: "DeveloperUser");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "JobUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsersId",
                table: "DeveloperUser",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobUser",
                table: "JobUser",
                columns: new[] { "JobsId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeveloperUser",
                table: "DeveloperUser",
                columns: new[] { "DevelopersId", "UsersId" });

            migrationBuilder.CreateIndex(
                name: "IX_JobUser_UsersId",
                table: "JobUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperUser_UsersId",
                table: "DeveloperUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeveloperUser_Users_UsersId",
                table: "DeveloperUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobUser_Users_UsersId",
                table: "JobUser",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
