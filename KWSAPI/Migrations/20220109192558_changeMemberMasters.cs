using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KWSAPI.Migrations
{
    public partial class changeMemberMasters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentName",
                table: "MemberMasters",
                newName: "MemberName");

            migrationBuilder.AddColumn<string>(
                name: "Roll",
                table: "MemberMasters",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Roll",
                table: "MemberMasters");

            migrationBuilder.RenameColumn(
                name: "MemberName",
                table: "MemberMasters",
                newName: "StudentName");
        }
    }
}
