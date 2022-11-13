using Dysnomia.DownStatus.Business.Interfaces;

using Microsoft.Extensions.Hosting;

using System.Timers;

namespace Dysnomia.DownStatus.CLI {
	public class MonitoringHostedService : IHostedService {
		private const int CLEAN_TIMER_INTERVAL = 3_600_000; // 1 hour
		private const int MONITORING_TIMER_INTERVAL = 10_000; // 10 seconds
		private const int OLDEST_ITEMS_AMOUNT = 5;

		private readonly IMonitoringService monitoringService;

		private System.Timers.Timer cleanTimer;
		private System.Timers.Timer monitoringTimer;

		public MonitoringHostedService(IMonitoringService monitoringService) {
			this.monitoringService = monitoringService;
		}

		private void DoCleaning(object state, ElapsedEventArgs eventData) {
			monitoringService.CleanUselessEntries();
		}

		private void DoMonitoring(object state, ElapsedEventArgs eventData) {
			monitoringService.UpdateOldestEntries(OLDEST_ITEMS_AMOUNT);
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.Run(() => {
				cleanTimer = new System.Timers.Timer(CLEAN_TIMER_INTERVAL);
				cleanTimer.Elapsed += DoCleaning;
				cleanTimer.AutoReset = true;
				cleanTimer.Enabled = true;

				monitoringTimer = new System.Timers.Timer(MONITORING_TIMER_INTERVAL);
				monitoringTimer.Elapsed += DoMonitoring;
				monitoringTimer.AutoReset = true;
				monitoringTimer.Enabled = true;
			}, cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.Run(() => {
				cleanTimer.Stop();
				monitoringTimer.Stop();
			}, cancellationToken);
		}
	}
}
