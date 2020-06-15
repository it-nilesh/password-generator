using System.Linq;

namespace PasswordGenerator
{
    internal static class StringExtensions
    {
        public static string ToRemoveChars(this string value, string removeChars)
        {
            return new string(value.Except(removeChars).ToArray());
        }

        public static string ToRemoveChars(this string value, char[] removeChars)
        {
            return new string(value.Except(removeChars).ToArray());
        }

        public static string RemoveDuplicateChars(this string value)
        {
            return new string(value.Distinct().ToArray());
        }

        public static bool IsNull(this object @obj)
        {
            return (obj is null);
        }

        public static bool IsNotNull(this object @obj)
        {
            return !(obj is null);
        }

        public static bool IsNotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str?.Trim());
        }
    }
}
