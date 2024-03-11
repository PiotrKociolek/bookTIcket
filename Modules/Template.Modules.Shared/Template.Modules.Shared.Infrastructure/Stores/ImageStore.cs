using Template.Modules.Shared.Application.Services;
using Template.Modules.Shared.Application.Settings;
using Template.Modules.Shared.Application.Stores;
using Template.Modules.Shared.Core.Exceptions;

namespace Template.Modules.Shared.Infrastructure.Stores
{
    public class ImageStore : IImageStore
    {
        private readonly FileStoreSettings _fileStoreSettings;
        private readonly IMimeDetectionService _mimeDetectionService;

        public ImageStore(
            FileStoreSettings fileStoreSettings,
            IMimeDetectionService mimeDetectionService)
        {
            _fileStoreSettings = fileStoreSettings;
            _mimeDetectionService = mimeDetectionService;
        }

        public async Task<(byte[] content, string mimeType)> GetAsync(string fileIdentifier)
        {
            try
            {
                var path = fileIdentifier.Contains("Assets") ? $"{fileIdentifier}" : $"{_fileStoreSettings.ImagesBasePath}{fileIdentifier}";
                var content = await File.ReadAllBytesAsync(path);
                return (content, _mimeDetectionService.GetMimeType(content));
            }
            catch
            { 
                return (null, string.Empty);
            }
        }

        public async Task<string> SaveAsync(byte[] fileContent)
        {
            if (!_mimeDetectionService.IsImageMimeType(_mimeDetectionService.GetMimeType(fileContent)))
            {
                throw new BusinessException("invalid_image", "invalid image");
            }
            var identifier = Guid.NewGuid()
                .ToString();
            
            await File.WriteAllBytesAsync($"{_fileStoreSettings.ImagesBasePath}{identifier}", fileContent);
            return identifier;
        }

        public string GetUrl(string scheme, string host, string imageId)
        {
            return @$"{scheme}://{host}/files/image/{imageId}";
        }
    }
}
