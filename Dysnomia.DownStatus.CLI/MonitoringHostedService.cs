using Dysnomia.DownStatus.Business.Interfaces;

using Microsoft.Extensions.Hosting;

using System.Timers;

namespace Dysnomia.DownStatus.CLI {
	public class MonitoringHostedService : IHostedService {
		private const int TIMER_INTERVAL = 5_000; // 5 seconds
		private const int OLDEST_ITEMS_AMOUNT = 5;

		private readonly IMonitoringService monitoringService;

		private System.Timers.Timer monitoringTimer;

		public MonitoringHostedService(IMonitoringService monitoringService) {
			this.monitoringService = monitoringService;
		}

		private void DoMonitoring(object state, ElapsedEventArgs eventData) {
			monitoringService.UpdateOldestEntries(OLDEST_ITEMS_AMOUNT);
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.Run(() => {
				monitoringTimer = new System.Timers.Timer(TIMER_INTERVAL);
				monitoringTimer.Elapsed += DoMonitoring;
				monitoringTimer.AutoReset = true;
				monitoringTimer.Enabled = true;
			}, cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.Run(() => {
				monitoringTimer.Stop();
			}, cancellationToken);
		}
	}
}
