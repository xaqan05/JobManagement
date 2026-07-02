using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LegacyId",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_LegacyId",
                table: "Countries",
                column: "LegacyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Countries_LegacyId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LegacyId",
                table: "Countries");
        }
    }
}
