using Dysnomia.DownStatus.Common.Models;
using Dysnomia.DownStatus.Persistance.Interfaces;

namespace Dysnomia.DownStatus.Persistance.Implementations {
	public class MonitoringEntryHistoryEntriesRepository : IMonitoringEntryHistoryEntriesRepository {
		private readonly MonitoringContext context;

		public MonitoringEntryHistoryEntriesRepository(MonitoringContext context) {
			this.context = context;
		}

		public void AppendToHistoryWithoutSaving(MonitoringEntryHistoryEntry entry) {
			context.MonitoringHistory.Add(entry);
		}

		public async Task ApplyHistoryChanges() {
			await context.SaveChangesAsync();
		}
	}
}
