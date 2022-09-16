using Dysnomia.DownStatus.Common.Enums;

using System.Text.Json;

namespace Dysnomia.DownStatus.Monitoring {
	public class AtlassianStatusPageJob : IMonitoringJob {
		private readonly IHttpClientFactory factory;

		public AtlassianStatusPageJob(IHttpClientFactory factory) {
			this.factory = factory;
		}

		public async Task<(HealthStatus, string)> IsAlive(string url) {
			using var client = factory.CreateClient();

			var response = await client.GetAsync(url + "/api/v2/status.json");

			var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(await response.Content.ReadAsStreamAsync());

			if (data["status"]["indicator"] == "none") {
				return (HealthStatus.Alive, "");
			}

			if (data["status"]["indicator"] == "minor") {
				return (HealthStatus.Degraded, data["status"]["description"]);
			}

			return (HealthStatus.Unhealthy, data["status"]["description"]);
		}

		public MonitoringType GetMonitoringType() {
			return MonitoringType.AtlassianStatusPage;
		}
	}
}
