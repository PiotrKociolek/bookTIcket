using Template.Modules.Shared.Core.Exceptions;

namespace Template.Modules.Shared.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return $"{value.GetType().Name}_{value.ToString()}";
        }
        
        public static TEnum GetEnumValue<TEnum>(this string value) 
        {
            try
            {
                return (TEnum)Enum.Parse(typeof(TEnum), value);
            }
            catch
            {
                throw new BusinessException("invalid_enum_value", $"Value: '{value}' is not correct value for enum with type: '{typeof(TEnum)}'");
            }
        }
    }
}