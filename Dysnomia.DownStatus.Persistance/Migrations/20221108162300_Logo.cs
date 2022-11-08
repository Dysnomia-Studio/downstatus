using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dysnomia.DownStatus.Persistance.Migrations
{
    public partial class Logo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Apps",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Apps");
        }
    }
}
