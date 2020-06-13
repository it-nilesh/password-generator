using System.Linq;

namespace PasswordGenerator.Validate
{
    public class PasswordValidate
    {
        private int _minimumPasswordLength;
        private int _maximumPasswordLength;
        private int _minimumLowerCaseChars;
        private int _minimumUpperCaseChars;
        private int _minimumNumericChars;
        private int _minimumSpecialChars;
        private int _maximumUniqueChars;

        public PasswordValidate(int minimumPasswordLength, int maximumPasswordLength,
                                int minimumLowerCaseChars, int minimumUpperCaseChars,
                                int minimumNumericChars, int minimumSpecialChars,
                                int maximumUniqueChars)
        {
            _minimumPasswordLength = minimumPasswordLength;
            _maximumPasswordLength = maximumPasswordLength;
            _minimumLowerCaseChars = minimumLowerCaseChars;
            _minimumUpperCaseChars = minimumUpperCaseChars;
            _minimumNumericChars = minimumNumericChars;
            _minimumSpecialChars = minimumSpecialChars;
            _maximumUniqueChars = maximumUniqueChars;
        }

        public bool IsValid(string password)
        {
            bool status = false;
            if (_minimumPasswordLength >= password.Length && password.Length <= _maximumPasswordLength)
            {
                string d = "";
                foreach (char c in password)
                {
                    if (char.IsSymbol(c) || char.IsPunctuation(c))
                    {
                        d = d + c;
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
