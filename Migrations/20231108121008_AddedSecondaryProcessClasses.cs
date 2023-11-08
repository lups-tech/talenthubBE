using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class AddedSecondaryProcessClasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractData_MatchingProcesses_MatchingProcessId",
                table: "ContractData");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewData_MatchingProcesses_MatchingProcessId",
                table: "InterviewData");

            migrationBuilder.DropForeignKey(
                name: "FK_ProposedData_MatchingProcesses_MatchingProcessId",
                table: "ProposedData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProposedData",
                table: "ProposedData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InterviewData",
                table: "InterviewData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractData",
                table: "ContractData");

            migrationBuilder.RenameTable(
                name: "ProposedData",
                newName: "Proposals");

            migrationBuilder.RenameTable(
                name: "InterviewData",
                newName: "Interviews");

            migrationBuilder.RenameTable(
                name: "ContractData",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_ProposedData_MatchingProcessId",
                table: "Proposals",
                newName: "IX_Proposals_MatchingProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_InterviewData_MatchingProcessId",
                table: "Interviews",
                newName: "IX_Interviews_MatchingProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractData_MatchingProcessId",
                table: "Contracts",
                newName: "IX_Contracts_MatchingProcessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Proposals",
                table: "Proposals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Interviews",
                table: "Interviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_MatchingProcesses_MatchingProcessId",
                table: "Contracts",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Interviews_MatchingProcesses_MatchingProcessId",
                table: "Interviews",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proposals_MatchingProcesses_MatchingProcessId",
                table: "Proposals",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_MatchingProcesses_MatchingProcessId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Interviews_MatchingProcesses_MatchingProcessId",
                table: "Interviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposals_MatchingProcesses_MatchingProcessId",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Proposals",
                table: "Proposals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Interviews",
                table: "Interviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "Proposals",
                newName: "ProposedData");

            migrationBuilder.RenameTable(
                name: "Interviews",
                newName: "InterviewData");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "ContractData");

            migrationBuilder.RenameIndex(
                name: "IX_Proposals_MatchingProcessId",
                table: "ProposedData",
                newName: "IX_ProposedData_MatchingProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_Interviews_MatchingProcessId",
                table: "InterviewData",
                newName: "IX_InterviewData_MatchingProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_MatchingProcessId",
                table: "ContractData",
                newName: "IX_ContractData_MatchingProcessId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProposedData",
                table: "ProposedData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InterviewData",
                table: "InterviewData",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractData",
                table: "ContractData",
                column: "Id");

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
                name: "FK_ProposedData_MatchingProcesses_MatchingProcessId",
                table: "ProposedData",
                column: "MatchingProcessId",
                principalTable: "MatchingProcesses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
