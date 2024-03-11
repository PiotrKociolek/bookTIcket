using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Template.Modules.Integrations.Tauron.Core.Responses
{
    public class OutageItem
    {
        public Guid OutageId { get; set; }
        public DateTime Modified { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<int> IdsWWW { get; set; } = new List<int>();
        public List<int> AddressPointIds { get; set; } = new List<int>();
        public string Message { get; set; }
        public int TypeId { get; set; }
        public bool IsActive { get; set; }
    }

    public class OutagesResponse
    {
        public int? AddressPoint { get; set; }
        public List<int> IdsWWW { get; set; } = new List<int>();

        [JsonPropertyName("OutageItems")]
        public List<OutageItem> OutageItems { get; set; } = new List<OutageItem>();
    }
}
