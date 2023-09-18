using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class jobTechId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Skills",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "jobTech_id",
                table: "JobDescriptions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "jobTech_id",
                table: "JobDescriptions");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "Skills",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
