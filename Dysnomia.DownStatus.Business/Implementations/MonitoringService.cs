using Dysnomia.DownStatus.Business.Interfaces;
using Dysnomia.DownStatus.Common.Enums;
using Dysnomia.DownStatus.Common.Models;
using Dysnomia.DownStatus.Monitoring;
using Dysnomia.DownStatus.Persistance.Interfaces;

namespace Dysnomia.DownStatus.Business.Implementations {
	public class MonitoringService : IMonitoringService {
		private readonly IAppsRepository appsRepository;
		private readonly IMonitoringEntriesRepository monitoringEntriesRepository;
		private readonly IMonitoringEntryHistoryEntriesRepository monitoringEntryHistoryRepository;
		private readonly IEnumerable<IMonitoringJob> monitoringJobs;

		public MonitoringService(IAppsRepository appsRepository, IMonitoringEntriesRepository monitoringEntriesRepository, IMonitoringEntryHistoryEntriesRepository monitoringEntryHistoryRepository, IEnumerable<IMonitoringJob> monitoringJobs) {
			this.appsRepository = appsRepository;
			this.monitoringEntriesRepository = monitoringEntriesRepository;
			this.monitoringEntryHistoryRepository = monitoringEntryHistoryRepository;
			this.monitoringJobs = monitoringJobs;
		}

		private Task<(HealthStatus, string)> Monitore(MonitoringEntry entry) {
			var job = monitoringJobs.First(x => x.GetMonitoringType() == entry.Type);

			return job.IsAlive(entry.Target);
		}

		public async Task UpdateOldestEntries(int amount) {
			try {
				await foreach (var entry in monitoringEntriesRepository.GetOldestUpdatedEntries(amount)) {
					var (status, message) = await Monitore(entry);

					monitoringEntryHistoryRepository.AppendToHistoryWithoutSaving(new MonitoringEntryHistoryEntry() {
						MonitoringEntryAppId = entry.AppId,
						MonitoringEntryName = entry.Name,
						Date = DateTime.UtcNow,
						Status = status,
						Message = message
					});
				}

				await monitoringEntryHistoryRepository.ApplyHistoryChanges();
			} catch (Exception e) {
				var test = "";
			}
		}
	}
}
