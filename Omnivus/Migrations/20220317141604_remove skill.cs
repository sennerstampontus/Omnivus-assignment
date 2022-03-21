using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Omnivus.Migrations
{
    public partial class removeskill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSkill");

            migrationBuilder.DropTable(
                name: "AppProfileSkill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "AppSkill",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppProfileSkillProfileId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AppProfileSkillSkillId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SkillName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppSkill_AppProfile_AppProfileId",
                        column: x => x.AppProfileId,
                        principalTable: "AppProfile",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppSkill_AppProfileSkill_AppProfileSkillProfileId_AppProfileSkillSkillId",
                        columns: x => new { x.AppProfileSkillProfileId, x.AppProfileSkillSkillId },
                        principalTable: "AppProfileSkill",
                        principalColumns: new[] { "ProfileId", "SkillId" });
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppSkill_AppProfileId",
                table: "AppSkill",
                column: "AppProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AppSkill_AppProfileSkillProfileId_AppProfileSkillSkillId",
                table: "AppSkill",
                columns: new[] { "AppProfileSkillProfileId", "AppProfileSkillSkillId" });
        }
    }
}
