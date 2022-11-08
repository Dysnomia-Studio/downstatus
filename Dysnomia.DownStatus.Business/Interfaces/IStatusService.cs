using Dysnomia.DownStatus.Common.TransferObjects;

namespace Dysnomia.DownStatus.Business.Interfaces {
	public interface IStatusService {
		Task<IEnumerable<MinimalAppStatusDto>> GetStatusForHomePage();
		Task<IEnumerable<MinimalAppStatusDto>> Search(string str);
	}
}
