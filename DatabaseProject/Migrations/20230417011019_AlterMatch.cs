using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseProject.Migrations
{
    /// <inheritdoc />
    public partial class AlterMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Matchs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Matchs",
                newName: "Competitions");
        }
    }
}
