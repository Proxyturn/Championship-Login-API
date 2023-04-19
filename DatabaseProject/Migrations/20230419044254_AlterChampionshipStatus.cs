using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseProject.Migrations
{
    /// <inheritdoc />
    public partial class AlterChampionshipStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Championships",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Championships");
        }
    }
}
