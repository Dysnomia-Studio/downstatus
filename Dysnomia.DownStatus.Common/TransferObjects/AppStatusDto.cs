using Dysnomia.DownStatus.Common.Models;

namespace Dysnomia.DownStatus.Common.TransferObjects {
	public class AppStatusDto {
		public string AppId { get; init; }
		public string AppName { get; init; }
		public string? Description { get; init; }
		public string? Logo { get; set; }
		public IEnumerable<AppStatusDtoItem> StatusList { get; set; }
		public IEnumerable<AppStatusDtoTarget> Targets { get; set; }

		public AppStatusDto() { }

		public static AppStatusDto FromModel(App app) {
			var statusList = app.MonitoringEntries.SelectMany(monitoringEntry => monitoringEntry.History.Select(historyEntry => new AppStatusDtoItem {
				Date = historyEntry.Date,
				TargetName = monitoringEntry.Name,
				Status = historyEntry.Status.ToString()
			}));
			statusList = statusList
				.OrderBy(x => x.Date)
				.GroupBy(x => new { x.TargetName, x.Status })
				.Select(x => x.First());

			return new AppStatusDto {
				AppId = app.Key,
				AppName = app.Name,
				Description = app.Description,
				Logo = app.Logo != null ? $"/image/{app.Key}" : null,
				StatusList = statusList.Take(15),
				Targets = app.MonitoringEntries.Select(x => new AppStatusDtoTarget {
					Name = x.Name,
					Target = x.Target,
				})
			};
		}
	}

	public class AppStatusDtoItem {
		public DateTime Date { get; set; }
		public string TargetName { get; set; }
		public string Status { get; set; }
	}

	public class AppStatusDtoTarget {
		public string Name { get; set; }
		public string Target { get; set; }
	}
}
