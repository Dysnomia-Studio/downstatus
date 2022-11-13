using Dysnomia.DownStatus.Business.Interfaces;

using Microsoft.Extensions.Hosting;

using System.Timers;

namespace Dysnomia.DownStatus.CLI {
	public class CleanerHostedService : IHostedService {
		private const int CLEAN_TIMER_INTERVAL = 3_600_000; // 1 hour

		private readonly IMonitoringService monitoringService;

		private System.Timers.Timer cleanTimer;

		public CleanerHostedService(IMonitoringService monitoringService) {
			this.monitoringService = monitoringService;
		}

		private void DoCleaning(object state, ElapsedEventArgs eventData) {
			monitoringService.CleanUselessEntries();
		}

		public Task StartAsync(CancellationToken cancellationToken) {
			return Task.Run(() => {
				cleanTimer = new System.Timers.Timer(CLEAN_TIMER_INTERVAL);
				cleanTimer.Elapsed += DoCleaning;
				cleanTimer.AutoReset = true;
				cleanTimer.Enabled = true;
			}, cancellationToken);
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			return Task.Run(() => {
				cleanTimer.Stop();
			}, cancellationToken);
		}
	}
}
