namespace Dysnomia.DownStatus.Business.Interfaces {
	public interface IMonitoringService {
		Task UpdateOldestEntries(int amount);
	}
}
