namespace Dysnomia.DownStatus.Business.Interfaces {
	public interface IMonitoringService {
		Task CleanUselessEntries();
		Task UpdateOldestEntries(int amount);
	}
}
