using System.Linq;

namespace PasswordGenerator.Validate
{
    public class PasswordValidate
    {
        private readonly int _minimumPasswordLength;
        private readonly int _maximumPasswordLength;
        private readonly int _maximumUniqueChars;

        private int _minimumLowerCaseChars;
        private int _minimumUpperCaseChars;
        private int _minimumNumericChars;
        private int _minimumSpecialChars;

        public PasswordValidate(Policy policy)
        {
            _minimumPasswordLength = policy.MinimumPasswordLength;
            _maximumPasswordLength = policy.MaximumPasswordLength;
            _minimumLowerCaseChars = policy.MinimumLowerCaseChars;
            _minimumUpperCaseChars = policy.MinimumUpperCaseChars;
            _minimumNumericChars = policy.MinimumNumericChars;
            _minimumSpecialChars = policy.MinimumSpecialChars;
            _maximumUniqueChars = policy.MaximumUniqueChars;
        }

        public bool IsValid(string password)
        {
            bool status = false;
            if (password.Length >= _minimumPasswordLength && password.Length <= _maximumPasswordLength)
            {
                foreach (char c in password)
                {
                    if (char.IsSymbol(c) || char.IsPunctuation(c))
                    {
                        _minimumSpecialChars--;
                    }
                    else if (char.IsLower(c))
                    {
                        _minimumLowerCaseChars--;
                    }
                    else if (char.IsUpper(c))
                    {
                        _minimumUpperCaseChars--;
                    }
                    else if (char.IsNumber(c))
                    {
                        _minimumNumericChars--;
                    }

                }

                status = IsCheck(_minimumLowerCaseChars) &&
                         IsCheck(_minimumUpperCaseChars) &&
                         IsCheck(_minimumNumericChars) &&
                         IsCheck(_minimumSpecialChars);
            }


            return status &&
                   IsIsCheckUniqueCharsFrom(password);
        }

        private bool IsIsCheckUniqueCharsFrom(string password)
        {
            int value = password.GroupBy(_ => _)
                     .Where(x => x.Count() == 1)
                     .Count() - _maximumUniqueChars;

            return value >= 0;
        }

        private bool IsCheck(int count)
        {
            return count <= 0;
        }
    }
}
