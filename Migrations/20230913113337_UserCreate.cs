using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class UserCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    auth0Id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    role = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperUser",
                columns: table => new
                {
                    DevelopersId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperUser", x => new { x.DevelopersId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_DeveloperUser_Developers_DevelopersId",
                        column: x => x.DevelopersId,
                        principalTable: "Developers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobUser",
                columns: table => new
                {
                    JobsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobUser", x => new { x.JobsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_JobUser_JobDescriptions_JobsId",
                        column: x => x.JobsId,
                        principalTable: "JobDescriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperUser_UsersId",
                table: "DeveloperUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_JobUser_UsersId",
                table: "JobUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperUser");

            migrationBuilder.DropTable(
                name: "JobUser");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
