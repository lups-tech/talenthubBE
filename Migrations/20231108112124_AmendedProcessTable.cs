using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class AmendedProcessTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractData_MatchingProcesseses_MatchingProcessId",
                table: "ContractData");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewData_MatchingProcesseses_MatchingProcessId",
                table: "InterviewData");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingProcesseses_Developers_DeveloperId",
                table: "MatchingProcesseses");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingProcesseses_JobDescriptions_JobId",
                table: "MatchingProcesseses");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingProcesseses_Users_UserId",
                table: "MatchingProcesseses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposedData_MatchingProcesseses_MatchingProcessId",
                table: "ProposedData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchingProcesseses",
                table: "MatchingProcesseses");

            migrationBuilder.RenameTable(
                name: "MatchingProcesseses",
                newName: "MatchingProcesses");

            migrationBuilder.RenameIndex(
                name: "IX_MatchingProcesseses_UserId",
                table: "MatchingProcesses",
                newName: "IX_MatchingProcesses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchingProcesseses_JobId",
                table: "MatchingProcesses",
                newName: "IX_MatchingProcesses_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchingProcesseses_DeveloperId",
                table: "MatchingProcesses",
                newName: "IX_MatchingProcesses_DeveloperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchingProcesses",
                table: "MatchingProcesses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractData_MatchingProcesses_MatchingProcessId",
                table: "ContractData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewData_MatchingProcesses_MatchingProcessId",
                table: "InterviewData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingProcesses_Developers_DeveloperId",
                table: "MatchingProcesses",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingProcesses_JobDescriptions_JobId",
                table: "MatchingProcesses",
                column: "JobId",
                principalTable: "JobDescriptions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingProcesses_Users_UserId",
                table: "MatchingProcesses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedData_MatchingProcesses_MatchingProcessId",
                table: "ProposedData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractData_MatchingProcesses_MatchingProcessId",
                table: "ContractData");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewData_MatchingProcesses_MatchingProcessId",
                table: "InterviewData");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingProcesses_Developers_DeveloperId",
                table: "MatchingProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingProcesses_JobDescriptions_JobId",
                table: "MatchingProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchingProcesses_Users_UserId",
                table: "MatchingProcesses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposedData_MatchingProcesses_MatchingProcessId",
                table: "ProposedData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MatchingProcesses",
                table: "MatchingProcesses");

            migrationBuilder.RenameTable(
                name: "MatchingProcesses",
                newName: "MatchingProcesseses");

            migrationBuilder.RenameIndex(
                name: "IX_MatchingProcesses_UserId",
                table: "MatchingProcesseses",
                newName: "IX_MatchingProcesseses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchingProcesses_JobId",
                table: "MatchingProcesseses",
                newName: "IX_MatchingProcesseses_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchingProcesses_DeveloperId",
                table: "MatchingProcesseses",
                newName: "IX_MatchingProcesseses_DeveloperId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MatchingProcesseses",
                table: "MatchingProcesseses",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractData_MatchingProcesseses_MatchingProcessId",
                table: "ContractData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesseses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewData_MatchingProcesseses_MatchingProcessId",
                table: "InterviewData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesseses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingProcesseses_Developers_DeveloperId",
                table: "MatchingProcesseses",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingProcesseses_JobDescriptions_JobId",
                table: "MatchingProcesseses",
                column: "JobId",
                principalTable: "JobDescriptions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchingProcesseses_Users_UserId",
                table: "MatchingProcesseses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProposedData_MatchingProcesseses_MatchingProcessId",
                table: "ProposedData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesseses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
