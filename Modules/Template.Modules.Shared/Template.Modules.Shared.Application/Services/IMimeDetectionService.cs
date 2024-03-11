namespace Template.Modules.Shared.Application.Services
{
    public interface IMimeDetectionService
    {
        string GetMimeType(byte[] fileContent);
        bool IsImageMimeType(string mimeType);
    }
}
