using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektProgramowanie.Data.Migrations
{
    public partial class migration2321312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kraj",
                table: "Miejsca",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Trasa",
                table: "Miejsca",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kraj",
                table: "Miejsca");

            migrationBuilder.DropColumn(
                name: "Trasa",
                table: "Miejsca");
        }
    }
}
