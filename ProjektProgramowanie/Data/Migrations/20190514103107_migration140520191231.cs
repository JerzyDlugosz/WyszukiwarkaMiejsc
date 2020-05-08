using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjektProgramowanie.Data.Migrations
{
    public partial class migration140520191231 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Zdjecie",
                table: "Miejsca",
                nullable: true,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Zdjecie",
                table: "Miejsca",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
