using Dysnomia.DownStatus.Common.Models;

namespace Dysnomia.DownStatus.Persistance.Interfaces {
	public interface IAppsRepository {
		Task<App?> GetByKey(string key);
		Task<App?> GetByKeyWithSubEntities(string key);
		Task<string?> GetImageSrc(string key);
	}
}
