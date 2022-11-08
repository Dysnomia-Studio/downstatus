using Dysnomia.DownStatus.Business.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Dysnomia.DownStatus.WebApp.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class ImageController : ControllerBase {
		private readonly IImagesService _imagesService;
		public ImageController(IImagesService imagesService) {
			_imagesService = imagesService;
		}

		[HttpGet("{serviceKey}")]
		[ResponseCache(Duration = 3600)] // Cache this picture for an hour
										 // TODO: serverside cache
		public async Task<IActionResult> GetImage(string serviceKey) {
			try {
				(byte[] content, string contentType, string name) = await _imagesService.GetImageFromServiceKey(serviceKey);

				return File(content, contentType);
			} catch {
				return NotFound();
			}
		}
	}
}
