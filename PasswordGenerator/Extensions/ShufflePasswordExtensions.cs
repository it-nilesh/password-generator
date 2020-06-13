using System;
using System.Collections.Generic;
using System.Linq;

namespace PasswordGenerator
{
    internal static class ShufflePasswordExtensions
    {
        private static readonly Lazy<RandomPasswordSecureVersion> _randomPasswordSecureVersion = new Lazy<RandomPasswordSecureVersion>(() => new RandomPasswordSecureVersion());
        public static IEnumerable<T> ShuffleSecure<T>(this IEnumerable<T> source)
        {
            var sourceArray = source.ToArray();
            for (int counter = 0; counter < sourceArray.Length; counter++)
            {
                int randomIndex = _randomPasswordSecureVersion.Value.Next(counter, sourceArray.Length);
                yield return sourceArray[randomIndex];

                sourceArray[randomIndex] = sourceArray[counter];
            }
        }

        public static string ShuffleText(this string source)
        {
            var shuffeldChars = source.ShuffleSecure().ToArray();
            return new string(shuffeldChars);
        }
    }
}
