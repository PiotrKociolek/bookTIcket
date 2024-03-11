using System.Net;
using System.Security.Authentication;
using Template.Modules.Shared.Application.Extensions;
using Template.Modules.Shared.Core.Exceptions;
using Serilog;

namespace Template.Modules.Shared.Infrastructure.Framework.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public HandledResponse HandleError(Exception exception, string remoteIpAddress)
        {
            var exceptionType = exception.GetType();

            HandledResponse response;
            switch (exception)
            {
                case NotFoundException e when exceptionType == typeof(NotFoundException):
                    response = HandleNotFoundException(e);

                    break;

                case ForbiddenException e when exceptionType == typeof(ForbiddenException):
                    response = HandleForbiddenException(e);

                    break;

                case BusinessException e when exceptionType == typeof(BusinessException):
                    response = HandleBusinessException(e);

                    break;  
                    
                case ValidatorException e when exceptionType == typeof(ValidatorException):
                    response = HandleValidationException(e);

                    break;

                case AuthenticationException e when exceptionType == typeof(AuthenticationException):
                    response = HandleAuthenticationException(e);

                    break;

                case UnauthorizedAccessException e when exceptionType == typeof(UnauthorizedAccessException):
                    response = HandleUnauthorizedAccessException(e);

                    break;
                default:
                    response = HandleException(exception);
                    break;
            }

            HandleResponse(response, exception, remoteIpAddress);

            return response;
        }

        private void HandleResponse(HandledResponse response, Exception exception, string remoteIpAddress)
        {
            Log.Logger
                .ForContext("ExceptionId", response.ErrorResponse.ExceptionId)
                .ForContext("RemoteIpAddress", remoteIpAddress)
                .Fatal(exception, exception.Message);
        }

        private HandledResponse HandleNotFoundException(NotFoundException exception)
        {
            var httpResponseCode = HttpStatusCode.NotFound;

            var errors = ErrorToList(exception.Code, exception.Message);

            var result = PrepareHandledResponse(
                httpResponseCode,
                errors);

            return result;
        }

        private static HandledResponse PrepareHandledResponse(
            HttpStatusCode httpResponseCode,
            ICollection<ErrorObject> errors)
        {
            return new HandledResponse()
            {
                HttpStatusCode = httpResponseCode,
                ErrorResponse = new ErrorResponse
                {
                    ExceptionId = Guid.NewGuid(),
                    Errors = errors
                }
            };
        }

        private HandledResponse HandleForbiddenException(ForbiddenException exception)
        {
            var httpResponseCode = HttpStatusCode.Forbidden;

            var errors = ErrorToList(exception.Code, exception.Message);

            var result = PrepareHandledResponse(
                httpResponseCode,
                errors);

            return result;
        }

        private HandledResponse HandleBusinessException(BusinessException exception)
        {
            var httpResponseCode = HttpStatusCode.BadRequest;

            var errors = ErrorToList(exception.Code, exception.Message);

            var result = PrepareHandledResponse(
                httpResponseCode,
                errors);

            return result;
        }
        
        private HandledResponse HandleValidationException(ValidatorException exception)
        {
            var httpResponseCode = HttpStatusCode.BadRequest;

            var result = PrepareHandledResponse(
                httpResponseCode,
                exception.Errors);

            return result;
        }

        private HandledResponse HandleForbiddenResponse(Exception exception)
        {
            var errors = ErrorToList(ExceptionExtensions.UnauthorizedReturnedCode, ExceptionExtensions.UnauthorizedReturnedMessage);

            var result = PrepareHandledResponse(
                HttpStatusCode.Forbidden,
                errors);

            return result;
        }

        private HandledResponse HandleAuthenticationException(AuthenticationException exception)
        {
            var httpResponseCode = HttpStatusCode.Unauthorized;

            var errors = ErrorToList(ExceptionExtensions.UnauthenticatedReturnedCode, ExceptionExtensions.UnauthenticatedReturnedMessage);

            var result = PrepareHandledResponse(
                httpResponseCode,
                errors);

            return result;
        }

        private HandledResponse HandleUnauthorizedAccessException(UnauthorizedAccessException exception)
        {
            return HandleForbiddenResponse(exception);
        }

        private HandledResponse HandleException(Exception exception)
        {
            var httpResponseCode = HttpStatusCode.InternalServerError;

            var errors = ErrorToList(ExceptionExtensions.ExceptionReturnedCode, ExceptionExtensions.ExceptionReturnedMessage);

            var result = PrepareHandledResponse(
                httpResponseCode,
                errors);

            return result;
        }

        private List<ErrorObject> ErrorToList(string code, string message)
        {
            return new List<ErrorObject>
            {
                new ErrorObject()
                {
                    Code = code,
                    Message = message,
                }
            };
        }
    }
}