using System;

namespace PasswordGenerator
{
    internal static class Policy
    {
        public static void ThrowIfPolicyIsInValid(int minimumLengthPassword, int maximumLengthPassword, int minimumLowerCaseChars,
                                  int minimumUpperCaseChars, int minimumNumericChars, int minimumSpecialChars, int minimumNumberOfChars)
        {
            if (minimumLengthPassword < 1)
            {
                throw new ArgumentException("The minimumlength is smaller than 1.", nameof(PasswordPolicy.MinimumPasswordLength));
            }

            if (minimumLengthPassword > maximumLengthPassword)
            {
                throw new ArgumentException("The minimumLength is bigger than the maximum length.", nameof(PasswordPolicy.MaximumPasswordLength));
            }

            if (minimumLowerCaseChars < 0)
            {
                throw new ArgumentException("The minimumLowerCase is smaller than 0.", nameof(PasswordPolicy.MinimumLowerCaseChars));
            }

            if (minimumUpperCaseChars < 0)
            {
                throw new ArgumentException("The minimumUpperCase is smaller than 0.", nameof(PasswordPolicy.MinimumUpperCaseChars));
            }

            if (minimumNumericChars < 0)
            {
                throw new ArgumentException("The minimumNumeric is smaller than 0.", nameof(PasswordPolicy.MinimumNumericChars));
            }

            if (minimumSpecialChars < 0)
            {
                throw new ArgumentException("The minimumSpecial is smaller than 0.", nameof(PasswordPolicy.MinimumSpecialChars));
            }

            if (minimumLengthPassword < minimumNumberOfChars)
            {
                throw new ArgumentException(
                        "The minimum length of the password is smaller than the sum " +
                        "of the minimum characters of all catagories.",
                        nameof(PasswordPolicy.MaximumPasswordLength));
            }
        }
    }
}
