using Template.Modules.Shared.Application.Services;
using MimeDetective;
using System.Text;

namespace Template.Modules.Shared.Infrastructure.Services
{
    public class MimeDetectionService : IMimeDetectionService
    {
        private readonly ContentInspector _inspector;

        public MimeDetectionService()
        {
            _inspector = new ContentInspectorBuilder
                {
                    Definitions = MimeDetective.Definitions.Default.All()
                }
            .Build();
        }
        public string GetMimeType(byte[] fileContent)
        {
            return IsSvgMimeType(fileContent) ?
                "image/svg+xml" :
            _inspector.Inspect(fileContent)
            .ByMimeType()
            .FirstOrDefault()?.MimeType ?? "application/octet-stream";
        }

        public bool IsImageMimeType(string mimeType)
        {
            return !string.IsNullOrWhiteSpace(mimeType) && (new[] { "image/bmp", "image/jpeg", "image/x-png", "image/png", "image/gif", "image/svg+xml" })
                .Contains(mimeType.ToLower());
        }

        private static bool IsSvgMimeType(byte[] fileContent)
        {
            try
            {
                var textContent = fileContent.Length > 1024 ?
                    Encoding.UTF8.GetString(fileContent, 0, 1024) :
                    Encoding.UTF8.GetString(fileContent);

                return textContent.Contains("<svg");
            }
            catch 
            {
                return false;
            }
        }
    }
}
