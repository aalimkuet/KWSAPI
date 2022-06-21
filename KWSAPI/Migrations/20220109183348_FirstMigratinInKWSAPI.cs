using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KWSAPI.Migrations
{
    public partial class FirstMigratinInKWSAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PassingYear = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Cgpa = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberMasters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberMasters");
        }
    }
}
