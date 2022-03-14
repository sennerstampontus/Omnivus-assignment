using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Omnivus.Migrations
{
    public partial class Changenames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUserAddresses",
                table: "AspNetUserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetAddresses",
                table: "AspNetAddresses");

            migrationBuilder.RenameTable(
                name: "AspNetUserAddresses",
                newName: "UserAddresses");

            migrationBuilder.RenameTable(
                name: "AspNetAddresses",
                newName: "Addresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAddresses",
                table: "UserAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "UserAddresses",
                newName: "AspNetUserAddresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "AspNetAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUserAddresses",
                table: "AspNetUserAddresses",
                columns: new[] { "UserId", "AddressId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetAddresses",
                table: "AspNetAddresses",
                column: "Id");
        }
    }
}
