using Dysnomia.DownStatus.Common.Models;

namespace Dysnomia.DownStatus.Persistance.Interfaces {
	public interface IMonitoringEntriesRepository {
		IAsyncEnumerable<MonitoringEntry> GetOldestUpdatedEntries(int amount);
	}
}
