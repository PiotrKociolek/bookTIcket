using System.Net;

namespace Template.Modules.Shared.Core.Exceptions
{
    public class HandledResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}
