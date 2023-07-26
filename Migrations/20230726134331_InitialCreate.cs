using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "JobDescriptions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "text", nullable: true),
                    job_text = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescriptions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: true),
                    type = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "DeveloperSkill",
                columns: table => new
                {
                    DevelopersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeveloperSkill", x => new { x.DevelopersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_DeveloperSkill_Developers_DevelopersId",
                        column: x => x.DevelopersId,
                        principalTable: "Developers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeveloperSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeveloperSkill_SkillsId",
                table: "DeveloperSkill",
                column: "SkillsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeveloperSkill");

            migrationBuilder.DropTable(
                name: "JobDescriptions");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
