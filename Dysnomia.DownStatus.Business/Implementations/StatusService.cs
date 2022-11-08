using Dysnomia.DownStatus.Business.Interfaces;
using Dysnomia.DownStatus.Common.TransferObjects;
using Dysnomia.DownStatus.Persistance.Interfaces;

namespace Dysnomia.DownStatus.Business.Implementations {
	public class StatusService : IStatusService {
		public const int MAX_RETURN_APPS = 30;

		private readonly IAppsRepository appsRepository;
		private readonly IMonitoringEntriesRepository monitoringEntriesRepository;
		private readonly IMonitoringEntryHistoryEntriesRepository monitoringEntryHistoryRepository;

		public StatusService(IAppsRepository appsRepository, IMonitoringEntriesRepository monitoringEntriesRepository, IMonitoringEntryHistoryEntriesRepository monitoringEntryHistoryRepository) {
			this.appsRepository = appsRepository;
			this.monitoringEntriesRepository = monitoringEntriesRepository;
			this.monitoringEntryHistoryRepository = monitoringEntryHistoryRepository;
		}

		public async Task<IEnumerable<MinimalAppStatusDto>> GetStatusForHomePage() {
			try {
				List<MinimalAppStatusDto> statuses = new();

				foreach (var entry in monitoringEntryHistoryRepository.GetEntriesForHomepage(MAX_RETURN_APPS)) {
					if (entry == null) {
						continue;
					}

					var app = await appsRepository.GetByKey(entry.MonitoringEntryAppId);
					statuses.Add(MinimalAppStatusDto.FromModel(entry, app));
				}

				return statuses;
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);

				return null;
			}
		}

		public async Task<IEnumerable<MinimalAppStatusDto>> Search(string str) {
			try {
				List<MinimalAppStatusDto> statuses = new();

				foreach (var entry in monitoringEntryHistoryRepository.Search(str, MAX_RETURN_APPS)) {
					if (entry == null) {
						continue;
					}

					var app = await appsRepository.GetByKey(entry.MonitoringEntryAppId);
					statuses.Add(MinimalAppStatusDto.FromModel(entry, app));
				}

				return statuses;
			} catch (Exception e) {
				Console.WriteLine(e.Message);
				Console.WriteLine(e.StackTrace);

				return null;
			}
		}
	}
}
