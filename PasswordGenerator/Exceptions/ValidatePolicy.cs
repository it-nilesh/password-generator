using System;

namespace PasswordGenerator
{
    internal static class ValidatePolicy
    {
        public static void ThrowIfPolicyIsInValid(Policy policy)
        {
            if (policy.MinimumPasswordLength < 1)
            {
                throw new ArgumentException("The minimumlength is smaller than 1.", nameof(Policy.MinimumPasswordLength));
            }

            if (policy.MinimumPasswordLength > policy.MaximumPasswordLength)
            {
                throw new ArgumentException("The minimumLength is bigger than the maximum length.", nameof(policy.MaximumPasswordLength));
            }

            if (policy.MinimumLowerCaseChars < 0)
            {
                throw new ArgumentException("The minimumLowerCase is smaller than 0.", nameof(policy.MinimumLowerCaseChars));
            }

            if (policy.MinimumUpperCaseChars < 0)
            {
                throw new ArgumentException("The minimumUpperCase is smaller than 0.", nameof(policy.MinimumUpperCaseChars));
            }

            if (policy.MinimumNumericChars < 0)
            {
                throw new ArgumentException("The minimumNumeric is smaller than 0.", nameof(policy.MinimumNumericChars));
            }

            if (policy.MinimumSpecialChars < 0)
            {
                throw new ArgumentException("The minimumSpecial is smaller than 0.", nameof(policy.MinimumSpecialChars));
            }

            if (policy.MinimumPasswordLength < policy.MinimumNumberOfChars)
            {
                throw new ArgumentException(
                        "The minimum length of the password is smaller than the sum " +
                        "of the minimum characters of all catagories.",
                        nameof(policy.MaximumPasswordLength));
            }
        }
    }
}
