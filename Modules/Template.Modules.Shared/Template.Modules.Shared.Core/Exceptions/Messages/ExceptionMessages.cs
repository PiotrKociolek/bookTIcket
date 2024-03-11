using Template.Modules.Shared.Core.Extensions;

namespace Template.Modules.Shared.Core.Exceptions.Messages
{
    public static class ExceptionMessages
    {
        public static string GetInvalidValueMessage(string fieldName)
        {
            return $"Value of field '{fieldName.ToSnakeCase()}' is invalid.";
        }

        public static string GetNullValueMessage(string fieldName)
        {
            return $"Value of field '{fieldName.ToSnakeCase()}' cannot be null.";
        }

        public static string GetEmptyValueMessage(string fieldName)
        {
            return $"Value of field '{fieldName.ToSnakeCase()}' cannot be empty.";
        }
    }
}
