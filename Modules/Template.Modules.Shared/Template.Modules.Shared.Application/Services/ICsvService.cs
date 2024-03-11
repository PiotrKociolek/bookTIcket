namespace Template.Modules.Shared.Application.Services
{
    public interface ICsvService
    {
        IEnumerable<T> GetRecordsFromFile<T>(TextReader reader, Type classMapType, bool fileHasHeaderRow);
    }
}
