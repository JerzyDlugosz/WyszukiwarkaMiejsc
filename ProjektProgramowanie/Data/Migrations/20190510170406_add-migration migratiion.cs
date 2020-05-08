using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektProgramowanie.Data.Migrations
{
    public partial class addmigrationmigratiion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Zdjecie",
                table: "Miejsca",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zdjecie",
                table: "Miejsca");
        }
    }
}
