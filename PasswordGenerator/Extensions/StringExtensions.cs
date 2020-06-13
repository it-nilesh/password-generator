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
    }
}
