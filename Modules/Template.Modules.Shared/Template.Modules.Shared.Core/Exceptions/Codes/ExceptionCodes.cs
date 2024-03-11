using Template.Modules.Shared.Core.Extensions;

namespace Template.Modules.Shared.Core.Exceptions.Codes
{
    public static class ExceptionCodes
    {
        public static string GetInvalidValueCode(string fieldName)
        {
            return $"invalid_{fieldName.ToSnakeCase()}_value";
        }
        
        public static string GetInvalidLengthCode(string fieldName)
        {
            return $"invalid_{fieldName.ToSnakeCase()}_length";
        }
    }
}
