using Dysnomia.DownStatus.Common.Models;
using Dysnomia.DownStatus.Persistance.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Dysnomia.DownStatus.Persistance.Implementations {
	public class AppsRepository : IAppsRepository {
		private readonly MonitoringContext context;

		public AppsRepository(MonitoringContext context) {
			this.context = context;
		}

		public Task<bool> Exists(string key) {
			return context.Apps.AnyAsync(x => x.Key == key);
		}

		public Task<App?> GetByKey(string key) {
			return context.Apps.FirstOrDefaultAsync(x => x.Key == key);
		}

		public Task<App?> GetByKeyWithSubEntities(string key) {
			return context.Apps.Include(x => x.MonitoringEntries).ThenInclude(x => x.History).FirstOrDefaultAsync(x => x.Key == key);
		}

		public async Task<string?> GetImageSrc(string key) {
			return (await GetByKey(key))?.Logo;
		}
	}
}
