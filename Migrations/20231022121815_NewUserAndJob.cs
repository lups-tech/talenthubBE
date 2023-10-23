using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talenthubBE.Migrations
{
    /// <inheritdoc />
    public partial class NewUserAndJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "deadline",
                table: "JobDescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employer",
                table: "JobDescriptions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "title",
                table: "JobDescriptions",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "deadline",
                table: "JobDescriptions");

            migrationBuilder.DropColumn(
                name: "employer",
                table: "JobDescriptions");

            migrationBuilder.DropColumn(
                name: "title",
                table: "JobDescriptions");
        }
    }
}
