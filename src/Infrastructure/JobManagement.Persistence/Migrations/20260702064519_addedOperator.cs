using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addedOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OperatorId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Operators",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operators", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_OperatorId",
                table: "Sales",
                column: "OperatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Operators_OperatorId",
                table: "Sales",
                column: "OperatorId",
                principalTable: "Operators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Operators_OperatorId",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Operators");

            migrationBuilder.DropIndex(
                name: "IX_Sales_OperatorId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "OperatorId",
                table: "Sales");
        }
    }
}
