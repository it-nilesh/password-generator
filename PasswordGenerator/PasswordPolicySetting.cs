using System;
using System.Linq;
using System.Text;

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

        public PasswordPolicySetting(int minimumNumberOfChars, PasswordPolicy passwordPolicy, PasswordPolicySetting policySetting)
        {
            AllLowerCaseChars = policySetting.AllLowerCaseChars;
            AllUpperCaseChars = policySetting.AllUpperCaseChars;
            AllNumericChars = policySetting.AllNumericChars;
            AllSpecialChars = policySetting.AllSpecialChars;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(minimumNumberOfChars, passwordPolicy.MinimumLowerCaseChars, AllLowerCaseChars));
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(minimumNumberOfChars, passwordPolicy.MinimumUpperCaseChars, AllUpperCaseChars));
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(minimumNumberOfChars, passwordPolicy.MinimumNumericChars, AllNumericChars));
            stringBuilder.Append(GetOnlyIfOneCharIsRequired(minimumNumberOfChars, passwordPolicy.MinimumSpecialChars, AllSpecialChars));
            AllAvailableChars = stringBuilder.ToString();
        }

        private string GetOnlyIfOneCharIsRequired(int minimumNumberOfChars, int minimum, string allChars)
        {
            return minimum > 0 || minimumNumberOfChars == 0 ? allChars : string.Empty;
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
