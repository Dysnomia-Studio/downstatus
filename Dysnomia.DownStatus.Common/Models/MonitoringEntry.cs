using Dysnomia.DownStatus.Common.Enums;

namespace Dysnomia.DownStatus.Common.Models {
	public class MonitoringEntry {
		public string AppId { get; set; }
		public App App { get; set; }
		public string Name { get; set; }
		public string Target { get; set; }
		public MonitoringType Type { get; set; }
		public List<MonitoringEntryHistoryEntry> History { get; set; }
	}
}
