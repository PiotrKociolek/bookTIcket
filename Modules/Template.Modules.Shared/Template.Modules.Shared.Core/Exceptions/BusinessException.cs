namespace Template.Modules.Shared.Core.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public string Code { get; protected set; }

        public BusinessException(string code)
        {
            Code = code;
        }
        
        public BusinessException(string code, string message)
            : base(message)
        {
            Code = code;
        }
    }
}
