using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmnivusMvcWebsite.Migrations
{
    public partial class imagecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Profiles");
        }
    }
}
