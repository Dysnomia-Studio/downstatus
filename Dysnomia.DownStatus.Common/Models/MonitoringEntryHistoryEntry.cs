using Dysnomia.DownStatus.Common.Enums;

namespace Dysnomia.DownStatus.Common.Models {
	public class MonitoringEntryHistoryEntry {
		public string MonitoringEntryAppId { get; set; }
		public string MonitoringEntryName { get; set; }
		public MonitoringEntry MonitoringEntry { get; set; }
		public DateTime Date { get; set; }
		public HealthStatus Status { get; set; }
		public string Message { get; set; }
	}
}
