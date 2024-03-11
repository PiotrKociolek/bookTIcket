namespace Template.Modules.Shared.Core.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrEmpty(this object input)
        {
            if (input == null)
            {
                return true;
            }

            switch (input)
            {
                case string s:
                {
                    return string.IsNullOrWhiteSpace(s);
                }
                default:
                {
                    throw new ArgumentException("type_not_supported");
                }
            }
        }
    }
}