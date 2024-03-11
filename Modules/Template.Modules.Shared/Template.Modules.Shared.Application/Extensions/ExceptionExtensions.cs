namespace Template.Modules.Shared.Application.Extensions
{
    public static class ExceptionExtensions
    {
        public static string ExceptionReturnedCode => "fatal_error";
        public static string ExceptionReturnedMessage => "Fatal error occured. Please contact with support.";
        public static string UnauthorizedReturnedCode => "unauthorized";
        public static string UnauthorizedReturnedMessage => "User unauthorized.";
        public static string UnauthenticatedReturnedCode => "unauthenticated";
        public static string UnauthenticatedReturnedMessage => "User unauthenticated.";

        public static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            var innerException = ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }

        public static ICollection<string> GetInnerMessages(this Exception ex)
        {
            return ex.GetInnerExceptions().Select(x => x.Message).ToList();
        }
    }
}