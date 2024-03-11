using CsvHelper.Configuration;
using Template.Modules.Core.Application.Models.Records;

namespace Template.Modules.Core.Application.CsvClassMaps
{
    public class SampleRecordCsvClassMap : ClassMap<SampleRecord>
    {
        public SampleRecordCsvClassMap()
        {   
            Map(m => m.Value1).Index(0);
            Map(m => m.Value2).Index(1);
            Map(m => m.Value3).Index(2);
        }
    }
}
