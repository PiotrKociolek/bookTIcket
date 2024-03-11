using Template.Modules.Shared.Core.Exceptions;

namespace Template.Modules.Shared.Infrastructure.Framework.Exceptions
{
    public interface IExceptionHandler
    {
        HandledResponse HandleError(Exception exception, string remoteIpAddress);
    }
}