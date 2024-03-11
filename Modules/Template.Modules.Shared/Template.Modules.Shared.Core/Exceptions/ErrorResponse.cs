namespace Template.Modules.Shared.Core.Exceptions
{
    public class ErrorResponse
    {
        public Guid ExceptionId { get; set; }
        public ICollection<ErrorObject> Errors { get; set; }
    }
}
