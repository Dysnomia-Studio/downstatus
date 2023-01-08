using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dysnomia.DownStatus.Persistance.Migrations {
	public partial class Typo : Migration {
		protected override void Up(MigrationBuilder migrationBuilder) {
			migrationBuilder.RenameColumn(
				name: "status",
				table: "MonitoringHistory",
				newName: "Status");
		}

		protected override void Down(MigrationBuilder migrationBuilder) {
			migrationBuilder.RenameColumn(
				name: "Status",
				table: "MonitoringHistory",
				newName: "status");
		}
	}
}
