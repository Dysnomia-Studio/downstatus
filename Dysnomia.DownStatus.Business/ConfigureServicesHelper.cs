﻿using Dysnomia.DownStatus.Business.Implementations;
using Dysnomia.DownStatus.Business.Interfaces;

using Microsoft.Extensions.DependencyInjection;

namespace Dysnomia.DownStatus.Business {
	public static class ConfigureServicesHelper {
		public static void ConfigureBusinessServices(this IServiceCollection services) {
			services.AddTransient<IMonitoringService, MonitoringService>();
		}
	}
}
