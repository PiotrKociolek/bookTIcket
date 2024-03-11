namespace Template.Modules.Shared.Core.Exceptions
{
    public class ValidatorException : ApplicationException
    {
        public ICollection<ErrorObject> Errors { get; protected set; }

        public ValidatorException(ICollection<ErrorObject> errors)
        {
            Errors = errors;
        }
    }
}
