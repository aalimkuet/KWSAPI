using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KWSAPI.Migrations
{
    public partial class nameUserAuthentoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAuthens",
                table: "UserAuthens");

            migrationBuilder.RenameTable(
                name: "UserAuthens",
                newName: "User");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "UserAuthens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAuthens",
                table: "UserAuthens",
                column: "Id");
        }
    }
}
