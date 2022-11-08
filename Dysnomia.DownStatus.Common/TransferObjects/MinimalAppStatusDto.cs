namespace Dysnomia.DownStatus.Common.TransferObjects {
	public class MinimalAppStatusDto {
		public string AppId { get; init; }
		public string AppName { get; init; }
		public string Status { get; init; }
		public string? Logo { get; set; }

		public MinimalAppStatusDto() { }
	}
}
