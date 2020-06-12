using System;
using System.Linq;

namespace PasswordGenerator
{
    internal sealed class PasswordPolicySetting : IDisposable
    {
        public string AllAvailableChars { get; private set; }
        internal string AllLowerCaseChars { get; set; }
        internal string AllUpperCaseChars { get; set; }
        internal string AllNumericChars { get; set; }
        internal string AllSpecialChars { get; set; }

        public PasswordPolicySetting()
        {
            AllLowerCaseChars = GetCharRange('a', 'z');
            AllUpperCaseChars = GetCharRange('A', 'Z');
            AllNumericChars = GetCharRange('0', '9');
            AllSpecialChars = @"~!@#$%^&*_-+=`|\(){}[]:;<>,.?/";
        }

        public void CharsConf(Action<PasswordPolicyCharacter> characterOptions)
        {
            if (characterOptions != null)
            {
                var passwordPolicyCharacters = new PasswordPolicyCharacter();
                characterOptions(passwordPolicyCharacters);
                passwordPolicyCharacters.SetNewPasswordChars(this);
            }
        }

        private readonly int _minimumNumberOfChars;
        public PasswordPolicySetting(int minimumNumberOfChars, PasswordPolicy passwordPolicy, PasswordPolicySetting policySetting)
        {
            _minimumNumberOfChars = minimumNumberOfChars;
            AllLowerCaseChars = policySetting.AllLowerCaseChars;
            AllUpperCaseChars = policySetting.AllUpperCaseChars;
            AllNumericChars = policySetting.AllNumericChars;
            AllSpecialChars = policySetting.AllSpecialChars;

            AllAvailableChars =
                GetOnlyIfOneCharIsRequired(passwordPolicy.MinimumLowerCaseChars, AllLowerCaseChars) +
                GetOnlyIfOneCharIsRequired(passwordPolicy.MinimumUpperCaseChars, AllUpperCaseChars) +
                GetOnlyIfOneCharIsRequired(passwordPolicy.MinimumNumericChars, AllNumericChars) +
                GetOnlyIfOneCharIsRequired(passwordPolicy.MinimumSpecialChars, AllSpecialChars);
        }

        private string GetOnlyIfOneCharIsRequired(int minimum, string allChars)
        {
            return minimum > 0 || _minimumNumberOfChars == 0 ? allChars : string.Empty;
        }

        internal string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            var result = string.Empty;
            for (char value = minimum; value <= maximum; value++)
            {
                result += value;
            }
            if (!string.IsNullOrEmpty(exclusiveChars))
            {
                var inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }

            return result;
        }

        public void Dispose()
        {
            AllAvailableChars = null;
            AllLowerCaseChars = null;
            AllNumericChars = null;
            AllSpecialChars = null;
            AllUpperCaseChars = null;

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
