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

		public IEnumerable<MonitoringEntryHistoryEntry?> GetEntriesForHomepage(int amount) {
			return context.MonitoringHistory
				.GroupBy(x => x.MonitoringEntryAppId, (_, x) => x.OrderByDescending(x => x.Date).FirstOrDefault())
				.AsEnumerable()
				.OrderByDescending(x => x.Status)
				.ThenByDescending(x => x.Date)
				.Take(amount)
				.ToList();
		}
	}
}
