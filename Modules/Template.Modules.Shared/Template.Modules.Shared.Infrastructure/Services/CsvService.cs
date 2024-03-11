using Template.Modules.Shared.Application.Services;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace Template.Modules.Shared.Infrastructure.Services
{
    public class CsvService : ICsvService
    {
        public IEnumerable<T> GetRecordsFromFile<T>(TextReader reader, Type classMapType, bool fileHasHeaderRow)
        {
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = fileHasHeaderRow
            });
            csv.Context.RegisterClassMap(classMapType);

            return csv.GetRecords<T>().ToList();
        }
    }
}
