using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class CreateOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "auth0Id",
                table: "Users",
                newName: "OrganizationId");

            migrationBuilder.RenameColumn(
                name: "UsersAuth0Id",
                table: "JobUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_JobUser_UsersAuth0Id",
                table: "JobUser",
                newName: "IX_JobUser_UsersId");

            migrationBuilder.RenameColumn(
                name: "UsersAuth0Id",
                table: "DeveloperUser",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_DeveloperUser_UsersAuth0Id",
                table: "DeveloperUser",
                newName: "IX_DeveloperUser_UsersId");

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationId",
                table: "Developers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobOrganization",
                columns: table => new
                {
                    JobsId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationsId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOrganization", x => new { x.JobsId, x.OrganizationsId });
                    table.ForeignKey(
                        name: "FK_JobOrganization_JobDescriptions_JobsId",
                        column: x => x.JobsId,
                        principalTable: "JobDescriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOrganization_Organizations_OrganizationsId",
                        column: x => x.OrganizationsId,
                        principalTable: "Organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Developers_OrganizationId",
                table: "Developers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOrganization_OrganizationsId",
                table: "JobOrganization",
                column: "OrganizationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Developers_Organizations_OrganizationId",
                table: "Developers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Developers_Organizations_OrganizationId",
                table: "Developers");

            migrationBuilder.DropForeignKey(
                name: "FK_DeveloperUser_Users_UsersId",
                table: "DeveloperUser");

            migrationBuilder.DropForeignKey(
                name: "FK_JobUser_Users_UsersId",
                table: "JobUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Organizations_OrganizationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "JobOrganization");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OrganizationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Developers_OrganizationId",
                table: "Developers");

            migrationBuilder.DropColumn(
                name: "id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Developers");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Users",
                newName: "auth0Id");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "JobUser",
                newName: "UsersAuth0Id");

            migrationBuilder.RenameIndex(
                name: "IX_JobUser_UsersId",
                table: "JobUser",
                newName: "IX_JobUser_UsersAuth0Id");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "DeveloperUser",
                newName: "UsersAuth0Id");

            migrationBuilder.RenameIndex(
                name: "IX_DeveloperUser_UsersId",
                table: "DeveloperUser",
                newName: "IX_DeveloperUser_UsersAuth0Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "auth0Id");

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
    }
}
