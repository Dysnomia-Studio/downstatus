using Dysnomia.DownStatus.Common.Enums;

namespace Dysnomia.DownStatus.Monitoring {
	public interface IMonitoringJob {
		Task<(HealthStatus, string)> IsAlive(string url);
		MonitoringType GetMonitoringType();
	}
}
