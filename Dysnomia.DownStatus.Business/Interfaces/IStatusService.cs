using Dysnomia.DownStatus.Common.TransferObjects;

namespace Dysnomia.DownStatus.Business.Interfaces {
	public interface IStatusService {
		Task<AppStatusDto?> GetByKey(string key);
		Task<IEnumerable<MinimalAppStatusDto>> GetStatusForHomePage();
		Task<IEnumerable<MinimalAppStatusDto>> Search(string str);
	}
}
