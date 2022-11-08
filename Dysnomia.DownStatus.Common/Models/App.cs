namespace Dysnomia.DownStatus.Common.Models {
	public class App {
		public string Key { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public string? Website { get; set; }
		public string? Logo { get; set; }
		public IEnumerable<MonitoringEntry> MonitoringEntries { get; set; }
	}
}
