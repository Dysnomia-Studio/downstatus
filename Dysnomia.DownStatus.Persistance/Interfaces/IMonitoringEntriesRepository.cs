using Dysnomia.DownStatus.Common.Models;

namespace Dysnomia.DownStatus.Persistance.Interfaces {
	public interface IMonitoringEntriesRepository {
		Task<IEnumerable<MonitoringEntry>> GetOldestUpdatedEntries(int amount);
	}
}
