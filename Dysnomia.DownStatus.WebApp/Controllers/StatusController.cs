using Dysnomia.DownStatus.Business.Interfaces;
using Dysnomia.DownStatus.Common.TransferObjects;

using Microsoft.AspNetCore.Mvc;

namespace Dysnomia.DownStatus.WebApp.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class StatusController : ControllerBase {
		private readonly IStatusService _statusService;
		public StatusController(IStatusService statusService) {
			_statusService = statusService;
		}

		[HttpGet("homepage")]
		public async Task<ActionResult<IEnumerable<MinimalAppStatusDto>>> GetStatusForHomePage() {
			var data = await _statusService.GetStatusForHomePage();


			if (data == null) {
				return NoContent();
			}

			return Ok(data);
		}
	}
}
