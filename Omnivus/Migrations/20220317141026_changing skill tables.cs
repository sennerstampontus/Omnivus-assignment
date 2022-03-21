using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Omnivus.Migrations
{
    public partial class changingskilltables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSkill_AspNetUsers_AppUserId",
                table: "AppSkill");

            migrationBuilder.DropIndex(
                name: "IX_AppSkill_AppUserId",
                table: "AppSkill");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "AppSkill",
                newName: "AppProfileSkillSkillId");

            migrationBuilder.AddColumn<string>(
                name: "AppProfileSkillProfileId",
                table: "AppSkill",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AppProfileSkill",
                columns: table => new
                {
                    ProfileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SkillId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppProfileSkill", x => new { x.ProfileId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_AppProfileSkill_AppProfile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "AppProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSkill_AppProfileSkillProfileId_AppProfileSkillSkillId",
                table: "AppSkill",
                columns: new[] { "AppProfileSkillProfileId", "AppProfileSkillSkillId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppSkill_AppProfileSkill_AppProfileSkillProfileId_AppProfileSkillSkillId",
                table: "AppSkill",
                columns: new[] { "AppProfileSkillProfileId", "AppProfileSkillSkillId" },
                principalTable: "AppProfileSkill",
                principalColumns: new[] { "ProfileId", "SkillId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSkill_AppProfileSkill_AppProfileSkillProfileId_AppProfileSkillSkillId",
                table: "AppSkill");

            migrationBuilder.DropTable(
                name: "AppProfileSkill");

            migrationBuilder.DropIndex(
                name: "IX_AppSkill_AppProfileSkillProfileId_AppProfileSkillSkillId",
                table: "AppSkill");

            migrationBuilder.DropColumn(
                name: "AppProfileSkillProfileId",
                table: "AppSkill");

            migrationBuilder.RenameColumn(
                name: "AppProfileSkillSkillId",
                table: "AppSkill",
                newName: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSkill_AppUserId",
                table: "AppSkill",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSkill_AspNetUsers_AppUserId",
                table: "AppSkill",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
