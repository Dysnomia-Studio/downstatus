using Dysnomia.DownStatus.Common.Models;
using Dysnomia.DownStatus.Persistance.Interfaces;

using Microsoft.EntityFrameworkCore;

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

		public async Task CleanUselessEntriesForApp(string appId) {
			await context.Database.ExecuteSqlInterpolatedAsync($@"
				DELETE
				FROM ""MonitoringHistory"" as current_history
				USING ""MonitoringHistory"" as previous_history, ""MonitoringHistory"" as next_history
				WHERE 
					previous_history.""MonitoringEntryAppId"" = {appId} AND
					previous_history.""MonitoringEntryAppId"" = current_history.""MonitoringEntryAppId"" AND 
					previous_history.""MonitoringEntryName"" = current_history.""MonitoringEntryName"" AND
					previous_history.""Status"" = current_history.""Status"" AND
					previous_history.""Message"" = current_history.""Message"" AND
					previous_history.""Date"" = (
						SELECT MAX(tableA.""Date"") FROM ""MonitoringHistory"" as tableA 
						WHERE tableA.""Date"" < current_history.""Date""
						AND previous_history.""MonitoringEntryAppId"" = tableA.""MonitoringEntryAppId""
						AND previous_history.""MonitoringEntryName"" = tableA.""MonitoringEntryName""
					) AND
					next_history.""MonitoringEntryAppId"" = current_history.""MonitoringEntryAppId"" AND 
					next_history.""MonitoringEntryName"" = current_history.""MonitoringEntryName"" AND
					next_history.""Status"" = current_history.""Status"" AND
					next_history.""Message"" = current_history.""Message"" AND
					next_history.""Date"" = (
						SELECT MIN(tableB.""Date"")
						FROM ""MonitoringHistory"" as tableB
						WHERE tableB.""Date"" > current_history.""Date""
						AND next_history.""MonitoringEntryAppId"" = tableB.""MonitoringEntryAppId""
						AND next_history.""MonitoringEntryName"" = tableB.""MonitoringEntryName""
					)
			");
		}

		public async Task CleanUselessEntries() {
			await context.Database.ExecuteSqlRawAsync(@"
				DELETE
				FROM ""MonitoringHistory"" as current_history
				USING ""MonitoringHistory"" as previous_history, ""MonitoringHistory"" as next_history
				WHERE 
					previous_history.""MonitoringEntryAppId"" = current_history.""MonitoringEntryAppId"" AND 
					previous_history.""MonitoringEntryName"" = current_history.""MonitoringEntryName"" AND
					previous_history.""Status"" = current_history.""Status"" AND
					previous_history.""Message"" = current_history.""Message"" AND
					previous_history.""Date"" = (
						SELECT MAX(tableA.""Date"") FROM ""MonitoringHistory"" as tableA 
						WHERE tableA.""Date"" < current_history.""Date""
						AND previous_history.""MonitoringEntryAppId"" = tableA.""MonitoringEntryAppId""
						AND previous_history.""MonitoringEntryName"" = tableA.""MonitoringEntryName""
					) AND
					next_history.""MonitoringEntryAppId"" = current_history.""MonitoringEntryAppId"" AND 
					next_history.""MonitoringEntryName"" = current_history.""MonitoringEntryName"" AND
					next_history.""Status"" = current_history.""Status"" AND
					next_history.""Message"" = current_history.""Message"" AND
					next_history.""Date"" = (
						SELECT MIN(tableB.""Date"")
						FROM ""MonitoringHistory"" as tableB
						WHERE tableB.""Date"" > current_history.""Date""
						AND next_history.""MonitoringEntryAppId"" = tableB.""MonitoringEntryAppId""
						AND next_history.""MonitoringEntryName"" = tableB.""MonitoringEntryName""
					)
			");
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

		public IEnumerable<MonitoringEntryHistoryEntry> Search(string str, int amount) {
			return context.MonitoringHistory
				.Include(x => x.MonitoringEntry)
				.ThenInclude(x => x.App)
				.Where(x => EF.Functions.ILike(x.MonitoringEntry.App.Name, $"%{str}%"))
				.GroupBy(x => x.MonitoringEntryAppId, (_, x) => x.OrderByDescending(x => x.Date).FirstOrDefault())
				.AsEnumerable()
				.OrderByDescending(x => x.Status)
				.ThenByDescending(x => x.Date)
				.Take(amount)
				.ToList();
		}
	}
}
