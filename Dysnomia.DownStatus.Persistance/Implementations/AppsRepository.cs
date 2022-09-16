using Dysnomia.DownStatus.Persistance.Interfaces;

namespace Dysnomia.DownStatus.Persistance.Implementations {
	public class AppsRepository : IAppsRepository {
		private readonly MonitoringContext context;

		public AppsRepository(MonitoringContext context) {
			this.context = context;
		}
	}
}
