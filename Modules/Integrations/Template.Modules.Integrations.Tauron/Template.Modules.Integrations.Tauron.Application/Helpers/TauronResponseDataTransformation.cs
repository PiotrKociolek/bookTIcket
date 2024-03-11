using Template.Modules.Integrations.Tauron.Application.Dto;
using Template.Modules.Integrations.Tauron.Core.Responses;
using Microsoft.Identity.Client;

namespace Template.Modules.Integrations.Tauron.Infrastructure.Helpers
{
    internal static class TauronResponseDataTransformation
    {
        private const string NOWY_SACZ = "w Nowym Sączu";

        /// <summary>
        /// This function mutates the input.
        /// </summary>
        /// <param name="outageItems"></param>
        internal static void TrimCityPrefix(ICollection<OutageDto> outageItems)
        {
            foreach(var outageItem in outageItems)
            {
                var span = outageItem.Message.AsSpan();
                var idx = span.IndexOf(',');
                var prefix = span[..idx];
                if(prefix.SequenceEqual(NOWY_SACZ))
                {
                    var parsedMessage = span[(idx + 1)..].Trim().ToString();
                    outageItem.Message = parsedMessage;
                }
            }
        }
    }
}
