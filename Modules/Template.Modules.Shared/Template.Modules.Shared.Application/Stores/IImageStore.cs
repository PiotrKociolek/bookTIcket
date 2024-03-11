namespace Template.Modules.Shared.Application.Stores
{
    public interface IImageStore
    {
        Task<string> SaveAsync(byte[] itemData);
        Task<(byte[] content, string mimeType)> GetAsync(string fileIdentifier);
        string GetUrl(string scheme, string host, string fileIdentifier);
    }
}
