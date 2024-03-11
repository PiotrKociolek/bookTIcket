using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Modules.Integrations.Tauron.Core.Responses
{
    public class GetStreetGAIDResponse
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public int GAID { get; set; }
    }
}
