using Dysnomia.DownStatus.Common.Models;

namespace Dysnomia.DownStatus.Common.TransferObjects {
	public class MinimalAppStatusDto {
		public string AppId { get; init; }
		public string AppName { get; init; }
		public string Status { get; init; }
		public string? Logo { get; set; }

		public MinimalAppStatusDto() { }

		public static MinimalAppStatusDto FromModel(MonitoringEntryHistoryEntry entry, App app) {
			return new MinimalAppStatusDto {
				AppId = entry.MonitoringEntryAppId,
				AppName = app.Name,
				Status = entry.Status.ToString(),
				Logo = app.Logo != null ? $"/image/{app.Key}" : null

			};
		}
	}
}
