using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KWSAPI.Migrations
{
    public partial class AddsecretkeytoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secretkey",
                table: "UserAuthens",
                type: "varchar(300)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secretkey",
                table: "UserAuthens");
        }
    }
}
