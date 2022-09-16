using Microsoft.Extensions.DependencyInjection;

namespace Dysnomia.DownStatus.Monitoring {
	public static class ConfigureServicesHelper {
		public static void ConfigureJobsServices(this IServiceCollection services) {
			services.AddTransient<IMonitoringJob, AtlassianStatusPageJob>();
			services.AddTransient<IMonitoringJob, StatusMonitoringJob>();
		}
	}
}
