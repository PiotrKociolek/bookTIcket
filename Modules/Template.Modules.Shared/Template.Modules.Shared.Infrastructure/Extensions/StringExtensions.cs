namespace Template.Modules.Shared.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static int GetBase64EncodedFileSizeInBytes(this string base64String)
        {
            if (string.IsNullOrEmpty(base64String)) { return 0; }

            var characterCount = base64String.Length;
            var paddingCount = base64String.Substring(characterCount - 2, 2)
                .Count(c => c == '=');
            return (3 * (characterCount / 4)) - paddingCount;
        }
    }
}