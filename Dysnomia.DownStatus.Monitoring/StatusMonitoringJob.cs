using Dysnomia.DownStatus.Common.Enums;

using System.Net;

namespace Dysnomia.DownStatus.Monitoring {
	public class StatusMonitoringJob : IMonitoringJob {
		private const HttpStatusCode expectedStatusCode = HttpStatusCode.OK;
		private readonly IHttpClientFactory factory;

		public StatusMonitoringJob(IHttpClientFactory factory) {
			this.factory = factory;
		}

		public async Task<(HealthStatus, string)> IsAlive(string url) {
			using var client = factory.CreateClient();

			var response = await client.GetAsync(url);

			if (response.StatusCode == expectedStatusCode) {
				return (HealthStatus.Alive, "");
			}

			return (HealthStatus.Unhealthy, response.StatusCode.ToString());
		}

		public MonitoringType GetMonitoringType() {
			return MonitoringType.Status;
		}
	}
}
