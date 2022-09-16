using Dysnomia.DownStatus.Common.Models;
using Dysnomia.DownStatus.Persistance.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Dysnomia.DownStatus.Persistance.Implementations {
	public class MonitoringEntriesRepository : IMonitoringEntriesRepository {
		private readonly MonitoringContext context;

		public MonitoringEntriesRepository(MonitoringContext context) {
			this.context = context;
		}

		public IAsyncEnumerable<MonitoringEntry> GetOldestUpdatedEntries(int amount) {
			return context.MonitoringEntry
				.Include(x => x.History)
				.OrderByDescending(x => x.History.Max(h => h.Date))
				.Take(amount)
				.AsAsyncEnumerable();
		}
	}
}
