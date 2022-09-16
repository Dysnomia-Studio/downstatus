using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dysnomia.DownStatus.Persistance.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringEntry",
                columns: table => new
                {
                    AppId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Target = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringEntry", x => new { x.AppId, x.Name });
                    table.ForeignKey(
                        name: "FK_MonitoringEntry_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringHistory",
                columns: table => new
                {
                    MonitoringEntryAppId = table.Column<string>(type: "text", nullable: false),
                    MonitoringEntryName = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringHistory", x => new { x.MonitoringEntryAppId, x.MonitoringEntryName, x.Date });
                    table.ForeignKey(
                        name: "FK_MonitoringHistory_MonitoringEntry_MonitoringEntryAppId_Moni~",
                        columns: x => new { x.MonitoringEntryAppId, x.MonitoringEntryName },
                        principalTable: "MonitoringEntry",
                        principalColumns: new[] { "AppId", "Name" },
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitoringHistory");

            migrationBuilder.DropTable(
                name: "MonitoringEntry");

            migrationBuilder.DropTable(
                name: "Apps");
        }
    }
}
