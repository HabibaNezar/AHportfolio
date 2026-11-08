using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortfolioAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAboutFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AboutWords",
                table: "Abouts",
                newName: "About");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SkillCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SkillCategories");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Experiences");

            migrationBuilder.RenameColumn(
                name: "About",
                table: "Abouts",
                newName: "AboutWords");
        }
    }
}
