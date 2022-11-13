using Dysnomia.DownStatus.Common.Models;

namespace Dysnomia.DownStatus.Persistance.Interfaces {
	public interface IMonitoringEntryHistoryEntriesRepository {
		void AppendToHistoryWithoutSaving(MonitoringEntryHistoryEntry entry);
		Task ApplyHistoryChanges();
		Task CleanUselessEntries();
		IEnumerable<MonitoringEntryHistoryEntry?> GetEntriesForHomepage(int amount);
		IEnumerable<MonitoringEntryHistoryEntry> Search(string str, int amount);
	}
}
