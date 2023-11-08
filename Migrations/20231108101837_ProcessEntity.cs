using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class ProcessEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchingProcesseses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    placed = table.Column<bool>(type: "boolean", nullable: true),
                    result_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeveloperId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchingProcesseses", x => x.id);
                    table.ForeignKey(
                        name: "FK_MatchingProcesseses_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchingProcesseses_JobDescriptions_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDescriptions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchingProcesseses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ContractStage = table.Column<int>(type: "integer", nullable: false),
                    MatchingProcessId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractData_MatchingProcesseses_MatchingProcessId",
                        column: x => x.MatchingProcessId,
                        principalTable: "MatchingProcesseses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InterviewType = table.Column<int>(type: "integer", nullable: false),
                    Passed = table.Column<bool>(type: "boolean", nullable: false),
                    MatchingProcessId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterviewData_MatchingProcesseses_MatchingProcessId",
                        column: x => x.MatchingProcessId,
                        principalTable: "MatchingProcesseses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProposedData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Succeeded = table.Column<bool>(type: "boolean", nullable: false),
                    MatchingProcessId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposedData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposedData_MatchingProcesseses_MatchingProcessId",
                        column: x => x.MatchingProcessId,
                        principalTable: "MatchingProcesseses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractData_MatchingProcessId",
                table: "ContractData",
                column: "MatchingProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewData_MatchingProcessId",
                table: "InterviewData",
                column: "MatchingProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingProcesseses_DeveloperId",
                table: "MatchingProcesseses",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingProcesseses_JobId",
                table: "MatchingProcesseses",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchingProcesseses_UserId",
                table: "MatchingProcesseses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProposedData_MatchingProcessId",
                table: "ProposedData",
                column: "MatchingProcessId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractData");

            migrationBuilder.DropTable(
                name: "InterviewData");

            migrationBuilder.DropTable(
                name: "ProposedData");

            migrationBuilder.DropTable(
                name: "MatchingProcesseses");
        }
    }
}
