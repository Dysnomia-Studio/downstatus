namespace Dysnomia.DownStatus.Business.Interfaces {
	public interface IImagesService {
		Task<(byte[], string, string)> GetImageFromServiceKey(string key);
	}
}
