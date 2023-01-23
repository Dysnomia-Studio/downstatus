using Dysnomia.DownStatus.Business.Interfaces;
using Dysnomia.DownStatus.Persistance.Interfaces;

using Microsoft.Extensions.Caching.Memory;

namespace Dysnomia.DownStatus.Business.Implementations {
    public class ImagesService : IImagesService {
        private readonly IAppsRepository _appsRepository;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _memoryCache;

        public ImagesService(IAppsRepository appsRepository, HttpClient httpClient, IMemoryCache memoryCache) {
            _appsRepository = appsRepository;
            _httpClient = httpClient;
            _memoryCache = memoryCache;
        }

        private string GetContentTypeFromExtension(string extension) {
            var contentType = "";
            switch (extension.ToLower().Substring(1)) {
                case "bmp":
                    contentType = "image/bmp";
                    break;
                case "jpg":
                case "jpeg":
                    contentType = "image/jpeg";
                    break;
                case "gif":
                    contentType = "image/gif";
                    break;
                case "ico":
                    contentType = "image/vnd.microsoft.icon";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                case "svg":
                    contentType = "image/svg+xml";
                    break;
                case "webp":
                    contentType = "image/webp";
                    break;
            }

            return contentType;
        }

        public async Task<(byte[], string, string)> GetImageFromServiceKey(string key) {
            return await _memoryCache.GetOrCreateAsync<(byte[], string, string)>("img-" + key, async (x) => {
                var src = await _appsRepository.GetImageSrc(key);
                if (src == null) {
                    throw new ArgumentNullException(key);
                }

                HttpResponseMessage response = await _httpClient.GetAsync(src);
                byte[] content = await response.Content.ReadAsByteArrayAsync();

                var extension = Path.GetExtension(src);
                var contentType = GetContentTypeFromExtension(extension);
                var fileName = key + extension;

                return (content, contentType, fileName);
            });
        }
    }
}
