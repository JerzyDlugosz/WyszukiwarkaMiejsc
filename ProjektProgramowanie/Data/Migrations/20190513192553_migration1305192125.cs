using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektProgramowanie.Data.Migrations
{
    public partial class migration1305192125 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Autor",
                table: "Miejsca",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Miejsca",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Autor",
                table: "Miejsca");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Miejsca");
        }
    }
}
