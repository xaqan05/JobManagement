using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameSkillsToCommonSkills : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerSkills_Skills_SkillId",
                table: "JobSeekerSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "CommonSkills");

            migrationBuilder.RenameIndex(
                name: "IX_Skills_Name",
                table: "CommonSkills",
                newName: "IX_CommonSkills_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CommonSkills",
                table: "CommonSkills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerSkills_CommonSkills_SkillId",
                table: "JobSeekerSkills",
                column: "SkillId",
                principalTable: "CommonSkills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekerSkills_CommonSkills_SkillId",
                table: "JobSeekerSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CommonSkills",
                table: "CommonSkills");

            migrationBuilder.RenameTable(
                name: "CommonSkills",
                newName: "Skills");

            migrationBuilder.RenameIndex(
                name: "IX_CommonSkills_Name",
                table: "Skills",
                newName: "IX_Skills_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekerSkills_Skills_SkillId",
                table: "JobSeekerSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
