using Dysnomia.DownStatus.Persistance.Implementations;
using Dysnomia.DownStatus.Persistance.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Dysnomia.DownStatus.Persistance {
	public static class ConfigureServicesHelper {
		public static void ConfigureRepositories(this IServiceCollection services) {
			services.AddSingleton<MonitoringContext>();

			services.AddTransient<IAppsRepository, AppsRepository>();
			services.AddTransient<IMonitoringEntriesRepository, MonitoringEntriesRepository>();
			services.AddTransient<IMonitoringEntryHistoryEntriesRepository, MonitoringEntryHistoryEntriesRepository>();
		}
	}
}
